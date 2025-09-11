using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace CourseWorkCarsharing
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {

            private CarsharingBDEntities context;

            public AdminWindow()
            {
                InitializeComponent();
                context = new CarsharingBDEntities();
                LoadData();
            }

            private void LoadData()
            {
                try
                {
                    // Загружаем все данные с включением связанных сущностей
                    context.Orders.Include(o => o.User)
                        .Include(o => o.Auto)
                        .Include(o => o.pricingPlan)
                        .Include(o => o.Parking)
                        .Load();

                    context.Users.Load();
                    context.Autoes.Load();
                    context.pricingPlans.Load();
                    context.Parkings.Load();

                    OrdersGrid.ItemsSource = context.Orders.Local;
                    UsersGrid.ItemsSource = context.Users.Local;
                    CarsGrid.ItemsSource = context.Autoes.Local;
                    PricingGrid.ItemsSource = context.pricingPlans.Local;
                    ParkingGrid.ItemsSource = context.Parkings.Local;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
                }
            }

            private void SaveChanges()
            {
                try
                {
                    context.SaveChanges();
                    MessageBox.Show("Изменения сохранены успешно!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка сохранения: {ex.Message}");
                }
            }

            // Обработчики редактирования
            private void OrdersGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
            {
                SaveChanges();
            }

            private void UsersGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
            {
                SaveChanges();
            }

            private void CarsGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
            {
                SaveChanges();
            }

            private void PricingGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
            {
                SaveChanges();
            }

            private void ParkingGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
            {
                SaveChanges();
            }

            // Обработчик удаления (по клавише Delete)
            private void DataGrid_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
            {
                if (e.Key == System.Windows.Input.Key.Delete)
                {
                    var grid = sender as DataGrid;
                    if (grid != null && grid.SelectedItem != null)
                    {
                        var result = MessageBox.Show("Удалить выбранную запись?", "Подтверждение",
                            MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            try
                            {
                                if (grid.SelectedItem is Order order)
                                    context.Orders.Remove(order);
                                else if (grid.SelectedItem is User user)
                                    context.Users.Remove(user);
                                else if (grid.SelectedItem is Auto auto)
                                    context.Autoes.Remove(auto);
                                else if (grid.SelectedItem is pricingPlan pricing)
                                    context.pricingPlans.Remove(pricing);
                                else if (grid.SelectedItem is Parking parking)
                                    context.Parkings.Remove(parking);

                                SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Ошибка удаления: {ex.Message}");
                            }
                        }
                        e.Handled = true; // Отменяем стандартное удаление
                    }
                }
            }

            // Методы добавления новых записей
            private void AddUser_Click(object sender, RoutedEventArgs e)
            {
                var newUser = new User
                {
                    Login = "новый_логин",
                    Password = "пароль123",
                    Name = "Новый пользователь",
                    Email = "email@example.com",
                    Phone = "79990000000",
                    Role = "User",
                    CreatedAt = DateTime.Now
                };
                context.Users.Add(newUser);
                SaveChanges();
            }

            private void AddCar_Click(object sender, RoutedEventArgs e)
            {
                var newCar = new Auto
                {
                    Mark = "Марка",
                    Model = "Модель",
                    Year_of_release = DateTime.Now.Year,
                    Colour = "Цвет",
                    Fuel_type = "Бензин",
                    Transmission_box = "Автомат",
                    Mileage = 0,
                    Quantity = 1,
                    Status = "Доступен",
                    Date_added = DateTime.Now,
                    Date_of_last_service = DateTime.Now
                };
                context.Autoes.Add(newCar);
                SaveChanges();
            }

            private void AddPricing_Click(object sender, RoutedEventArgs e)
            {
                var newPricing = new pricingPlan
                {
                    Pricing_name = "Новый тариф",
                    Term = "1 час",
                    Cost = 1000,
                    Type = "Бюджет"
                };
                context.pricingPlans.Add(newPricing);
                SaveChanges();
            }

            private void AddParking_Click(object sender, RoutedEventArgs e)
            {
                var newParking = new Parking
                {
                    Название = "Новая парковка",
                    Расположение = "Адрес парковки"
                };
                context.Parkings.Add(newParking);
                SaveChanges();
            }

            protected override void OnClosed(EventArgs e)
            {
                context.Dispose();
                base.OnClosed(e);
            }
        }
    }