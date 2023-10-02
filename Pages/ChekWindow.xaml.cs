using ShopSystem.Context;
using ShopSystem.Models;
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
using System.Windows.Shapes;

namespace ShopSystem.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChekWindow.xaml
    /// </summary>
    public partial class ChekWindow : Window
    {
        Kassa Kassa { get; set; }
        public ChekWindow(MainWindow main, Kassa kassa)
        {
            InitializeComponent();
            Owner = main;
            Kassa = kassa;  
            Load();
        }

        public void Load()
        {
            int i = 1;
            checked_products.Items.Clear();
            var db = new AppDbContext();
            var products = db.CashedProducts.OrderBy(p => p.CashedTime).ToList();
            foreach (var product in products)
            {
                var check = new ProductChekModel();
                check.Height = 120; check.Width = 280;
                check.Price = product.PriceGo;
                check.Count = product.Totalcount;
                check.Total = product.Totalcount * (int)product.PriceGo;
                check.Shtrix = product.Barcode!;
                check.ProductName = product.Title;
                check.Id = i;
                checked_products.Items.Add(check);
                i++;
            }
        }
    }
}
