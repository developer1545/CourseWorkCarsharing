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
    /// Логика взаимодействия для pricingPlansPage.xaml
    /// </summary>
    public partial class pricingPlansPage : Page
    {
        public pricingPlansPage()
        {
            InitializeComponent();
            var allTypes = CarsharingBDEntities.GetContext().pricingPlans.ToList();
            UpdatePricing();
          
            DataContext = this;
        }
        private void UpdatePricing(int? id = null)
        {
            var currentTours = CarsharingBDEntities.GetContext().pricingPlans.ToList();

            // Фильтрация по Id, если он предоставлен
            if (id.HasValue)
            {
                var tourById = currentTours.FirstOrDefault(p => p.Pricing_id == id.Value);
                if (tourById != null)
                {
                    currentTours = new List<pricingPlan> { tourById }; // Создаем новый список с одним элементом
                }
                else
                {
                    currentTours = new List<pricingPlan>(); // Если по Id ничего не найдено, возвращаем пустой список
                }
            }
            else
            {
                // Фильтрация по имени
                if (!string.IsNullOrEmpty(TBoxSearch.Text))
                {
                    currentTours = currentTours
                        .Where(p => p.Pricing_name.ToLower().Contains(TBoxSearch.Text.ToLower()))
                        .ToList();
                }
            }

            // Установка источника данных
            ItemsControlRes.ItemsSource = currentTours.OrderBy(p => p.Cost).ToList();
            
        }


        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePricing();
        }

      
    }

}
