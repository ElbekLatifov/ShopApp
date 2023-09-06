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
    /// Логика взаимодействия для ProductsWindow.xaml
    /// </summary>
    public partial class ProductsWindow : Window
    {
        WorkerPage workerPage;
        Kassa Kassa;
        string Header;
        public ProductsWindow(MainWindow main, WorkerPage worker, Kassa kassa, string header)
        {
            InitializeComponent();
            workerPage = worker;
            Owner = main;
            Kassa = kassa;
            Header = header;
            Load();
        }

        public void Load()
        {
            using (var db = new AppDbContext())
            {
                var items = new List<DataModel>();
                var products = db.Products.Where(p => p.ShopId == workerPage.ShopId).Where(p => p.TabName == null).ToList();
                int i = 1;
                if (products.Count > 0)
                {
                    foreach (var product in products)
                    {
                        var subcategory = db.Subcategories.FirstOrDefault(p => p.Id == product.Categoryid);
                        if (subcategory != null)
                        {
                            var category = db.Categories.FirstOrDefault(p => p.Id == subcategory.ParentId);
                            var shop = db.Shops.FirstOrDefault(p => p.Id == product.ShopId);

                            var item = new DataModel();
                            item.Номер = i;
                            item.Продукт = product.Title;
                            item.Подкатегория = subcategory.Title;
                            item.Категория = category!.Title;
                            item.Прибывшая = product.PriceCome;
                            item.Штрихкод = product.Barcode!;
                            item.Текущая = product.PriceGo;
                            item.Магазин = shop!.Name;
                            item.Количство = product.Count;
                            items.Add(item);
                            i++;
                        }
                    }
                    storage_data.ItemsSource = items;
                }
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            var index = storage_data.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Выберите продукт");
                return;
            }
            var db = new AppDbContext();

            var items = storage_data.ItemsSource as List<DataModel>;
            var selectedData = items![index];
            var product2 = db.Products.First(p => p.Barcode == selectedData.Штрихкод);
            product2.TabName = Header;
            db.SaveChanges();
            if(Header == Kassa.tab1.Header.ToString()) Kassa.LoadTab();
            if(Header == Kassa.tab_kiyimlar.Header.ToString()) Kassa.LoadTabKiyim();
            if(Header == Kassa.tab_texnika.Header.ToString()) Kassa.LoadTabTexnika();
            if(Header == Kassa.tab_animal.Header.ToString()) Kassa.LoadTabAnimal();
            if(Header == Kassa.tab_ichimlik.Header.ToString()) Kassa.LoadTabIchimlik();
            if(Header == Kassa.tab_maishiy.Header.ToString()) Kassa.LoadTabMaishiy();

            Close();
        }

        private void storage_data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
