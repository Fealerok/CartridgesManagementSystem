using CartridgeManagementSystem.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CartridgeManagementSystem.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private Database _database = Database.GetDatabase();
        public string userRole = "";
        public LoginWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод очищения текстовых полей
        /// </summary>
        /// <param name="sender">Объект строки TextBox</param>
        /// <param name="e">Объект события</param>
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text.ToLower() == "логин" || (sender as TextBox).Text.ToLower() == "пароль")
            {
                (sender as TextBox).Text = string.Empty;
                (sender as TextBox).Foreground = Brushes.Black;
            }
        }

        /// <summary>
        /// Метод возврата стирающихся надписей "Логин" и "Пароль" на свои места в текстовых полях.
        /// </summary>
        /// <param name="sender">Объект строки TextBox</param>
        /// <param name="e">Объект события</param>
        private void LoginTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text.ToLower() == string.Empty)
            {
                switch ((sender as TextBox).Name)
                {
                    case "LoginTextBox":
                        (sender as TextBox).Text = "Логин";
                        break;
                    case "PasswordTextBox":
                        (sender as TextBox).Text = "Пароль";
                        break;
                }
                (sender as TextBox).Foreground = Brushes.Gray;
            }
        }

        /// <summary>
        /// Метод входа в систему по логину и паролю.
        /// </summary>
        /// <param name="sender">Объект кнопки Button</param>
        /// <param name="e">Объект события</param>
        private void Login(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.ToString().ToLower() == "войти как гость")
            {
                userRole = "Гость";
                this.Close();
            }
            if ((LoginTextBox.Text != string.Empty && LoginTextBox.Text.ToLower() != "логин") && 
                (PasswordTextBox.Text != string.Empty && PasswordTextBox.Text.ToLower() != "пароль"))
            {
                userRole = _database.GetUserRoleByLoginPassword(LoginTextBox.Text, PasswordTextBox.Text);
                if (userRole != "null")
                {
                    this.Close();
                }
            }
        }
    }
}
