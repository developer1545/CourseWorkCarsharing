using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public class ImageItem
        {
            public string ImagePath { get; set; }
        }

        public ObservableCollection<ImageItem> Images { get; set; }
        private int currentIndex;
        public MainPage()
        {
            InitializeComponent();
            Images = new ObservableCollection<ImageItem>
        {
                new ImageItem { ImagePath = "mashine\\pngimg.com - citroen_PNG82.png" },
                new ImageItem { ImagePath = "C:\\Users\\antom\\source\\repos\\CourseWorkCarsharing\\CourseWorkCarsharing\\mashine\\pngimg.com - honda_PNG10350.png" },
                new ImageItem { ImagePath = "C:\\Users\\antom\\source\\repos\\CourseWorkCarsharing\\CourseWorkCarsharing\\mashine\\pngimg.com - ford_PNG102956.png" }
        };

            currentIndex = 0;
            UpdateImage();
        }
        private void UpdateImage()
        {
            CarImage.Source = new BitmapImage(new Uri(Images[currentIndex].ImagePath, UriKind.RelativeOrAbsolute));
        }
        public void UpdateFontSize(double newSize)
        {
            TextM1.FontSize = newSize;
            
       
        }
        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                UpdateImage();
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndex < Images.Count - 1)
            {
                currentIndex++;
                UpdateImage();
            }
        }

    }
}
