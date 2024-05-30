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
    /// Логика взаимодействия для EditingCartridge.xaml
    /// </summary>
    public partial class EditingCartridge : Window
    {
        private CartridgeModel _cartridge;
        public EditingCartridge(CartridgeModel cartridge)
        {
            InitializeComponent();
            _cartridge = cartridge;

            //Подстановка всех данных выбранного картриджа сразу после открытия страницы
            TypeTextBox.Text = cartridge.Type;
            ModelTextBox.Text = cartridge.Model;
            SerialNumberTextBox.Text = cartridge.SerialNumber.ToString();
            StatusComboBox.Text = cartridge.Status;
            DescriptionTextBox.Text = cartridge.Description;

        }

        /// <summary>
        /// Метод сохранения измененных данных в БД.
        /// </summary>
        /// <param name="sender">Объект кнопки Button</param>
        /// <param name="e">Объект события</param>
        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            Database.GetDatabase().EditCartridge(TypeTextBox.Text, ModelTextBox.Text, long.Parse(SerialNumberTextBox.Text), StatusComboBox.Text, DescriptionTextBox.Text, _cartridge.Id);
            MessageBox.Show("Картридж изменен");
            this.Close();
        }
    }
}
