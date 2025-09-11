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

namespace CourseWorkCarsharing
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private bool isLogin = false;
        
            private bool _isUpdatingPassword = false;

            public LoginPage()
            {
                InitializeComponent();
            }
        private void ButtonLoginClick(object sender, RoutedEventArgs e)
        {
            string login = LogTextBox.Text.Trim();
            string password = showPasswordCheckBox.IsChecked == true ? textBox.Text : LogPasswordBox.Password;

            var user = CarsharingBDEntities.GetContext().Users
                .FirstOrDefault(u => u.Login == login && u.Password == password);

            if (user != null)
            {
                MessageBox.Show($"Добро пожаловать, {user.Name}!", "Успешный вход", MessageBoxButton.OK, MessageBoxImage.Information);

                Application.Current.Properties["IsLoggedIn"] = true;
                Application.Current.Properties["User Name"] = user.Name;
                Application.Current.Properties["User Role"] = user.Role;

                // Проверяем роль пользователя
                if (user.Role.Trim() == "Admin")
                {
                    var adminWindow = new AdminWindow();
                    adminWindow.Show();
                   // //Application.Current.MainWindow.Close();
                }
                else
                {
                    this.NavigationService.Navigate(new MainPage());
                }
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
       

            private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
            {
                if (_isUpdatingPassword) return;

                _isUpdatingPassword = true;
                if (showPasswordCheckBox.IsChecked == true)
                {
                    textBox.Text = LogPasswordBox.Password;
                }
                _isUpdatingPassword = false;
            }

            private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
            {
                if (_isUpdatingPassword) return;

                _isUpdatingPassword = true;
                if (showPasswordCheckBox.IsChecked == true)
                {
                    LogPasswordBox.Password = textBox.Text;
                }
                _isUpdatingPassword = false;
            }

            private void ShowPasswordCheckBox_Checked(object sender, RoutedEventArgs e)
            {
                textBox.Visibility = Visibility.Visible;
                textBox.Text = LogPasswordBox.Password;
                LogPasswordBox.Visibility = Visibility.Collapsed;
                showPasswordCheckBox.Content = "Скрыть пароль 🔓";
            }

            private void ShowPasswordCheckBox_Unchecked(object sender, RoutedEventArgs e)
            {
                LogPasswordBox.Visibility = Visibility.Visible;
                LogPasswordBox.Password = textBox.Text;
                textBox.Visibility = Visibility.Collapsed;
                showPasswordCheckBox.Content = "Показать пароль 🔒";
            }

            private void MouseRegEnter(object sender, MouseEventArgs e)
            {
                TextRegPage.TextDecorations = TextDecorations.Underline;
            }

            private void MouseRegLeave(object sender, MouseEventArgs e)
            {
                TextRegPage.TextDecorations = null;
            }

            private void RegisterButtonClick(object sender, MouseButtonEventArgs e)
            {
                this.NavigationService.Navigate(new RegisterPage());
            }
        }
    }
