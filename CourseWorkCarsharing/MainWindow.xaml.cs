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
using static System.Net.Mime.MediaTypeNames;

namespace CourseWorkCarsharing
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new MainPage());
            Manager.MainFrame = MainFrame;

        }

        private void MouseOutClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MouseMaximazeClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void FullscreenButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
               
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }
        private void MainWindow_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                // Измените размер шрифта на MainPage
                var mainPage = (MainPage)this.Content; // Предполагаем, что MainPage является контентом основного окна
                mainPage.UpdateFontSize(805);
            }
            else
            {
                // Вернуть шрифт в исходный размер, если нужно
                var mainPage = (MainPage)this.Content;
                mainPage.UpdateFontSize(12); // Установите ваш стандартный размер шрифта
            }
        }

        private void RulesButtonClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new RulesPage());
        }

      

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
     


    }
}
