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
            this.StateChanged += MainWindow_StateChanged;
           
        }
        private void UpdateBackButtonVisibility()
        {
            ButtonBack.Visibility = MainFrame.CanGoBack ? Visibility.Visible : Visibility.Collapsed;
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
                //Кнопка заказа
                /*
                ButtonOrder.Width = 400;
                ButtonOrder.Height = 80;
                */
               
            }
            else
            {
                this.WindowState = WindowState.Normal;
                //ButtonOrder.Width = 249;
                //ButtonOrder.Height = 53;
                
            }
        }
        private void MainWindow_StateChanged(object sender, EventArgs e)
        {
            
            var currentPage = MainFrame.Content as MainPage; 

            if (currentPage != null) 
            {
                if (this.WindowState == WindowState.Maximized)
                {
         
                    currentPage.UpdateFontSize(115);
                }
                else
                {
                 
                    currentPage.UpdateFontSize(68); 
                }
            }
        }


        private void RulesButtonClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new RulesPage());
        }

      

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            UpdateBackButtonVisibility();
        }

        private void pricingPlansPClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new pricingPlansPage());
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            // Получаем NavigationService для текущего Frame
            //var navigationService = MainFrame.NavigationService;

            //// Проверяем, можно ли вернуться на предыдущую страницу
            //if (navigationService.CanGoBack)
            //{
            //    navigationService.GoBack();
            //}
            if (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
                UpdateBackButtonVisibility();
            }


        }

        private void MainButtonClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new MainPage());
        }

        private void HelpPageClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new HelpPage());
        }

        private void ButtonBusinessClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new BusinessPage());
        }

        private void ParkButtonClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ParkAutoPage());
        }
    }
}
