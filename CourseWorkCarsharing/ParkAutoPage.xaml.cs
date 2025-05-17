using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
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
    /// Логика взаимодействия для ParkAutoPage.xaml
    /// </summary>
    public partial class ParkAutoPage : Page
    {
        public ParkAutoPage()
        {
            InitializeComponent();
            var allTypes = CarsharingBDEntities.GetContext().Autoes.ToList();
            UpdatePark();
            DataContext = this;
        }
        /*private BitmapImage ConvertByteArrayToImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
            {
                return null; // или вернуть изображение по умолчанию
            }

            using (var stream = new MemoryStream(imageData))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
                image.Freeze(); // Замораживаем объект для использования в других потоках
                return image;
            }
        }
        */
        private void UpdatePark(int? id = null)
        {
            var currentPricing = CarsharingBDEntities.GetContext().Autoes.ToList();

            // Фильтрация по типу
            var budgetCars = currentPricing.Where(p => p.Type.Trim() == "Бюджет").ToList();
            var premiumCars = currentPricing.Where(p => p.Type.Trim() == "Премиум").ToList();
            var electricCars = currentPricing.Where(p => p.Type.Trim() == "Электро").ToList();
            var specialCars = currentPricing.Where(p => p.Type.Trim() == "Особый").ToList();

           /* // Преобразование изображений
            foreach (var car in budgetCars)
            {
                car.ImageSource = ConvertByteArrayToImage(car.ImageData);
            }

            foreach (var car in premiumCars)
            {
                car.ImageSource = ConvertByteArrayToImage(car.ImageData);
            }

            foreach (var car in electricCars)
            {
                car.ImageSource = ConvertByteArrayToImage(car.ImageData);
            }

            foreach (var car in specialCars)
            {
                car.ImageSource = ConvertByteArrayToImage(car.ImageData);
            }
           */
            // Установка источника данных
            ItemsControlRes.ItemsSource = budgetCars.OrderBy(p => p.Quantity).ToList();
            ItemsControlRes1.ItemsSource = premiumCars.OrderBy(p => p.Quantity).ToList();
            ItemsControlRes2.ItemsSource = electricCars.OrderBy(p => p.Quantity).ToList();
            ItemsControlRes3.ItemsSource = specialCars.OrderBy(p => p.Quantity).ToList();
        }


        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePark();
        }
        public class Autoes
        {
            // Ваши существующие свойства
            public byte[] ImageData { get; set; }

            // Новое свойство для привязки изображения
            [NotMapped] // Не сохранять в БД
            public BitmapImage ImageSource { get; set; }
        }

    }
}
