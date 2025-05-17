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

        private string selectedPricingName = "";
        private string selectedCarInfo = "";
        private string selectedParkingName = "";

        public OrderPage()
        {
            InitializeComponent();
            UpdatePricing();
            var allTypes = CarsharingBDEntities.GetContext().Autoes.ToList();
            UpdatePark();
            DataContext = this;
            UpdateParking();
            UpdateCarOpacity();

        }
        private void CarBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _selectedCarBorder = sender as Border;
            UpdateCarOpacity();

            // Обновляем выбранный авто
            if (_selectedCarBorder != null)
            {
                var container = FindParent<ContentPresenter>(_selectedCarBorder);
                if (container != null)
                {
                    var selectedCar = (Auto)container.DataContext;
                    selectedCarInfo = $"{selectedCar.Mark} {selectedCar.Model}";
                    UpdateOrderSummary();
                }
            }
        }
        // Для хранения выбранного автомобиля
        private Border _selectedCarBorder = null;

       
  
        private void UpdateOrderSummary()
        {
            OrderText.Text = selectedPricingName;
            AutoText.Text = selectedCarInfo;
            ParkAutoText.Text = selectedParkingName;
            OrderText1.Text = selectedPricingName;
            AutoText1.Text = selectedCarInfo;
            ParkAutoText1.Text = selectedParkingName;
        }

        private void UpdateCarOpacity()
        {
            foreach (var item in ItemsControlRes.Items)
            {
                var container = ItemsControlRes.ItemContainerGenerator.ContainerFromItem(item) as ContentPresenter;
                if (container == null) continue;

                var border = FindVisualChild<Border>(container);
                if (border == null) continue;

                if (border == _selectedCarBorder)
                {
                    border.Opacity = 1.0;
                }
                else
                {
                    border.Opacity = 0.3;
                }
            }
        }


        private void UpdatePark(int? id = null)
        {
            var currentAuto = CarsharingBDEntities.GetContext().Autoes.ToList();


            // Установка источника данных
            ItemsControlRes.ItemsSource = currentAuto.ToList();
            //ItemsControlRes.ItemsSource = budgetCars.OrderBy(p => p.Quantity).ToList();
            //ItemsControlRes1.ItemsSource = premiumCars.OrderBy(p => p.Quantity).ToList();
            //ItemsControlRes2.ItemsSource = electricCars.OrderBy(p => p.Quantity).ToList();
            //ItemsControlRes3.ItemsSource = specialCars.OrderBy(p => p.Quantity).ToList();
        }
        private void UpdateParking()
        {

            ItemsControlParking.ItemsSource = CarsharingBDEntities.GetContext().Parkings.ToList();


        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePark();
        }




        private void UpdatePricing(int? id = null)
        {
            var currentPricing = CarsharingBDEntities.GetContext().pricingPlans.ToList();
            ItemsControl.ItemsSource = currentPricing.OrderBy(p => p.Cost).ToList();
        }



        private object _selectedPricingItem; // или конкретный тип, например pricingPlan


        // Объявляем поле для хранения выбранного тарифа
        private Border _selectedBorder = null;



        private void CChooseClickDown(object sender, MouseButtonEventArgs e)
        {
            Border btn = sender as Border;
            if (btn == null) return;

            var selectedPricing = (pricingPlan)btn.DataContext;
            _selectedPricingItem = selectedPricing; // сохраняем объект данных

            selectedPricingName = selectedPricing.Pricing_name;

            UpdateOpacity();
            UpdateOrderSummary();
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
            foreach (var item in ItemsControl.Items)
            {
                var container = ItemsControl.ItemContainerGenerator.ContainerFromItem(item) as ContentPresenter;
                if (container == null) continue;

                var border = FindVisualChild<Border>(container);
                if (border == null) continue;

                var dataContext = border.DataContext;

                if (dataContext == _selectedPricingItem)
                {
                    border.Opacity = 1.0;
                }
                else
                {
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

        private void HighlightPointByTag(string targetTag)
        {
            // Снимаем подсветку со всех точек
            foreach (var child in MapCanvas.Children)
            {
                if (child is Ellipse ellipse)
                {
                    ellipse.Fill = Brushes.LightGray;
                    ellipse.StrokeThickness = 1;
                }
            }

            // Находим точку по Tag
            foreach (var child in MapCanvas.Children)
            {
                if (child is Ellipse ellipse && ellipse.Tag.ToString() == targetTag)
                {
                    // Подсвечиваем выбранную точку
                    ellipse.Fill = Brushes.Blue;
                    ellipse.StrokeThickness = 2;
                }
            }
        }

        // В коде за кулисами
        // Задайте относительные координаты для каждой точки
        private double point1RelX = 0.51; // 40% ширины изображения
        private double point1RelY = 0.42; // 50% высоты изображения

        private double point2RelX = 0.22;
        private double point2RelY = 0.26;

        private double point3RelX = 0.2;
        private double point3RelY = 0.4;

        private double point4RelX = 0.47;
        private double point4RelY = 0.60;

        private double point5RelX = 0.39;
        private double point5RelY = 0.67;
        // В обработчике SizeChanged
        private void MapImage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdatePointsPosition();
        }

        private void UpdatePointsPosition()
        {

            double width = MapImage.ActualWidth;
            double height = MapImage.ActualHeight;

            Canvas.SetLeft(Point1, point1RelX * width);
            Canvas.SetTop(Point1, point1RelY * height);

            Canvas.SetLeft(Point2, point2RelX * width);
            Canvas.SetTop(Point2, point2RelY * height);

            Canvas.SetLeft(Point3, point3RelX * width);
            Canvas.SetTop(Point3, point3RelY * height);

            Canvas.SetLeft(Point4, point4RelX * width);
            Canvas.SetTop(Point4, point4RelY * height);

            Canvas.SetLeft(Point5, point5RelX * width);
            Canvas.SetTop(Point5, point5RelY * height);
        }


        private void MapBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;
            if (border != null && border.Tag != null)
            {
                string targetTag = border.Tag.ToString();
                HighlightPointByTag(targetTag);
                var parking = border.DataContext as Parking;
                if (parking != null)
                {
                    selectedParkingName = $"{parking.Название} - {parking.Расположение} ";
                    UpdateOrderSummary();
                }
            }
        }

     
    }
}

