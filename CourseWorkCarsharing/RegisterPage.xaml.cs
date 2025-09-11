using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
    
            private string _password = string.Empty;
            private bool _isUpdatingPasswordFromBox = false;

            public RegisterPage()
            {
                InitializeComponent();

                PasswordBox.PasswordChanged += PasswordBox_PasswordChanged;
                textBox.TextChanged += TextBox_TextChanged;
            }

            private void ButtonRegisterClick(object sender, RoutedEventArgs e)
            {
                StringBuilder errors = new StringBuilder();

                string login = LoginBox.Text.Trim();
                string password = showPasswordCheckBox.IsChecked == true ? textBox.Text : PasswordBox.Password;
                string phone = NumberPhoneBox.Text.Trim();
                string email = MailAdressBox.Text.Trim();
                string firstName = NameBox.Text.Trim();
                string lastName = FamilyBox.Text.Trim();
                DateTime? birthday = DatePickerBurd.SelectedDate;

                // Валидация
                if (string.IsNullOrWhiteSpace(login))
                    errors.AppendLine("Укажите логин.");

                if (string.IsNullOrWhiteSpace(password))
                    errors.AppendLine("Укажите пароль.");

                if (password.Length > 50)
                    errors.AppendLine("Длина пароля не должна превышать 50 символов.");

                if (string.IsNullOrWhiteSpace(phone))
                    errors.AppendLine("Укажите номер телефона.");

                if (!IsValidPhoneNumber(phone))
                    errors.AppendLine("Неверный формат номера телефона. Введите 11 цифр, например: 79991234567.");

                if (string.IsNullOrWhiteSpace(email))
                    errors.AppendLine("Укажите адрес электронной почты.");

                if (!IsValidEmail(email))
                    errors.AppendLine("Неверный формат электронной почты.");

                if (string.IsNullOrWhiteSpace(firstName))
                    errors.AppendLine("Укажите имя.");

                if (string.IsNullOrWhiteSpace(lastName))
                    errors.AppendLine("Укажите фамилию.");

                if (!birthday.HasValue)
                    errors.AppendLine("Укажите дату рождения.");
                else
                {
                    if (birthday.Value > DateTime.Today)
                        errors.AppendLine("Дата рождения не может быть в будущем.");

                    if (birthday.Value > DateTime.Today.AddYears(-16))
                        errors.AppendLine("Пользователь должен быть не младше 16 лет.");
                }

                // Проверка уникальности логина и email
                if (CarsharingBDEntities.GetContext().Users.Any(u => u.Login == login))
                    errors.AppendLine("Пользователь с таким логином уже существует.");

                if (CarsharingBDEntities.GetContext().Users.Any(u => u.Email == email))
                    errors.AppendLine("Пользователь с таким email уже существует.");

                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString(), "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                try
                {
                    var newUser = new User
                    {
                        Login = login,
                        Password = password,
                        Phone = phone,
                        Email = email,
                        Name = firstName + " " + lastName,
                        Birthday = birthday.Value,
                        CreatedAt = DateTime.Now,
                        Role = "User  "
                    };

                    CarsharingBDEntities.GetContext().Users.Add(newUser);
                    CarsharingBDEntities.GetContext().SaveChanges();

                    MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Переход на страницу входа
                    this.NavigationService.Navigate(new LoginPage());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при регистрации: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            private bool IsValidPhoneNumber(string phone)
            {
                // Проверяем, что номер состоит из 11 цифр (например, 7XXXXXXXXXX)
                return Regex.IsMatch(phone, @"^\d{11}$");
            }

            private bool IsValidEmail(string email)
            {
                try
                {
                    var addr = new MailAddress(email);
                    return addr.Address == email;
                }
                catch
                {
                    return false;
                }
            }

            private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
            {
                if (_isUpdatingPasswordFromBox)
                    return;

                _password = PasswordBox.Password;
                _isUpdatingPasswordFromBox = true;
                if (showPasswordCheckBox.IsChecked == true)
                {
                    textBox.Text = _password;
                }
                _isUpdatingPasswordFromBox = false;
            }

            private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
            {
                if (_isUpdatingPasswordFromBox)
                    return;

                _password = textBox.Text;
                _isUpdatingPasswordFromBox = true;
                if (showPasswordCheckBox.IsChecked != true)
                {
                    PasswordBox.Password = _password;
                }
                _isUpdatingPasswordFromBox = false;
            }

            private void ShowPasswordCheckBox_Checked(object sender, RoutedEventArgs e)
            {
                textBox.Visibility = Visibility.Visible;
                PasswordBox.Visibility = Visibility.Collapsed;
                textBox.Text = _password;
                showPasswordCheckBox.Content = "Скрыть пароль 🔓";
            }

            private void ShowPasswordCheckBox_Unchecked(object sender, RoutedEventArgs e)
            {
                textBox.Visibility = Visibility.Collapsed;
                PasswordBox.Visibility = Visibility.Visible;
                PasswordBox.Password = _password;
                showPasswordCheckBox.Content = "Показать пароль 🔒";
            }
        }
    }