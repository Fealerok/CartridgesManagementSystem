using CartridgeManagementSystem.Pages.UserPages;
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

namespace CartridgeManagementSystem.Pages.MainMenu
{
    /// <summary>
    /// Логика взаимодействия для StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        private Frame _navigationFrame;
        public StartPage(Frame navigationFrame)
        {
            InitializeComponent();

            _navigationFrame = navigationFrame;
        }

        /// <summary>
        /// Метод перенаправления на соответствующую страницу для зашедшего пользователя
        /// </summary>
        /// <param name="sender">Объект кнопки Button</param>
        /// <param name="e">Объект события</param>
        private void LoginToSystem(object sender, RoutedEventArgs e)
        {

            //Открываем новое окно и записываем в переменную для получения полей этого класса.
            var loginWindow = new LoginWindow();
            loginWindow.ShowDialog();

            switch (loginWindow.userRole.ToLower())
            {
                case "администратор":
                    AdministratorPage administratorPage = new AdministratorPage(_navigationFrame);
                    _navigationFrame.Navigate(administratorPage);
                    break;
                case "редактор":
                    EditorPage editorPage = new EditorPage(_navigationFrame);
                    _navigationFrame.Navigate(editorPage);
                    break;
                case "гость":
                    GuestPage guestPage = new GuestPage(_navigationFrame);
                    _navigationFrame.Navigate(guestPage);
                    break;
            }
            
        }
    }
}
