using ShopSystem.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ShopSystem.Pages
{
    /// <summary>
    /// Логика взаимодействия для WorkerPage.xaml
    /// </summary>
    public partial class WorkerPage : Page
    {
        MainWindow MainWindow;
        public Guid ShopId { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }
        public WorkerPage(MainWindow main)
        {
            InitializeComponent();
            MainWindow = main;
            belgi_moymagazin.Visibility = Visibility.Hidden;
            belgi_sklad.Visibility = Visibility.Hidden;
            belgi_addproduct.Visibility = Visibility.Hidden;
        }

        private void myshops_btn_Click(object sender, RoutedEventArgs e)
        {
           // Salom.Navigate(new ShopsPage(MainWindow, this, allShops: true));
            belgi_moymagazin.Visibility = Visibility.Visible;
            belgi_sklad.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Page_Loaded(false, true);
            belgi_moymagazin.Visibility = Visibility.Hidden;
            belgi_sklad.Visibility = Visibility.Visible; 
        }

        public void ShopsPage(bool allShops = false, bool myShops = false, bool IsAfter =  false, bool IsAfter2 = false, Guid? ids = null, bool isBack = false, bool _category = false, bool _subcategory = false, bool _products = false)
        {
          //  Salom.Navigate(new ShopsPage(MainWindow, this, allShops, myShops, IsAfter, IsAfter2, ids,_category , _subcategory, _products));
        }

        private void addproduct_btn_Click(object sender, RoutedEventArgs e)
        {
            if(CategoryId == null)
            {
                MessageBox.Show("");
              //  Salom.Navigate(new ShopsPage(MainWindow, this, isAfter: true));
            }
            else if(SubCategoryId == null) 
            {
                MessageBox.Show("");
              //  Salom.Navigate(new ShopsPage(MainWindow, this, isAfter2: false));
            }
            else
            {
                AddProduct addProduct = new AddProduct(MainWindow, this, SubCategoryId);
                addProduct.ShowDialog();
            }
        }

        private void nazad_btn_Click(object sender, RoutedEventArgs e)
        {
           // Salom.Navigate(new ShopsPage(MainWindow, this, isBack: true));
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void Visiblb()
        {
            cat_steak.Visibility = Visibility.Visible;
            search_steak.Visibility = Visibility.Collapsed;
        }
        private void NoVisiblb()
        {
            cat_steak.Visibility = Visibility.Collapsed;
            search_steak.Visibility = Visibility.Visible;
        }

        public void Load(bool _category = false, bool _subcategory = false, bool _products = false)
        {
            Visiblb();
            var db = new AppDbContext();

            if (_category)
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

            if (_subcategory)
            {
                var subcategories = db.Subcategories.Where(p => p.ParentId == CategoryId).OrderByDescending(p => p.Created_time).ToList();
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

            if (_products)
            {
                var products = db.Products.Where(p => p.Categoryid == SubCategoryId && p.ShopId == ShopId).OrderByDescending(p => p.Created_time).ToList();
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

        public void Page_Loaded(bool _myshops = false, bool _showShops = false)
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
                    ShopModel shopModel = new ShopModel(this);

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

                var shops = query.Shops.OrderByDescending(p => p.Created_time).ToList();

                foreach (var shop in shops)
                {
                    ShopModel shopModel = new ShopModel(this);

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
                ShopModel shopModel = new ShopModel(this);

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
                ShopModel shopModel = new ShopModel(this);

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
            if (addCategory_btn.Content.ToString() == "+Категория")
            {
                AddCategory addCategory = new AddCategory(MainWindow, this);
                addCategory.ShowDialog();
            }
            if (addCategory_btn.Content.ToString() == "+Продукт")
            {
                AddProduct addProduct = new AddProduct(MainWindow, this, SubCategoryId);
                addProduct.ShowDialog();
            }

        }

        public void Back(bool isBack)
        {
            var db = new AppDbContext();
            if (isBack)
            {
                if (SubCategoryId != null)
                {
                    SubCategoryId = null;
                }
                else if (CategoryId != null)
                {
                    CategoryId = null;
                }
                if (db.Subcategories.Any(p => p.Id == SubCategoryId))
                {
                    var _subcategory = db.Subcategories.First(p => p.Id == SubCategoryId);
                    var _category = db.Categories.First(p => p.Id == _subcategory.ParentId);
                    var shop = db.Shops.First(p => p.Id == ShopId);
                    Load(false, true, false);
                    addCategory_btn.Content = "+Категория";
                    subcategoryname.Text = "";
                    shopName.Text = shop.Name;
                    main_lbl.Content = "Мой магазин: ";
                    categoryname.Text = "   Категория: " + _category.Title;
                    var category = db.Categories.First();
                    CategoryId = category.Id;
                }
                else
                {
                    if (db.Categories.Any(p => p.Id == CategoryId))
                    {
                        Load(true, false, false);
                        addCategory_btn.Content = "+Категория";
                        main_lbl.Content = "Мой магазин: ";
                        var shop = db.Shops.First(p => p.Id == ShopId);
                        categoryname.Text = "";
                        shopName.Text = shop.Name;
                        CategoryId = null;
                    }
                    else
                    {
                        main_lbl.Content = "Мои магазины";
                        shopName.Text = "";
                        shops_btn.Visibility = Visibility.Visible;
                        addproduct_btn.Visibility = Visibility.Hidden;
                        nazad_btn.Visibility = Visibility.Hidden;
                        sklad_btn.Visibility = Visibility.Hidden;
                        Page_Loaded(false, true);
                    }
                }
            }
        }
    }
}
