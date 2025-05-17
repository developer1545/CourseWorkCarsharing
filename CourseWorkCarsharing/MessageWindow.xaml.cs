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
        private bool isRegistered = false;
        private string NameAccount = "";
        
        public MessageWindow()
        {
            InitializeComponent();
            TextWriten();
           
        }
        private  void TextWriten()
        {
           
            if (isRegistered == false)
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
    }
}
