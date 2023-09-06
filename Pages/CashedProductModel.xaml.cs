using System.Windows.Controls;

namespace ShopSystem.Pages
{
    /// <summary>
    /// Логика взаимодействия для CashedProductModel.xaml
    /// </summary>
    public partial class CashedProductModel : UserControl
    {
        public object Count
        {
            get
            {
                return count_product.Content;
            }
            set
            {
                count_product.Content = value;
            }
        }

        public object ProductName
        {
            get
            {
                return product_name.Text;
            }
            set
            {
                product_name.Text = value.ToString();
            }
        }

        public object Price
        {
            get
            {
                return price_product.Text;
            }
            set
            {
                price_product.Text = value.ToString();
            }
        }

        public object Total
        {
            get
            {
                return total_price_product.Text;
            }
            set
            {
                total_price_product.Text = value.ToString();    
            }
        }
        public CashedProductModel()
        {
            InitializeComponent();
        }
    }
}
