using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
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
        private List<Auto> allAutos;

        public ParkAutoPage()
        {
            InitializeComponent();
            Loaded += ParkAutoPage_Loaded;
        }

            private void CheckAndLoadDefaultImages()
            {
                using (var context = new CarsharingBDEntities())
                {
                    var carsWithoutImages = context.Autoes.Where(a => a.Image == null).ToList();

                    if (carsWithoutImages.Any())
                    {
                        byte[] defaultImage = LoadDefaultImage();
                        foreach (var car in carsWithoutImages)
                        {
                            car.Image = defaultImage;
                        }
                        context.SaveChanges();
                    }
                }
            }

            private byte[] LoadDefaultImage()
            {
                try
                {
                    var uri = new Uri("D:\\respos\\CourseWorkCarsharing\\CourseWorkCarsharing\\mashine\\AutoDefault.png");
                    var bitmap = new BitmapImage(uri);

                    using (MemoryStream stream = new MemoryStream())
                    {
                        PngBitmapEncoder encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(bitmap));
                        encoder.Save(stream);
                        return stream.ToArray();
                    }
                }
                catch
                {
                    // Возвращаем пустой массив если изображение не найдено
                    return new byte[0];
                }
            }

            private void ParkAutoPage_Loaded(object sender, RoutedEventArgs e)
            {
                LoadAutos();
            }

            private void LoadAutos()
            {
                try
                {
                    using (var context = new CarsharingBDEntities())
                    {
                        allAutos = context.Autoes
                            .Where(a => a.Quantity > 0)
                            .AsNoTracking() // Для только чтения
                            .ToList();

                        var budgetCars = allAutos
                            .ToList();

                        ItemsControlRes.ItemsSource = budgetCars;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
                }
            }

            private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
            {
                var filter = TBoxSearch.Text.ToLower();

                if (string.IsNullOrWhiteSpace(filter))
                {
                    var budgetCars = allAutos
                        .Where(a => a.Type != null && a.Type.Trim() == "Бюджет")
                        .ToList();
                    ItemsControlRes.ItemsSource = budgetCars;
                }
                else
                {
                    var filteredCars = allAutos
                        .Where(a => a.Type != null && a.Type.Trim() == "Бюджет" &&
                                   (a.Mark != null && a.Mark.ToLower().Contains(filter) ||
                                    a.Model != null && a.Model.ToLower().Contains(filter)))
                        .ToList();

                    ItemsControlRes.ItemsSource = filteredCars;
                }
            }

            private void SelectCarButton_Click(object sender, RoutedEventArgs e)
            {
                var button = sender as Button;
                if (button != null && button.Tag is int carId)
                {
                    var selectedCar = allAutos.FirstOrDefault(a => a.ID == carId);
                    if (selectedCar != null)
                    {
                        MessageBox.Show($"Выбран автомобиль: {selectedCar.Mark} {selectedCar.Model}");
                    }
                }
            }

            private void RefreshButton_Click(object sender, RoutedEventArgs e)
            {
                LoadAutos();
                TBoxSearch.Clear();
            }
        }
    }