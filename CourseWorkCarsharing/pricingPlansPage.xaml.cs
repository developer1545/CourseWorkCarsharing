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
            var currentPricing = CarsharingBDEntities.GetContext().pricingPlans.ToList();
            var currentPricing1 = CarsharingBDEntities.GetContext().pricingPlans.ToList();
            var currentPricing2 = CarsharingBDEntities.GetContext().pricingPlans.ToList();


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

            // Установка источника данных
            ItemsControlRes.ItemsSource = currentPricing.OrderBy(p => p.Cost).ToList();
            ItemsControlRes1.ItemsSource = currentPricing1.OrderBy(p => p.Cost).ToList();
            ItemsControlRes2.ItemsSource = currentPricing2.OrderBy(p => p.Cost).ToList();



        }


        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePricing();
        }


    }

}
