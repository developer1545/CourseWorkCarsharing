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
/*
            List<Car> cars = new List<Car>
    {
        new Car
        {
            Mark = "Audi",
            Model = "A4",
            Colour = "Белый",
            YearOfRelease = 2020,
            ImagePath = "C:\\Users\\antom\\source\\repos\\CourseWorkCarsharing\\CourseWorkCarsharing\\mashine\\Audi A4.png",
            Quantity = 5,
            Type = "Бюджет",
            Mileage = 15000,
            FuelType = "Бензин",
            TransmissionBox = "Автомат",
            Status = "Доступен",
            DateAdded = DateTime.Now,
            DateOfLastService = DateTime.Now.AddMonths(-6)
        },
        new Car
        {
            Mark = "BMW",
            Model = "X5",
            Colour = "Тёмно синий",
            YearOfRelease = 2019,
            ImagePath = "C:\\Users\\antom\\source\\repos\\CourseWorkCarsharing\\CourseWorkCarsharing\\mashine\\BMW X5.png",
            Quantity = 3,
            Type = "Бюджет",
            Mileage = 30000,
            FuelType = "Дизель",
            TransmissionBox = "Автомат",
            Status = "Доступен",
            DateAdded = DateTime.Now,
            DateOfLastService = DateTime.Now.AddMonths(-12)
        },
        new Car
        {
            Mark = "Mercedes",
            Model = "S-Class",
            Colour = "Синий",
            YearOfRelease = 2021,
            ImagePath = "C:\\Users\\antom\\source\\repos\\CourseWorkCarsharing\\CourseWorkCarsharing\\mashine\\Mercedec S - Class.png",
            Quantity = 4,
            Type = "Премиум",
            Mileage = 5000,
            FuelType = "Бензин",
            TransmissionBox = "Автомат",
            Status = "Доступен",
            DateAdded = DateTime.Now,
            DateOfLastService = DateTime.Now.AddMonths(-1)
        },
        new Car
        {
            Mark = "Volkswagen",
            Model = "Golf",
            Colour = "Белый",
            YearOfRelease = 2018,
            ImagePath = "C:\\Users\\antom\\source\\repos\\CourseWorkCarsharing\\CourseWorkCarsharing\\mashine\\Wolswagen Golf.png",
            Quantity = 6,
            Type = "Бюджет",
            Mileage = 25000,
            FuelType = "Бензин",
            TransmissionBox = "Механика",
            Status = "Продан",
            DateAdded = DateTime.Now,
            DateOfLastService = DateTime.Now.AddMonths(-8)
        },
        new Car
        {
            Mark = "Toyota",
            Model = "Corola",
            Colour = "Серый",
            YearOfRelease = 2020,
            ImagePath = "C:\\Users\\antom\\source\\repos\\CourseWorkCarsharing\\CourseWorkCarsharing\\mashine\\Toyota Corola.png",
            Quantity = 2,
            Type = "Бюджет",
            Mileage = 20000,
            FuelType = "Бензин",
            TransmissionBox = "Автомат",
            Status = "Доступен",
            DateAdded = DateTime.Now,
            DateOfLastService = DateTime.Now.AddMonths(-3)
        },
        new Car
        {
            Mark = "Tesla",
            Model = "Model X",
            Colour = "Белый",
            YearOfRelease = 2021,
            ImagePath = "C:\\Users\\antom\\source\\repos\\CourseWorkCarsharing\\CourseWorkCarsharing\\mashine\\Tesla Model X.png",
            Quantity = 4,
            Type = "Электро",
            Mileage = 15000,
            FuelType = "Электричество",
            TransmissionBox = "Автомат",
            Status = "Доступен",
            DateAdded = DateTime.Now,
            DateOfLastService = DateTime.Now.AddMonths(-2)
        },
         new Car
        {
            Mark = "Tesla",
            Model = "Model 3",
            Colour = "Синий",
            YearOfRelease = 2021,
            ImagePath = "C:\\Users\\antom\\source\\repos\\CourseWorkCarsharing\\CourseWorkCarsharing\\mashine\\Tesla Model 3.png",
            Quantity = 4,
            Type = "Электро",
            Mileage = 15000,
            FuelType = "Электричество",
            TransmissionBox = "Автомат",
            Status = "Доступен",
            DateAdded = DateTime.Now,
            DateOfLastService = DateTime.Now.AddMonths(-2)
        },
        new Car
        {
            Mark = "Kia",
            Model = "Sportage",
            Colour = "Серый",
            YearOfRelease = 2019,
            ImagePath = "C:\\Users\\antom\\source\\repos\\CourseWorkCarsharing\\CourseWorkCarsharing\\mashine\\Kia Sportage.png",
            Quantity = 1,
            Type = "Бюджет",
            Mileage = 18000,
            FuelType = "Бензин",
            TransmissionBox = "Автомат",
            Status = "Доступен",
            DateAdded = DateTime.Now,
            DateOfLastService = DateTime.Now.AddMonths(-5)
        },

        new Car
        {
            Mark = "Ford",
            Model = "Focus",
            Colour = "Синий",
            YearOfRelease = 2018,
            ImagePath = "C:\\Users\\antom\\source\\repos\\CourseWorkCarsharing\\CourseWorkCarsharing\\mashine\\Ford Focus.png",
            Quantity = 5,
            Type = "Бюджет",
            Mileage = 30000,
            FuelType = "Бензин",
            TransmissionBox = "Автомат",
            Status = "Доступен",
            DateAdded = DateTime.Now,
            DateOfLastService = DateTime.Now.AddMonths(-10)
        },

        new Car
        {
            Mark = "Mercedec",
            Model = "CLE Cabriolet",
            Colour = "Красный",
            YearOfRelease = 2021,
            ImagePath = "C:\\Users\\antom\\source\\repos\\CourseWorkCarsharing\\CourseWorkCarsharing\\mashine\\Mercedec CLE Cabriolet.png",
            Quantity = 3,
            Type = "Премиум",
            Mileage = 5000,
            FuelType = "Бензин",
            TransmissionBox = "Автомат",
            Status = "Доступен",
            DateAdded = DateTime.Now,
            DateOfLastService = DateTime.Now.AddMonths(-1)
        },
        new Car
        {
            Mark = "Mercedes",
            Model = "CLE Coupe",
            Colour = "Коричневый",
            YearOfRelease = 2020,
            ImagePath = "C:\\Users\\antom\\source\\repos\\CourseWorkCarsharing\\CourseWorkCarsharing\\mashine\\Mercedec CLE Coupe.png",
            Quantity = 5,
            Type = "Премиум",
            Mileage = 15000,
            FuelType = "Бензин",
            TransmissionBox = "Автомат",
            Status = "Доступен",
            DateAdded = DateTime.Now,
            DateOfLastService = DateTime.Now.AddMonths(-3)
        },
        new Car
        {
            Mark = "Volvo",
            Model = "XC60",
            Colour = "Синий",
            YearOfRelease = 2021,
            ImagePath = "C:\\Users\\antom\\source\\repos\\CourseWorkCarsharing\\CourseWorkCarsharing\\mashine\\Volvo XC 60.png",
            Quantity = 3,
            Type = "Премиум",
            Mileage = 7000,
            FuelType = "Бензин",
            TransmissionBox = "Автомат",
            Status = "Доступен",
            DateAdded = DateTime.Now,
            DateOfLastService = DateTime.Now.AddMonths(-1)
        },
        new Car
        {
            Mark = "Lexus",
            Model = "NX",
            Colour = "Белый",
            YearOfRelease = 2020,
            ImagePath = "C:\\Users\\antom\\source\\repos\\CourseWorkCarsharing\\CourseWorkCarsharing\\mashine\\Lexus NX.png",
            Quantity = 2,
            Type = "Бюджет",
            Mileage = 8000,
            FuelType = "Гибрид",
            TransmissionBox = "Автомат",
            Status = "Доступен",
            DateAdded = DateTime.Now,
            DateOfLastService = DateTime.Now.AddMonths(-2)
        },
        new Car
        {
            Mark = "Fiat",
            Model = "500",
            Colour = "Белый",
            YearOfRelease = 2019,
            ImagePath = "C:\\Users\\antom\\source\\repos\\CourseWorkCarsharing\\CourseWorkCarsharing\\mashine\\Fiat 500.png",
            Quantity = 4,
            Type = "Особый",
            Mileage = 15000,
            FuelType = "Бензин",
            TransmissionBox = "Механика",
            Status = "Доступен",
            DateAdded = DateTime.Now,
            DateOfLastService = DateTime.Now.AddMonths(-4)
        }
    };

            UploadCars(cars);
*/
           
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
            MainFrame.Navigate(new OrderPage());
        }
    }
}
