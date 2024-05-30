using CartridgeManagementSystem.Classes;
using CartridgeManagementSystem.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CartridgeManagementSystem.Pages.UserPages
{
    public partial class ListOfUsers : Page
    {
        private Frame _navigationFrame;
        private Database _database = Database.GetDatabase();
        public ListOfUsers(Frame navigationFrame)
        {
            InitializeComponent();
            _navigationFrame = navigationFrame;
            usersList.ItemsSource = _database.GetListOfUsers();
        }

        /// <summary>
        /// Метод возврата на страницу назад
        /// </summary>
        /// <param name="sender">Объект кнопки Button</param>
        /// <param name="e">Объект события</param>
        private void TransferToBack(object sender, RoutedEventArgs e)
        {
            _navigationFrame.GoBack();
        }

        /// <summary>
        /// Метод добавления нового пользователя
        /// </summary>
        /// <param name="sender">Объект кнопки Button</param>
        /// <param name="e">Объект события</param>
        private void AddNewUser(object sender, RoutedEventArgs e)
        {
            new AddingNewUser().ShowDialog();
            UpdateTable();

        }

        /// <summary>
        /// Метод обновления элементов в таблице.
        /// </summary>
        private void UpdateTable()
        {
            usersList.ItemsSource = null;
            usersList.ItemsSource = _database.GetListOfUsers();
        }
    }
}
