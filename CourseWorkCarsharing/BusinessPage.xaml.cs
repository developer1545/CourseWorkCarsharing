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
    /// Логика взаимодействия для BusinessPage.xaml
    /// </summary>
    public partial class BusinessPage : Page
    {
        public BusinessPage()
        {
            InitializeComponent();
        }

        private void ButtonInfo(object sender, RoutedEventArgs e)
        {
            ScrollViewer.ScrollToVerticalOffset(StackPanel.TranslatePoint(new Point(0, 0), ScrollViewer).Y);
        }

        private void RegisterCompanyClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
