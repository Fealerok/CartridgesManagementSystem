﻿using CartridgeManagementSystem.Classes;
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
    /// Логика взаимодействия для AddingNewUser.xaml
    /// </summary>
    public partial class AddingNewUser : Window
    {

        public AddingNewUser()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод добавления нового пользователя
        /// </summary>
        /// <param name="sender">Объект кнопки Button</param>
        /// <param name="e">Объект события</param>
        private void AddNewUser(object sender, RoutedEventArgs e)
        {
            if (LoginTextBox.Text == string.Empty || PasswordTextBox.Text == string.Empty || RoleComboBox.SelectedItem == null)
            {
                MessageBox.Show("Не все поля заполнены");
            }
            else
            {
                Database.GetDatabase().AddNewUser(LoginTextBox.Text, PasswordTextBox.Text, RoleComboBox.Text);
                MessageBox.Show("Пользователь добавлен.");
                this.Close();
            }
        }
    }
}
