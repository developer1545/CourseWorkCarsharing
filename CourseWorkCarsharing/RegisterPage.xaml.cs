using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Security.Policy;
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
        public RegisterPage()
        {
            InitializeComponent();
            LoadComboBoxes();

        }
        private void LoadComboBoxes()
        {
            // Заполнение ComboBox для дат
            LoadDateComboBoxes(DayComboBox, MonthComboBox, YearComboBox, 1925);
            LoadDateComboBoxes(DayComboBox2, MonthComboBox2, YearComboBox2, 1970);
            LoadDateComboBoxes(DayComboBox3, MonthComboBox3, YearComboBox3, 1970);
            LoadDateComboBoxes(DayComboBox4, MonthComboBox4, YearComboBox4, 1970);
            LoadDateComboBoxes(DayComboBox5, MonthComboBox5, YearComboBox5, 1970);
            LoadDateComboBoxes1(DayComboBox6, MonthComboBox6, YearComboBox6, 2070);
        }

        private void LoadDateComboBoxes(ComboBox dayComboBox, ComboBox monthComboBox, ComboBox yearComboBox, int yearStart)
        {
            // Заполнение ComboBox для дней
            for (int i = 1; i <= 31; i++)
            {
                dayComboBox.Items.Add(i);
            }

            // Заполнение ComboBox для месяцев
            monthComboBox.ItemsSource = new List<string>
    {
        "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь",
        "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
    };

            // Заполнение ComboBox для годов (например, от 1900 до текущего года)
            int currentYear = DateTime.Now.Year;
            for (int i = yearStart; i <= currentYear; i++)
            {
                yearComboBox.Items.Add(i);
            }
        }
        private void LoadDateComboBoxes1(ComboBox dayComboBox, ComboBox monthComboBox, ComboBox yearComboBox, int yearStart)
        {
            // Заполнение ComboBox для дней
            for (int i = 1; i <= 31; i++)
            {
                dayComboBox.Items.Add(i);
            }

            // Заполнение ComboBox для месяцев
            monthComboBox.ItemsSource = new List<string>
    {
        "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь",
        "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
    };

            // Заполнение ComboBox для годов (например, от 1900 до текущего года)
            int currentYear = DateTime.Now.Year;
            for (int i = currentYear;i <= yearStart; i++)
            {
                yearComboBox.Items.Add(i);
            }
        }

        private void NumberPhone()
        {
            // Проверяем, есть ли текст в Box
            if (!string.IsNullOrWhiteSpace(NumberPhoneBox.Text))
            {
                // Получаем номер телефона из Box
                string phoneNumber = NumberPhoneBox.Text;

                // Проверяем формат номера телефона
                if (IsValidPhoneNumber(phoneNumber))
                {
                    // Если номер корректен, форматируем его
                    string formattedNumber = FormatPhoneNumber(phoneNumber);
                    NumPhoHint.Text = "";
                }
                else
                {
                    NumPhoHint.Text = "Неверный формат номера телефона. Пожалуйста, введите номер в формате: 7XXX-XXX-XXXX";
                }
            }
        }

        // Метод для проверки правильности формата номера телефона
        private bool IsValidPhoneNumber(string phone)
        {
            // Проверяем, что номер состоит только из цифр и имеет длину 10 цифр
            return Regex.IsMatch(phone, @"^\d{11}$");
        }

        public static string FormatPhoneNumber(string phone)
        {
           

            Regex regex = new Regex(@"[^\d]");
            phone = regex.Replace(phone, "");
            phone = Regex.Replace(phone, @"(\d{1})(\d{3})(\d{3})(\d{4})", "$1-$2-$3-$4");
            return phone;

        }
        private void NumberPhoneBoxChanged(object sender, TextChangedEventArgs e)
        {
            NumberPhone();
        }

        private void MailAdressChanged(object sender, TextChangedEventArgs e)
        {
            MailAddress();
        }

        private void MailAddress()
        {
            if (!string.IsNullOrWhiteSpace(MailAdressBox.Text))
            {
                string email = MailAdressBox.Text;

                // Проверяем формат адреса электронной почты
                if (IsValidEmail(email))
                {
                    MailAdrHint.Text = ""; // Очистка сообщения об ошибке
                }
                else
                {
                    MailAdrHint.Text = "Неверный формат адреса электронной почты. Пожалуйста, введите адрес в формате: example@domain.com";
                }
            }
        }

        // Метод для проверки правильности формата адреса электронной почты
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email; // Проверяем, совпадает ли адрес с исходным
            }
            catch
            {
                return false; // Если возникло исключение, значит формат неверный
            }
        }

        private void LoginTextChanged(object sender, TextChangedEventArgs e)
        {
            if (LoginBox.Text.Length >= 31)
            {
                LoginBoxHint.Text = "Длина логина не должна привышать 30 символов";
            }
            else
            {
                LoginBoxHint.Text = "";
            }
            }

        private void SeriaChanged(object sender, TextChangedEventArgs e)
        {
            if (SerialPasswordPaBox.Text.Length >= 5)
            {
                SerialPHint.Text = "Длина символов превышает стандартную";
            }
            else
            {
                SerialPHint.Text = "";
            }
        }

        private void NumberChanged(object sender, TextChangedEventArgs e)
        {
            if (NumberPasswordBox.Text.Length >= 7)
            {
                NumberPHint.Text = "Длина символов превышает стандартную";
            }
            else
            {
                NumberPHint.Text = "";
            }
        }

        private void SeriaAuChanged(object sender, TextChangedEventArgs e)
        {
            if (SerialAutoPaBox.Text.Length >= 5)
            {
                SerialAutoHint.Text = "Длина символов превышает стандартную";
            }
            else
            {
                SerialAutoHint.Text = "";
            }
        }

        private void NumberAuChanged(object sender, TextChangedEventArgs e)
        {
            if (NumberAutoPaBox.Text.Length >= 7)
            {
                NumberAutoHint.Text = "Длина символов превышает стандартную";
            }
            else
            {
                NumberAutoHint.Text = "";
            }
        }
    }
    }




