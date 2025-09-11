using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
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
    public partial class OrderPage : Page
    {
        public string selectedPricingName = "";
        public string selectedCarInfo = "";
        public string selectedParkingName = "";

        private Border _selectedCarBorder = null;
        private object _selectedPricingItem = null;
        private Parking _selectedParking = null;
        private Border _selectedParkingBorder = null;

        private List<Auto> allAutos = new List<Auto>();
        private List<pricingPlan> allPricingPlans = new List<pricingPlan>();
        private List<Parking> allParkings = new List<Parking>();

        public OrderPage()
        {
            InitializeComponent();
            Loaded += OrderPage_Loaded;
        }

        private void OrderPage_Loaded(object sender, RoutedEventArgs e)
        {
            CheckAuthentication();
            LoadData();
        }

        private void CheckAuthentication()
        {
            // Проверяем, авторизован ли пользователь
            if (!IsUserLoggedIn())
            {
                ShowLoginPrompt();
            }
        }

        private bool IsUserLoggedIn()
        {
            return Application.Current.Properties.Contains("IsLoggedIn") &&
                   Application.Current.Properties["IsLoggedIn"] is bool isLoggedIn &&
                   isLoggedIn;
        }

        private void ShowLoginPrompt()
        {
            var result = MessageBox.Show("Для оформления заказа необходимо войти в систему. Хотите перейти на страницу входа?",
                "Требуется авторизация",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Переходим на страницу входа
                NavigationService?.Navigate(new LoginPage());
            }
        }

        private void LoadData()
        {
            try
            {
                using (var context = new CarsharingBDEntities())
                {
                    // Загружаем все данные
                    allAutos = context.Autoes
                        .AsNoTracking()
                        .ToList();

                    allPricingPlans = context.pricingPlans
                        .AsNoTracking()
                        .ToList();

                    allParkings = context.Parkings
                        .AsNoTracking()
                        .ToList();

                    // Обновляем интерфейс
                    UpdatePricing();
                    UpdatePark();
                    UpdateParking();
                    UpdateOrderSummary();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        private void UpdatePark()
        {
            ItemsControlRes.ItemsSource = allAutos;
        }

        private void UpdateParking()
        {
            ItemsControlParking.ItemsSource = allParkings;
        }

        private void UpdatePricing()
        {
            ItemsControl.ItemsSource = allPricingPlans.OrderBy(p => p.Cost).ToList();
        }

        private void CarBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsUserLoggedIn())
            {
                ShowLoginPrompt();
                return;
            }

            var border = sender as Border;
            if (border != null)
            {
                _selectedCarBorder = border;
                var selectedCar = border.DataContext as Auto;
                if (selectedCar != null)
                {
                    selectedCarInfo = $"{selectedCar.Mark} {selectedCar.Model}";
                    UpdateCarOpacity();
                    UpdateOrderSummary();
                }
            }
        }

        private void UpdateCarOpacity()
        {
            foreach (var item in ItemsControlRes.Items)
            {
                var container = ItemsControlRes.ItemContainerGenerator.ContainerFromItem(item) as ContentPresenter;
                if (container == null) continue;

                var border = FindVisualChild<Border>(container);
                if (border != null)
                {
                    border.Opacity = (border == _selectedCarBorder) ? 1.0 : 0.3;
                }
            }
        }

        private void CChooseClickDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsUserLoggedIn())
            {
                ShowLoginPrompt();
                return;
            }

            var border = sender as Border;
            if (border != null)
            {
                var selectedPricing = border.DataContext as pricingPlan;
                if (selectedPricing != null)
                {
                    _selectedPricingItem = selectedPricing;
                    selectedPricingName = selectedPricing.Pricing_name;
                    UpdatePricingOpacity();
                    UpdateOrderSummary();
                }
            }
        }

        private void UpdatePricingOpacity()
        {
            foreach (var item in ItemsControl.Items)
            {
                var container = ItemsControl.ItemContainerGenerator.ContainerFromItem(item) as ContentPresenter;
                if (container == null) continue;

                var border = FindVisualChild<Border>(container);
                if (border != null)
                {
                    var dataContext = border.DataContext;
                    border.Opacity = (dataContext == _selectedPricingItem) ? 1.0 : 0.3;
                }
            }
        }

        private void ParkingBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsUserLoggedIn())
            {
                ShowLoginPrompt();
                return;
            }

            var border = sender as Border;
            if (border != null)
            {
                _selectedParkingBorder = border;
                _selectedParking = border.DataContext as Parking;
                if (_selectedParking != null)
                {
                    selectedParkingName = _selectedParking.Название;
                    UpdateParkingOpacity();
                    UpdateOrderSummary();
                }
            }
        }

        private void UpdateParkingOpacity()
        {
            foreach (var item in ItemsControlParking.Items)
            {
                var container = ItemsControlParking.ItemContainerGenerator.ContainerFromItem(item) as ContentPresenter;
                if (container == null) continue;

                var border = FindVisualChild<Border>(container);
                if (border != null)
                {
                    var dataContext = border.DataContext;
                    border.Opacity = (dataContext == _selectedParking) ? 1.0 : 0.3;
                }
            }
        }

        private void UpdateOrderSummary()
        {
            OrderText.Text = selectedPricingName;
            AutoText.Text = selectedCarInfo;
            ParkAutoText.Text = selectedParkingName;
            OrderText1.Text = selectedPricingName;
            AutoText1.Text = selectedCarInfo;
            ParkAutoText1.Text = selectedParkingName;
        }

        // Вспомогательные методы для поиска элементов
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

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filter = TBoxSearch.Text.ToLower();
            if (string.IsNullOrWhiteSpace(filter))
            {
                ItemsControlRes.ItemsSource = allAutos;
            }
            else
            {
                var filteredCars = allAutos
                    .Where(a => (a.Mark != null && a.Mark.ToLower().Contains(filter)) ||
                               (a.Model != null && a.Model.ToLower().Contains(filter)))
                    .ToList();
                ItemsControlRes.ItemsSource = filteredCars;
            }
        }

        private void TBox__Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filter = TBoxSearchT.Text.ToLower();
            if (string.IsNullOrWhiteSpace(filter))
            {
                ItemsControl.ItemsSource = allPricingPlans.OrderBy(p => p.Cost);
            }
            else
            {
                var filteredPricing = allPricingPlans
                    .Where(p => p.Pricing_name != null && p.Pricing_name.ToLower().Contains(filter))
                    .OrderBy(p => p.Cost)
                    .ToList();
                ItemsControl.ItemsSource = filteredPricing;
            }
        }

        private void ButtonPlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            if (!IsUserLoggedIn())
            {
                ShowLoginPrompt();
                return;
            }

            if (string.IsNullOrEmpty(selectedCarInfo) || string.IsNullOrEmpty(selectedPricingName) || string.IsNullOrEmpty(selectedParkingName))
            {
                MessageBox.Show("Пожалуйста, выберите автомобиль, тариф и парковку.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (var context = new CarsharingBDEntities())
                {
                    string userName = Application.Current.Properties["User Name"] as string;
                    var user = context.Users.FirstOrDefault(u => u.Name == userName);
                    if (user == null)
                    {
                        MessageBox.Show("Пользователь не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Получаем выбранные объекты
                    var car = allAutos.FirstOrDefault(a => (a.Mark + " " + a.Model) == selectedCarInfo);
                    var pricing = allPricingPlans.FirstOrDefault(p => p.Pricing_name == selectedPricingName);

                    if (car == null || pricing == null || _selectedParking == null)
                    {
                        MessageBox.Show("Ошибка при выборе данных для заказа.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    var order = new Order
                    {
                        UserID = user.UserID,
                        AutoID = car.ID,
                        PricingPlanID = pricing.ID,
                        ParkingID = _selectedParking.ID_Парковки,
                        OrderDate = DateTime.Now,
                    };

                    context.Orders.Add(order);

                    // Уменьшаем количество доступных автомобилей
                    var carInDb = context.Autoes.Find(car.ID);
                    if (carInDb != null)
                    {
                        carInDb.Quantity--;
                        if (carInDb.Quantity <= 0)
                        {
                            carInDb.Status = "Не доступен";
                        }
                    }

                    context.SaveChanges();

                    MessageBox.Show("Заказ успешно оформлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Обновляем данные
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при оформлении заказа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Метод для обновления состояния при возвращении на страницу
        public void RefreshPage()
        {
            CheckAuthentication();
            LoadData();
        }
    }
}