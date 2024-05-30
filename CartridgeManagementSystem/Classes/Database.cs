using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Data.Sqlite;

namespace CartridgeManagementSystem.Classes
{
    internal class Database
    {
        private static Database _database;

        SqliteConnection connection = new SqliteConnection("Data Source=CartridgeManagementSystem.db");
        private Database(){}
        public static Database GetDatabase()
        {
            if (_database == null)
            {
                _database = new Database();
            }

            return _database;
        }
        
        /// <summary>
        /// Функция получения роли пользователя по его логину и паролю из БД
        /// </summary>
        /// <param name="userLogin">Логин пользователя</param>
        /// <param name="userPassword">Пароль пользователя</param>
        /// <returns>Роль пользователя</returns>
        public string GetUserRoleByLoginPassword(string userLogin, string userPassword)
        {
            string userRole = "";

            connection.Open();
            string sqlExpression = $"SELECT Title FROM Users JOIN Roles ON Users.RoleId = Roles.Id WHERE Users.Login = '{userLogin}' AND Users.Password = '{userPassword}'";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read())   // построчно считываем данные
                    {
                        userRole = reader.GetString(0);
                    }
                }

                else
                {
                    return "null";
                }
            }

            connection.Close();

            return userRole;
        }

        /// <summary>
        /// Функция получения списка всех картриджей из БД
        /// </summary>
        /// <returns>Список с объектами типа CartridgeModel</returns>
        public List<CartridgeModel> GetListOfCartridges()
        {
            List<CartridgeModel> cardridges = new List<CartridgeModel> ();

            connection.Open();
            string sqlExpression = $"SELECT *, Title FROM Cartridges JOIN CartridgesStatuses ON Cartridges.StatusId = CartridgesStatuses.Id";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read())   // построчно считываем данные
                    {
                        cardridges.Add(new CartridgeModel
                        {
                            Id = reader.GetInt32(0),
                            Type = reader.GetString(1),
                            Model = reader.GetString(2),
                            SerialNumber = reader.GetInt64(3),
                            Status = reader.GetString(7),
                            Description = reader.GetString(5),

                        });
                    }
                }
            }

            connection.Close();

            return cardridges;
        }

        /// <summary>
        /// Функция получения списка всех пользователей из БД
        /// </summary>
        /// <returns>Список с объектами типа UserModel</returns>
        public List<UserModel> GetListOfUsers()
        {
            List<UserModel> users = new List<UserModel> ();

            connection.Open();
            string sqlExpression = $"SELECT *, Title FROM Users JOIN Roles ON Users.RoleId = Roles.Id";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read())   // построчно считываем данные
                    {
                        users.Add(new UserModel
                        {
                            Id = reader.GetInt32(0),
                            Role = reader.GetString(5),
                            Login = reader.GetString(2),
                            Password = reader.GetString(3),
                        });
                    }
                }
            }

            connection.Close();

            return users;

        }

        /// <summary>
        /// Метод добавления нового пользователя в БД
        /// </summary>
        /// <param name="login">Логин нового пользователя</param>
        /// <param name="password">Пароль нового пользователя</param>
        /// <param name="role">Роль нового пользователя</param>
        public void AddNewUser(string login, string password, string role)
        {
            connection.Open();
            string sqlExpression = $"INSERT INTO Users(RoleId, Login, Password) VALUES((SELECT Id FROM Roles WHERE Title = '{role}'), '{login}', '{password}')";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);

            command.ExecuteNonQuery();

            connection.Close();
        }

        /// <summary>
        /// Метод добавления нового картриджа в БД
        /// </summary>
        /// <param name="type">Тип нового картриджа</param>
        /// <param name="model">Модель нового картриджа</param>
        /// <param name="serialNumber">Серийный номер нового картриджа</param>
        /// <param name="status">Статус нового картриджа</param>
        /// <param name="description">Описание нового картриджа</param>
        public void AddNewCartridge(string type, string model, long serialNumber, string status, string description)
        {
            connection.Open();
            string sqlExpression = $"INSERT INTO Cartridges(Type, Model, SerialNumber, StatusId, Description)" +
                                    $"VALUES('{type}', '{model}', {serialNumber}, (SELECT Id FROM CartridgesStatuses WHERE Title = '{status}'), '{description}')";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);

            command.ExecuteNonQuery();

            connection.Close();
        }

        /// <summary>
        /// Метод удаления картриджа из БД
        /// </summary>
        /// <param name="id">Идентификатор выбранного картриджа</param>
        public void DeleteCartridge(int id)
        {
            connection.Open();
            string sqlExpression = $"DELETE FROM Cartridges WHERE Id = {id}";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);
            command.ExecuteNonQuery();

            sqlExpression = "VACUUM";
            command.ExecuteNonQuery();

            connection.Close();
        }

        /// <summary>
        /// Метод редактирования картриджа в БД
        /// </summary>
        /// <param name="type">Тип картриджа</param>
        /// <param name="model">Модель картриджа</param>
        /// <param name="serialNumber">Серийный номер картриджа</param>
        /// <param name="status">Статус картриджа</param>
        /// <param name="description">Описание картриджа</param>
        /// <param name="id">Идентификатор картриджа</param>
        public void EditCartridge(string type, string model, long serialNumber, string status, string description, int id)
        {
            connection.Open();
            string sqlExpression = $"UPDATE Cartridges SET Type = '{type}', Model = '{model}', SerialNumber = {serialNumber}, StatusId = (SELECT Id FROM CartridgesStatuses WHERE Title = '{status}'), Description = '{description}' WHERE Id = {id}";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}
