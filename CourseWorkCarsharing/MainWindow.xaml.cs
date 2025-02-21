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
            // Get the current page from the MainFrame
            var currentPage = MainFrame.Content as MainPage; // Safe cast to MainPage

            if (currentPage != null) // Check if the cast was successful
            {
                if (this.WindowState == WindowState.Maximized)
                {
                    // Change the font size when maximized
                    currentPage.UpdateFontSize(115);
                }
                else
                {
                    // Reset the font size when not maximized
                    currentPage.UpdateFontSize(68); // Set your standard font size
                }
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
