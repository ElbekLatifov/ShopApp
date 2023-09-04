using Microsoft.EntityFrameworkCore;
using ShopSystem.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            cat_steak.Visibility = Visibility.Hidden;
            search_steak.Visibility= Visibility.Hidden;
            Salom.Height = 0;
        }

        private void myshops_btn_Click(object sender, RoutedEventArgs e)
        {
           // Salom.Navigate(new ShopsPage(MainWindow, this, allShops: true));
            belgi_moymagazin.Visibility = Visibility.Visible;
            belgi_sklad.Visibility = Visibility.Hidden;
            NoVisiblb();
            belgi_addproduct.Visibility= Visibility.Hidden;
            Salom.Height = 0;
            worker_grid.Height = 700;
            Page_Loaded(false, true);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            belgi_moymagazin.Visibility = Visibility.Hidden;
            belgi_sklad.Visibility = Visibility.Visible;
            belgi_addproduct.Visibility = Visibility.Hidden;
            Salom.Height = 700;
            Salom.Navigate(new StoragesPage(MainWindow, this));
            worker_grid.Height = 0;
        }

        private async void addproduct_btn_Click(object sender, RoutedEventArgs e)
        {
            belgi_addproduct.Visibility = Visibility.Visible;
            belgi_moymagazin.Visibility = Visibility.Hidden;
            belgi_sklad.Visibility = Visibility.Hidden;
            if(CategoryId == null)
            {
                worker_grid.Height = 700;
                Salom.Height = 0;
                await Load(_category: true);
            }
            else if(SubCategoryId == null) 
            {
                worker_grid.Height = 700;
                Salom.Height = 0;
                await Load(_subcategory: true); 
            }
            else
            {
                if(worker_grid.Height == 0)
                {
                    worker_grid.Height = 700;
                    Salom.Height = 0;
                    await Load(_products: true);
                }
                else
                {
                    AddProduct addProduct = new AddProduct(MainWindow, this, SubCategoryId);
                    addProduct.ShowDialog();
                }
            }
        }

        private void nazad_btn_Click(object sender, RoutedEventArgs e)
        {
            Back();
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

        public async Task Load(bool _category = false, bool _subcategory = false, bool _products = false)
        {
            Visiblb();
            var db = new AppDbContext();

            if (_category)
            {
                var categories = await db.Categories.OrderByDescending(p => p.Created_time).ToListAsync();
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
                var subcategories = await db.Subcategories.Where(p => p.ParentId == CategoryId).OrderByDescending(p => p.Created_time).ToListAsync();
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
                var products = await db.Products.Where(p => p.Categoryid == SubCategoryId && p.ShopId == ShopId).OrderByDescending(p => p.Created_time).ToListAsync();
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
            if (addCategory_btn.Content.ToString() == "+Категория" || addCategory_btn.Content.ToString() == "+Подкатегория")
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

        public async void Back()
        {
            var db = new AppDbContext();

            if(worker_grid.Height == 0)
            {
                belgi_addproduct.Visibility = Visibility.Visible;
                belgi_sklad.Visibility = Visibility.Collapsed;
                if(SubCategoryId != null)
                {
                    Salom.Height = 0;
                    worker_grid.Height = 700;
                    var _subcategory = db.Subcategories.First(p => p.Id == SubCategoryId);
                    var _category = db.Categories.First(p => p.Id == _subcategory.ParentId);
                    var shop = db.Shops.First(p => p.Id == ShopId);
                    await Load(false, false, true);
                    shopName.Text = shop.Name;
                    main_lbl.Content = "Мой магазин: ";
                    categoryname.Text = "   Категория: " + _category.Title;
                    subcategoryname.Text = "   Подкатегория: " + _subcategory.Title;
                }
                else 
                if(CategoryId != null)
                {
                    Salom.Height = 0;
                    worker_grid.Height = 700;
                    var _category = db.Categories.First(p => p.Id == CategoryId);
                    var shop = db.Shops.First(p => p.Id == ShopId);
                    await Load(false, true, false);
                    addCategory_btn.Content = "+Подкатегория";
                    subcategoryname.Text = "";
                    shopName.Text = shop.Name;
                    main_lbl.Content = "Мой магазин: ";
                    categoryname.Text = "   Категория: " + _category.Title;
                }
                else if(ShopId != null)
                {
                    Salom.Height = 0;
                    worker_grid.Height = 700;
                    await Load(true, false, false);
                    addCategory_btn.Content = "+Категория";
                    main_lbl.Content = "Мой магазин: ";
                    var shop = db.Shops.First(p => p.Id == ShopId);
                    shopName.Text = shop.Name;
                }
                else
                {
                    Salom.Height = 0;
                    worker_grid.Height = 700;
                    Page_Loaded(_showShops: true);
                }
            }
            else

            if (db.Subcategories.Any(p => p.Id == SubCategoryId))
            {
                var _subcategory = db.Subcategories.First(p => p.Id == SubCategoryId);
                var _category = db.Categories.First(p => p.Id == _subcategory.ParentId);
                var shop = db.Shops.First(p => p.Id == ShopId);
                await Load(false, true, false);
                addCategory_btn.Content = "+Подкатегория";
                addCategory_btn.Visibility = Visibility.Visible;
                subcategoryname.Text = "";
                shopName.Text = shop.Name;
                main_lbl.Content = "Мой магазин: ";
                categoryname.Text = "   Категория: " + _category.Title;
                var category = db.Categories.First();
                CategoryId = category.Id;
                SubCategoryId = null;
            }
            else
            {
                if (db.Categories.Any(p => p.Id == CategoryId))
                {
                    await Load(true, false, false);
                    addCategory_btn.Content = "+Категория";
                    addCategory_btn.Visibility = Visibility.Visible;
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
                    belgi_moymagazin.Visibility = Visibility.Visible;
                    belgi_sklad.Visibility = Visibility.Hidden;
                    Page_Loaded(false, true);
                }
            }
        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            Back();
        }
    }
}
