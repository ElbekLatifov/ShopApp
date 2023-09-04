using ShopSystem.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ShopSystem.Pages
{
    /// <summary>
    /// Логика взаимодействия для ShopsPage.xaml
    /// </summary>
    public partial class ShopsPage : Page
    {
        MainWindow MainWindow { get; set; }
        WorkerPage workerPage;
        public Guid? ids;
        public Guid shopId;
        public ShopsPage(MainWindow main, WorkerPage worker, bool allShops = false,
            bool myShops = false, Guid? ids = null)
        {
            InitializeComponent();
            workerPage = worker;    
            MainWindow = main;
            Page_Loaded(myShops, allShops);
            this.ids = ids;

        }

        private void Visiblb()
        {
            cat_steak.Visibility = Visibility.Visible;
            search_steak.Visibility = Visibility.Collapsed;
        }
        private void NoVisiblb()
        {
            cat_steak.Visibility= Visibility.Collapsed;
            search_steak.Visibility = Visibility.Visible;
        }

        public void Load(Guid? id, bool _category=false, bool _subcategory=false, bool _products=false)
        {
            Visiblb();
            ids = id;
            var db = new AppDbContext();

            if(_category)
            {
                var categories = db.Categories.OrderByDescending(p => p.Created_time).ToList();
                var categoriesList = new List<CategoryControl>();

                foreach (var category in categories)
                {
                    CategoryControl categoryModel = new CategoryControl(this);

                    categoryModel.Width = 200;
                    categoryModel.Height = 120;
                    categoryModel.labelcha0 = category.Title;
                    categoryModel.labelcha1 = category.Id;
                    categoriesList.Add(categoryModel);
                }

                shoplists.ItemsSource = categoriesList;
            }

            if(_subcategory)
            {
                var subcategories = db.Subcategories.Where(p=>p.ParentId == ids).OrderByDescending(p => p.Created_time).ToList();
                var categoriesList = new List<CategoryControl>();

                foreach (var subcategory in subcategories)
                {
                    CategoryControl categoryModel = new CategoryControl(this);

                    categoryModel.Width = 200;
                    categoryModel.Height = 120;
                    categoryModel.labelcha0 = subcategory.Title;
                    categoryModel.labelcha1 = subcategory.Id;
                    categoriesList.Add(categoryModel);
                }

                shoplists.ItemsSource = categoriesList;
            }

            if(_products)
            {
                var products = db.Products.Where(p => p.Categoryid == id && p.ShopId == shopId).OrderByDescending(p => p.Created_time).ToList();
                var productsBox = new List<ProductControl>();

                foreach (var product in products)
                {
                    ProductControl productModel = new ProductControl();

                    productModel.Width = 150;
                    productModel.Height = 200;
                    productModel.Name = product.Title;
                    var formet = (String.Format("{0:N}", product.PriceGo));
                    productModel.Price = formet.Remove(formet.Length - 3);
                    productModel.Id = product.Id;

                    productsBox.Add(productModel);
                }

                shoplists.ItemsSource = productsBox;
            }
        }

        public void Page_Loaded(bool _myshops=false, bool _showShops=false)
        {
            main_lbl.Content = "Мои магазины";
            NoVisiblb();
            if (_myshops)
            {
                var query = new AppDbContext();
                var owner = Properties.Settings.Default.Name;
                List<ShopModel> shopModels = new List<ShopModel>();

                var shops = query.Shops.Where(p => p.Owner == owner).OrderByDescending(p => p.Created_time).ToList();

                foreach (var shop in shops)
                {
                    ShopModel shopModel = new ShopModel(workerPage, this);

                    shopModel.Width = 200;
                    shopModel.Height = 70;
                    shopModel.labelcha0 = shop.Id;
                    shopModel.labelcha1 = shop.Name;
                    shopModels.Add(shopModel);
                }

                shoplists.ItemsSource = shopModels;
            }
            if (_showShops)
            {
                var query = new AppDbContext();
                List<ShopModel> shopModels = new List<ShopModel>();

                var shops = query.Shops.OrderByDescending(p=>p.Created_time).ToList();

                foreach (var shop in shops)
                {
                    ShopModel shopModel = new ShopModel(workerPage, this);

                    shopModel.Width = 200;
                    shopModel.Height = 70;
                    shopModel.labelcha0 = shop.Id;
                    shopModel.labelcha1 = shop.Name;
                    shopModels.Add(shopModel);
                }

                shoplists.ItemsSource = shopModels;
            }
        }

        private void search_btn_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(search_txt.Text))
            {
                return;
            }
            var shopModels = new List<ShopModel>();
            var query = new AppDbContext();

            var shops = from b in query.Shops
                        where b.Name.Contains(search_txt.Text)
                        select b;

            foreach (var shop in shops)
            {
                ShopModel shopModel = new ShopModel(workerPage, this);

                shopModel.Width = 200;
                shopModel.Height = 70;
                shopModel.labelcha0 = shop.Id;
                shopModel.labelcha1 = shop.Name;
                shopModels.Add(shopModel);
            }

            shoplists.ItemsSource = shopModels;
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            CreateWindow createWindow = new CreateWindow(MainWindow, this);
            createWindow.ShowDialog();
        }

        private void search_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            var shopModels = new List<ShopModel>();
            var query = new AppDbContext();

            var shops = from b in query.Shops
                        where b.Name.Contains(search_txt.Text)
                        select b;

            foreach (var shop in shops)
            {
                ShopModel shopModel = new ShopModel(workerPage, this);

                shopModel.Width = 200;
                shopModel.Height = 70;
                shopModel.labelcha0 = shop.Id;
                shopModel.labelcha1 = shop.Name;
                shopModels.Add(shopModel);
            }

            shoplists.ItemsSource = shopModels;
        }

        private void addCategory_btn_Click(object sender, RoutedEventArgs e)
        {
            if(addCategory_btn.Content.ToString() == "+Категория")
            {
                AddCategory addCategory = new AddCategory(MainWindow, this);
                addCategory.ShowDialog();
            }
            if(addCategory_btn.Content.ToString() == "+Продукт")
            {
                AddProduct addProduct = new AddProduct(MainWindow, this, ids);
                addProduct.ShowDialog();
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var db = new AppDbContext();

            if (db.Subcategories.Any(p => p.Id == ids))
            {
                var _subcategory = db.Subcategories.First(p => p.Id == ids);
                Load(_subcategory.ParentId, false, true, false);
                addCategory_btn.Content = "+Категория";
                subcategoryname.Text = "";
                var category = db.Categories.First();
                ids = category.Id;
            }
            else
            {
                if(db.Categories.Any(p => p.Id == ids))
                {
                    Load(ids, true, false, false);
                    addCategory_btn.Content = "+Категория";
                    categoryname.Text = "";
                    ids = null;
                }
                else
                {
                    NavigationService.Navigate(new ShopsPage(MainWindow, workerPage, allShops: true));
                    main_lbl.Content = "Мои магазины";
                    shopName.Text = "";
                    workerPage.shops_btn.Visibility = Visibility.Visible;
                    workerPage.addproduct_btn.Visibility = Visibility.Hidden;
                    workerPage.sklad_btn.Visibility = Visibility.Hidden;
                }
            }
        }
    }
}
