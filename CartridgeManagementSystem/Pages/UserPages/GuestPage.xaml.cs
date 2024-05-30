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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CartridgeManagementSystem.Pages.UserPages
{
    /// <summary>
    /// Логика взаимодействия для GuestPage.xaml
    /// </summary>
    public partial class GuestPage : Page
    {
        private Database _database = Database.GetDatabase();
        private Frame _navigationFrame;
        public GuestPage(Frame navigationFrame)
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
        private void userSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTable();
            List<CartridgeModel> cartridges = new List<CartridgeModel>();
            if (userSearch.Text == "" || userSearch.Text == string.Empty)
            {
                UpdateTable();
            }
            else
            {
                foreach (CartridgeModel cartridge in cartridgesList.Items)
                {
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
        /// Метод обновления элементов в таблице
        /// </summary>
        private void UpdateTable()
        {
            cartridgesList.ItemsSource = null;
            cartridgesList.ItemsSource = _database.GetListOfCartridges();
        }
    }
}
