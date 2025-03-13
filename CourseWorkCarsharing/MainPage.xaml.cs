using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        public class TextCarName
        {
            public string Name;
        }
  
        public ObservableCollection<ImageItem> Images { get; set; }
        private int currentIndex;
        public string[] name;
        public MainPage()
        {
            InitializeComponent();
            Images = new ObservableCollection<ImageItem>
            {
                    new ImageItem { ImagePath = "mashine\\pngimg.com - citroen_PNG82.png" },
                    new ImageItem { ImagePath = "C:\\Users\\antom\\source\\repos\\CourseWorkCarsharing\\CourseWorkCarsharing\\mashine\\pngimg.com - honda_PNG10350.png" },
                    new ImageItem { ImagePath = "C:\\Users\\antom\\source\\repos\\CourseWorkCarsharing\\CourseWorkCarsharing\\mashine\\pngimg.com - ford_PNG102956.png" },
                    new ImageItem { ImagePath = "mashine\\pngimg.com - citroen_PNG82.png" },
                    new ImageItem { ImagePath = "C:\\Users\\antom\\source\\repos\\CourseWorkCarsharing\\CourseWorkCarsharing\\mashine\\pngimg.com - honda_PNG10350.png" },
                    new ImageItem { ImagePath = "C:\\Users\\antom\\source\\repos\\CourseWorkCarsharing\\CourseWorkCarsharing\\mashine\\pngimg.com - ford_PNG102956.png" }
            };
            name = new string[6] { "Sitroen DS3", "Honda Civic","Ford Focus", "Sitroen DS3", "Honda Civic", "Ford Focus" };
         

            DataContext = this;
            currentIndex = 0;
            UpdateImage();
        }
    
        private void UpdateImage()
        {
            /*
            CarImage.Source = new BitmapImage(new Uri(Images[currentIndex].ImagePath, UriKind.RelativeOrAbsolute));
            CarImage1.Source = new BitmapImage(new Uri(Images[currentIndex+1].ImagePath, UriKind.RelativeOrAbsolute));
            CarImage2.Source = new BitmapImage(new Uri(Images[currentIndex+2].ImagePath, UriKind.RelativeOrAbsolute));
            CarName.Text = name[currentIndex];
            CarName1.Text = name[currentIndex+1];
            CarName2.Text = name[currentIndex+2];
            */
            if (currentIndex >= 0 && currentIndex < Images.Count)
            {
                CarImage.Source = new BitmapImage(new Uri(Images[currentIndex].ImagePath, UriKind.RelativeOrAbsolute));
                CarName.Text = name[currentIndex];
            }

            // Проверяем, есть ли изображение для CarImage1
            if (currentIndex + 1 >= 0 && currentIndex + 1 < Images.Count)
            {
                CarImage1.Source = new BitmapImage(new Uri(Images[currentIndex + 1].ImagePath, UriKind.RelativeOrAbsolute));
                CarName1.Text = name[currentIndex + 1];
            }

            // Проверяем, есть ли изображение для CarImage2
            if (currentIndex + 2 >= 0 && currentIndex + 2 < Images.Count)
            {
                CarImage2.Source = new BitmapImage(new Uri(Images[currentIndex + 2].ImagePath, UriKind.RelativeOrAbsolute));
                CarName2.Text = name[currentIndex + 2];
            }
            

        }
        public void UpdateFontSize(double newSize)
        {
            //TextM1.FontSize = newSize;
            
       
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
           
            if (currentIndex < Images.Count-3) 
            {
                currentIndex++;
                UpdateImage();
            }
        }

        private void ButtonMailClick(object sender, RoutedEventArgs e)
        {
            string email = "carsharingbusiness@mail.ru";
            string subject = "Тема сообщения";
            string body = "Текст сообщения";

            // Создайте mailto URL
            string mailtoUrl = $"mailto:{email}?subject={Uri.EscapeDataString(subject)}&body={Uri.EscapeDataString(body)}";

            // Открыть почтовый клиент с указанным адресом
            Process.Start(new ProcessStartInfo
            {
                FileName = mailtoUrl,
                UseShellExecute = true // Важно для открытия в почтовом клиенте
            });
        }
      

        private void NumberTelephonClick(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonTelegrammClick(object sender, RoutedEventArgs e)
        {

        }

        private void RulesButtonClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RulesPage());
        }

        private void HelpPageClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HelpPage());
        }
    }
}
