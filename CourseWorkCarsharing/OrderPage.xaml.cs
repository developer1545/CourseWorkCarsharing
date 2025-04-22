using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        public OrderPage()
        {
            InitializeComponent();
            UpdatePricing();
            var allTypes = CarsharingBDEntities1.GetContext().Autoes.ToList();
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
            var currentPricing = CarsharingBDEntities1.GetContext().Autoes.ToList();

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

    

        private void UpdatePricing(int? id = null)
        {
            var currentPricing = CarsharingBDEntities1.GetContext().pricingPlans.ToList();
            var currentPricing1 = CarsharingBDEntities1.GetContext().pricingPlans.ToList();
            var currentPricing2 = CarsharingBDEntities1.GetContext().pricingPlans.ToList();


            // Фильтрация по Id, если он предоставлен

            //var typeById = currentTours.FirstOrDefault(p => p.Type == "Бюджет    ");
            /*if (typeById != null)
            {
                currentTours = new List<pricingPlan> { typeById }; // Создаем новый список с одним элементом
            }
            else
            {
                currentTours = new List<pricingPlan>(); // Если по Id ничего не найдено, возвращаем пустой список
            }

      */
            /*currentPricing = currentPricing.Where(p => p.Type.Trim() == "Бюджет").ToList();
            currentPricing1 = currentPricing1.Where(p => p.Type.Trim() == "Премиум").ToList();
            currentPricing2 = currentPricing2.Where(p => p.Type.Trim() == "Электро").ToList();
            */
            // Установка источника данных
            ItemsControl.ItemsSource = currentPricing.OrderBy(p => p.Cost).ToList();
            //ItemsControlRes1.ItemsSource = currentPricing1.OrderBy(p => p.Cost).ToList();
            //ItemsControlRes2.ItemsSource = currentPricing2.OrderBy(p => p.Cost).ToList();



        }


      

        
        // Объявляем поле для хранения выбранного тарифа
        private Border _selectedBorder = null;

        private void ButtonChooseClick(object sender, RoutedEventArgs e)
        {
            // Получаем кнопку, которая была нажата
            Button btn = sender as Button;
            if (btn == null) return;

            // Получаем родительский Grid или Border, где находится тариф
            // В вашем случае это Border, внутри которого Button
            var parentBorder = FindParent<Border>(btn);
            if (parentBorder == null) return;

            // Сохраняем выбранный Border
            _selectedBorder = parentBorder;

            // Обновляем прозрачность всех тарифов
            UpdateOpacity();
        }

        // Метод для поиска родителя нужного типа
        public static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null) return null;

            T parent = parentObject as T;
            if (parent != null)
                return parent;
            else
                return FindParent<T>(parentObject);
        }

        // Метод обновления прозрачности
        private void UpdateOpacity()
        {
            // Обход всех элементов ItemsControl
            foreach (var item in ItemsControl.Items)
            {
                // Получаем контейнер
                var container = ItemsControl.ItemContainerGenerator.ContainerFromItem(item) as ContentPresenter;
                if (container == null) continue;

                // Находим Border внутри DataTemplate
                var border = FindVisualChild<Border>(container);
                if (border == null) continue;

                // Если это выбранный Border, делаем его полностью непрозрачным
                if (border == _selectedBorder)
                {
                    border.Opacity = 1.0;
                }
                else
                {
                    // Остальные делают прозрачнее, например 0.3
                    border.Opacity = 0.3;
                }
            }
        }

        // Обобщённый метод поиска VisualChild нужного типа
        public static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T tChild)
                    return tChild;
                var result = FindVisualChild<T>(child);
                if (result != null)
                    return result;
            }
            return null;
        }

        private void TBox__Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePricing();
        }
    }
}

