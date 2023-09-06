using Mapster;
using ShopSystem.Context;
using ShopSystem.Models;
using System;
using System.Linq;
using System.Windows.Controls;

namespace ShopSystem.Pages
{
    /// <summary>
    /// Логика взаимодействия для TabProduct.xaml
    /// </summary>
    public partial class TabProduct : UserControl
    {
        public object ProductId
        {
            get
            {
                return product_id.Content;
            }
            set
            {
                product_id.Content = value;
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

        Kassa Kassa { get; set; }
        public TabProduct(Kassa kassa)
        {
            InitializeComponent();
            Kassa = kassa;
        }

        private void UserControl_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var db = new AppDbContext();
            var product = db.Products.First(p => p.Id == (Guid)ProductId);
            var cashedproduct = new CashedProduct()
            {
                Id = product.Id,
                Count = product.Count,
                Categoryid = product.Categoryid,
                Barcode = product.Barcode,
                Created_time = product.Created_time,
                PriceCome = product.PriceCome,
                PriceGo = product.PriceGo,
                ShopId = product.ShopId,
                Title = product.Title,
                CashedTime = DateTime.UtcNow,
                Totalcount = 1
            };

            if(!db.CashedProducts.Any(p=>p.Id == (Guid)ProductId))
            {
                db.CashedProducts.Add(cashedproduct);
                db.SaveChanges();
                Kassa.LoadCashedProducts();
            }
            else
            {
                var cashedproductThan = db.CashedProducts.First(p => p.Id == (Guid)ProductId);
                cashedproductThan.Totalcount += 1;
                db.CashedProducts.Update(cashedproductThan);
                db.SaveChanges();
                Kassa.LoadCashedProducts();
            }
            
        }
    }
}
