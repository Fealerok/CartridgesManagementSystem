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
    /// <summary>
    /// Логика взаимодействия для AdministratorPage.xaml
    /// </summary>
    public partial class AdministratorPage : Page
    {
        private Database _database = Database.GetDatabase();
        private Frame _navigationFrame;
        public AdministratorPage(Frame navigationFrame)
        {
            InitializeComponent();
            _navigationFrame = navigationFrame;

            cartridgesList.ItemsSource = _database.GetListOfCartridges();
        }

        /// <summary>
        /// Метод, срабатываемый при изменении строки поиска
        /// </summary>
        /// <param name="sender">Объект строки TextBox</param>
        /// <param name="e">Объект события</param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTable();
            List<CartridgeModel> cartridges = new List<CartridgeModel>();
            if (userSearch.Text == "" || userSearch.Text == string.Empty)
            {
                UpdateTable();
            }
            else
            {
                foreach(CartridgeModel cartridge in cartridgesList.Items)
                {
                    //Проверка на содержание введенного текста пользователем во всех колонках в таблице.
                    if (cartridge.Type.Contains(userSearch.Text) || cartridge.Model.Contains(userSearch.Text) || cartridge.SerialNumber.ToString().Contains(userSearch.Text) || cartridge.Status.Contains(userSearch.Text))
                    {
                        cartridges.Add(cartridge);
                    }
                }
                cartridgesList.ItemsSource = null;
                cartridgesList.ItemsSource = cartridges;
            }

            
        }

        /// <summary>
        /// Метод отображения всего списка пользователей
        /// </summary>
        /// <param name="sender">Объект кнопки Button</param>
        /// <param name="e">Объект события</param>
        private void ShowListOfUsers(object sender, RoutedEventArgs e)
        {
            ListOfUsers listOfUsers = new ListOfUsers(_navigationFrame);
            _navigationFrame.Navigate(listOfUsers);
        }

        /// <summary>
        /// Метод добавления нового картриджа в БД
        /// </summary>
        /// <param name="sender">Объект кнопки Button</param>
        /// <param name="e">Объект события</param>
        private void AddNewCartridge(object sender, RoutedEventArgs e)
        {
            new AddingNewCartridge().ShowDialog();
            UpdateTable();
        }

        /// <summary>
        /// Метод удаления картриджа из БД
        /// </summary>
        /// <param name="sender">Объект кнопки Button</param>
        /// <param name="e">Объект события</param>
        private void DeleteCartridge(object sender, RoutedEventArgs e)
        {
            if (cartridgesList.SelectedItem == null)
            {
                MessageBox.Show("Для удаления необходимо выбрать элемент.");
            }
            else
            {
                _database.DeleteCartridge(((CartridgeModel)cartridgesList.SelectedItem).Id);
                UpdateTable();
            }
        }

        /// <summary>
        /// Метод редактирования картриджа в БД
        /// </summary>
        /// <param name="sender">Объект кнопки Button</param>
        /// <param name="e">Объект события</param>
        private void EditCartridge(object sender, RoutedEventArgs e)
        {
            if (cartridgesList.SelectedItem == null)
            {
                MessageBox.Show("Для редактирования необходимо выбрать элемент.");
            }
            else
            {
                new EditingCartridge((CartridgeModel)cartridgesList.SelectedItem).ShowDialog();
                UpdateTable();
            }
        }

        /// <summary>
        /// Метод обновления элементов в таблице
        /// </summary>
        private void UpdateTable()
        {
            cartridgesList.ItemsSource = null;
            cartridgesList.ItemsSource = _database.GetListOfCartridges();
        }
    }
}
