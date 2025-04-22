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
        public LoginPage()
        {
            InitializeComponent();
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
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!showPasswordCheckBox.IsChecked.GetValueOrDefault())
            {
                textBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                textBox.Text = LogPasswordBox.Password;
            }
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

        }
    }

