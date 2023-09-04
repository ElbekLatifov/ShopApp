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
    /// Логика взаимодействия для ProductControl.xaml
    /// </summary>
    public partial class ProductControl : UserControl
    {
        public object Price
        {
            get
            {
                return productPrice_lbl.Content;
            }
            set
            {
                productPrice_lbl.Content = value;
            }
        }
        public object Name
        {
            get
            {
                return productName_lbl.Text;
            }
            set
            {
                productName_lbl.Text = value.ToString();
            }
        }
        
        public object Id
        {
            get
            {
                return id.Content;
            }
            set
            {
                id.Content = value;
            }
        }
        public ProductControl()
        {
            InitializeComponent();
        }
    }
}
