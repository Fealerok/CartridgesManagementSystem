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
    /// Логика взаимодействия для AddingNewCartridge.xaml
    /// </summary>
    public partial class AddingNewCartridge : Window
    {
        public AddingNewCartridge()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод добавления нового картриджа
        /// </summary>
        /// <param name="sender">Объект кнопки Button</param>
        /// <param name="e">Объект события</param>
        private void AddNewCartridge(object sender, RoutedEventArgs e)
        {
            if (TypeTextBox.Text == string.Empty || ModelTextBox.Text == string.Empty || SerialNumberTextBox.Text == string.Empty || StatusComboBox.SelectedItem == null)
            {
                MessageBox.Show("Не все поля заполнены");
            }
            else
            {
                Database.GetDatabase().AddNewCartridge(TypeTextBox.Text, ModelTextBox.Text, long.Parse(SerialNumberTextBox.Text), StatusComboBox.Text, DescriptionTextBox.Text);
                MessageBox.Show("Добавлен новый картридж");
                this.Close();
            }
        }
    }
}
