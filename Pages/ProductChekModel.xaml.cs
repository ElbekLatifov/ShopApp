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

namespace ShopSystem.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductChekModel.xaml
    /// </summary>
    public partial class ProductChekModel : UserControl
    {
        public object Count
        {
            get
            {
                return count.Content;
            }
            set
            {
                count.Content = value;
            }
        }

        public object Id
        {
            get
            {
                return nomer.Content;
            }
            set
            {
                nomer.Content = value;
            }
        }

        public object Shtrix
        {
            get
            {
                return shtrix_kod.Content;
            }
            set
            {
                shtrix_kod.Content = value;
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
                return price.Content;
            }
            set
            {
                price.Content = value.ToString();
            }
        }

        public object Total
        {
            get
            {
                return total_price.Content;
            }
            set
            {
                total_price.Content = value.ToString();
            }
        }
        public ProductChekModel()
        {
            InitializeComponent();
        }
    }
}
