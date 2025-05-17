using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using static System.Net.Mime.MediaTypeNames;

namespace CourseWorkCarsharing
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public class Car
    {
        public string Mark { get; set; }
        public string Model { get; set; }
        public string Colour { get; set; }
        public int YearOfRelease { get; set; }
        public string ImagePath { get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; }
        public int Mileage { get; set; }
        public string FuelType { get; set; }
        public string TransmissionBox { get; set; }
        public string Status { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateOfLastService { get; set; }
    }
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new MainPage());
            Manager.MainFrame = MainFrame;
            this.StateChanged += MainWindow_StateChanged;
           
         

            

        }


        private void MessageWindow_Closed()
        {
            // После закрытия окна навигируем на страницу входа
            MainFrame.Navigate(new LoginPage());
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
        //Загрузка изображений в БД
        
        public static void UploadCars(List<Car> cars)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CarsharingBD;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (Car car in cars)
                {
                    byte[] imageData = File.ReadAllBytes(car.ImagePath);

                    using (SqlCommand command = new SqlCommand("INSERT INTO Auto (Mark, Model, Colour, Year_of_release, Image, Quantity, Type, Mileage, Fuel_type, Transmission_box, Status, Date_added, Date_of_last_service) VALUES (@Mark, @Model, @Colour, @YearOfRelease, @Image, @Quantity, @Type, @Mileage, @FuelType, @TransmissionBox, @Status, @DateAdded, @Date_of_last_service)", connection))
                    {
                        command.Parameters.AddWithValue("@Mark", car.Mark);
                        command.Parameters.AddWithValue("@Model", car.Model);
                        command.Parameters.AddWithValue("@Colour", car.Colour);
                        command.Parameters.AddWithValue("@YearOfRelease", car.YearOfRelease);
                        command.Parameters.AddWithValue("@Image", imageData);
                        command.Parameters.AddWithValue("@Quantity", car.Quantity);
                        command.Parameters.AddWithValue("@Type", car.Type);
                        command.Parameters.AddWithValue("@Mileage", car.Mileage);
                        command.Parameters.AddWithValue("@FuelType", car.FuelType);
                        command.Parameters.AddWithValue("@TransmissionBox", car.TransmissionBox);
                        command.Parameters.AddWithValue("@Status", car.Status);
                        command.Parameters.AddWithValue("@DateAdded", car.DateAdded);
                        command.Parameters.AddWithValue("@Date_Of_Last_Service", car.DateOfLastService);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        private void ButtonAccountEnterClikc(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new LoginPage());
        }

        private void OrderButtonClick(object sender, RoutedEventArgs e)
        {
            Window window = new MessageWindow();
            if (MainFrame.Content is OrderPage)
            {
                window.Closed += (s, args) => MessageWindow_Closed();
                window.Show();
         
            }
            
            else
            {
                // На другой странице — перейти на OrderPage
                MainFrame.Navigate(new OrderPage());
            }
           
        
        }

    }
}
