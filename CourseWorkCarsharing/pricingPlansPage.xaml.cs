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
            private List<pricingPlan> allPricingPlans;

            public pricingPlansPage()
            {
                InitializeComponent();
                Loaded += PricingPlansPage_Loaded;
                DataContext = this;
            }

            private void PricingPlansPage_Loaded(object sender, RoutedEventArgs e)
            {
                UpdatePricing();
            }

            private void UpdatePricing(int? id = null)
            {
                try
                {
                    using (var context = new CarsharingBDEntities())
                    {
                        allPricingPlans = context.pricingPlans
                            .AsNoTracking() // Для только чтения
                            .ToList();

                        var currentPricing = allPricingPlans.ToList();
                        var currentPricing1 = allPricingPlans
                            .Where(p => p.Type != null && p.Type.Trim() == "Премиум")
                            .ToList();
                        var currentPricing2 = allPricingPlans
                            .Where(p => p.Type != null && p.Type.Trim() == "Электро")
                            .ToList();

                        ItemsControlRes.ItemsSource = currentPricing.OrderBy(p => p.Cost).ToList();
                        // ItemsControlRes1.ItemsSource = currentPricing1.OrderBy(p => p.Cost).ToList();
                        // ItemsControlRes2.ItemsSource = currentPricing2.OrderBy(p => p.Cost).ToList();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки тарифов: {ex.Message}");
                }
            }

            private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
            {
                UpdatePricing();
            }
        }
    }