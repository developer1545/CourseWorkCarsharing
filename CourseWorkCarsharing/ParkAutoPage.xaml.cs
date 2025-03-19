using System;
using System.Collections.Generic;
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
            var allTypes = CarsharingBDEntities1.GetContext().Autoes.ToList();
            UpdatePark();
            DataContext = this;
        }
        private void UpdatePark(int? id = null)
        {
            var currentPricing = CarsharingBDEntities1.GetContext().Autoes.ToList();
            var currentPricing1 = CarsharingBDEntities1.GetContext().Autoes.ToList();
            var currentPricing2 = CarsharingBDEntities1.GetContext().Autoes.ToList();
            var currentPricing3 = CarsharingBDEntities1.GetContext().Autoes.ToList();


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
            currentPricing = currentPricing.Where(p => p.Type.Trim() == "Бюджет").ToList();
            currentPricing1 = currentPricing1.Where(p => p.Type.Trim() == "Премиум").ToList();
            currentPricing2 = currentPricing2.Where(p => p.Type.Trim() == "Электро").ToList();
            currentPricing3 = currentPricing3.Where(p => p.Type.Trim() == "Особый").ToList();

            // Установка источника данных
            ItemsControlRes.ItemsSource = currentPricing.OrderBy(p => p.Quantity).ToList();
            ItemsControlRes1.ItemsSource = currentPricing1.OrderBy(p => p.Quantity).ToList();
            ItemsControlRes2.ItemsSource = currentPricing2.OrderBy(p => p.Quantity).ToList();
            ItemsControlRes3.ItemsSource = currentPricing3.OrderBy(p => p.Quantity).ToList();

        }
        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePark();
        }
    }
}
