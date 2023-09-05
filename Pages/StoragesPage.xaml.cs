using MaterialDesignThemes.Wpf;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShopSystem.Pages
{
    /// <summary>
    /// Логика взаимодействия для StoragesPage.xaml
    /// </summary>
    public partial class StoragesPage : Page
    {
        MainWindow Main;
        public WorkerPage Worker;
        public StoragesPage(MainWindow main, WorkerPage worker)
        {
            InitializeComponent();
            Main = main;
            Worker = worker;
            Load();
        }

        public void Load()
        {
            using (var db = new AppDbContext())
            {
                var items = new List<DataModel>();
                var products = db.Products.Where(p=>p.ShopId == Worker.ShopId).ToList();
                int i = 1;
                if (products.Count > 0)
                {
                    foreach (var product in products)
                    {
                        var subcategory = db.Subcategories.First(p => p.Id == product.Categoryid);
                        var category = db.Categories.First(p => p.Id == subcategory.ParentId);
                        var shop = db.Shops.First(p => p.Id == product.ShopId);

                        var item = new DataModel();
                        item.Номер = i;
                        item.Продукт = product.Title;
                        item.Подкатегория = subcategory.Title;
                        item.Категория = category.Title;
                        item.Прибывшая = product.PriceCome;
                        item.Штрихкод = product.Barcode!;
                        item.Текущая = product.PriceGo;
                        item.Магазин = shop.Name;
                        item.Количство = product.Count;
                        items.Add(item);
                        i++;
                    }
                    
                    storage_data.ItemsSource = items;
                }
            }
        }

        private void edit_btn_Click(object sender, RoutedEventArgs e)
        {
            var index = storage_data.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Выберите продукт");
                return;
            }
            var db = new AppDbContext();
            
                var items = new List<DataModel>();
                var products = db.Products.Where(p => p.ShopId == Worker.ShopId).ToList();
                int i = 1;
                if (products.Count > 0)
                {
                    foreach (var product in products)
                    {
                        var subcategory = db.Subcategories.First(p => p.Id == product.Categoryid);
                        var category = db.Categories.First(p => p.Id == subcategory.ParentId);
                        var shop = db.Shops.First(p => p.Id == product.ShopId);

                        var item = new DataModel();
                        item.Номер = i;
                        item.Продукт = product.Title;
                        item.Подкатегория = subcategory.Title;
                        item.Категория = category.Title;
                        item.Прибывшая = product.PriceCome;
                        item.Штрихкод = product.Barcode!;
                        item.Текущая = product.PriceGo;
                        item.Магазин = shop.Name;
                        item.Количство = product.Count;
                        items.Add(item);
                        i++;
                    }
                }
            var selectedData = items[index];
            EditFromGrid editFrom = new EditFromGrid(Main, this, selectedData);
            editFrom.ShowDialog();
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            AddProductToGrid addProductToGrid = new AddProductToGrid(Main, this);
            addProductToGrid.ShowDialog();
        }

        private void storage_data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //edit_btn.Visibility = Visibility.Visible;
            //delete_btn.Visibility = Visibility.Visible;
            //only_add.Visibility = Visibility.Visible;
        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            var index = storage_data.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Выберите продукт");
                return;
            }
            var items = new List<DataModel>();
            items = (List<DataModel>)storage_data.ItemsSource;
            var item = items[index];
            var surov = MessageBox.Show("Вы уверены, что хотите удалить товар?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Stop);
            if (surov == MessageBoxResult.Yes)
            {
                var db = new AppDbContext();
                var product = db.Products.Where(p => p.ShopId == Worker.ShopId).First(p => p.Barcode == item.Штрихкод);
                db.Products.Remove(product);
                db.SaveChanges();
                Load();
            }
        }

        private void read_btn_Click(object sender, RoutedEventArgs e)
        {
            Load(); 
        }

        private void only_add_Click(object sender, RoutedEventArgs e)
        {
            var index = storage_data.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Выберите продукт");
                return;
            } 
            var db = new AppDbContext();

            var items = new List<DataModel>();
            var products = db.Products.Where(p => p.ShopId == Worker.ShopId).ToList();
            int i = 1;
            if (products.Count > 0)
            {
                foreach (var product in products)
                {
                    var subcategory = db.Subcategories.First(p => p.Id == product.Categoryid);
                    var category = db.Categories.First(p => p.Id == subcategory.ParentId);
                    var shop = db.Shops.First(p => p.Id == product.ShopId);

                    var item = new DataModel();
                    item.Номер = i;
                    item.Продукт = product.Title;
                    item.Подкатегория = subcategory.Title;
                    item.Категория = category.Title;
                    item.Прибывшая = product.PriceCome;
                    item.Штрихкод = product.Barcode!;
                    item.Текущая = product.PriceGo;
                    item.Магазин = shop.Name;
                    item.Количство = product.Count;
                    items.Add(item);
                    i++;
                }
            }
            var selectedData = items[index];
            OnlyNumberAdd editFrom = new OnlyNumberAdd(Main, this, selectedData);
            editFrom.ShowDialog();       
        }
    }
}
