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

namespace CourseWorkCarsharing
{
    /// <summary>
    /// Логика взаимодействия для MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        private bool isLogin = false;
        private string NameAccount = "";
        private string selectedPricingName = "";
        private string selectedCarInfo = "";
        private string selectedParkingName = "";

        public MessageWindow(string selectedPricingName, string selectedCarInfo, string selectedParkingName, bool isLogin = false, string nameAccount = "")
        {
            InitializeComponent();
            TextWriten();
            this.selectedPricingName = selectedPricingName;
            this.selectedCarInfo = selectedCarInfo;
            this.selectedParkingName = selectedParkingName;
            this.isLogin = isLogin;
            this.NameAccount = nameAccount;
            UpdateOrderSummary();
        }
        private void UpdateOrderSummary()
        {
            OrderText.Text = selectedPricingName;
            AutoText.Text = selectedCarInfo;
            ParkAutoText.Text = selectedParkingName;
        }
            private  void TextWriten()
        {
           
            if (isLogin == false)
            {
                WindowText.Text = "Вы не вошли в аккаунт";
                WindowButton.Content = "Войти";
                WindowButton.Click += (sender, e) => { this.Close(); } ;
            }
            else { WindowText.Text = "Ваш аккаунт"; WindowButton.Content = $"{NameAccount}"; }
        }
        private void MessageWindow_Closed(object sender, EventArgs e)
        {
            // Здесь запускаем новое окно после закрытия текущего
            var nextWindow = new MainWindow(); // или любое другое окно
            
        }

        private void ButtonClickOrderM(object sender, RoutedEventArgs e)
        {

        }
    }
}
