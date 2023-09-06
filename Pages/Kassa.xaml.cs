using ShopSystem.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace ShopSystem.Pages
{
    /// <summary>
    /// Логика взаимодействия для Kassa.xaml
    /// </summary>
    public partial class Kassa : Page
    {
        public WorkerPage WorkerPage { get; set; }  
        public MainWindow MainWindow { get; set; } 
        public Kassa(MainWindow main, WorkerPage worker)
        {
            InitializeComponent();
            WorkerPage = worker;
            MainWindow = main;
            LoadTab(); LoadTabAnimal(); LoadTabIchimlik(); LoadTabKiyim(); LoadTabMaishiy(); LoadTabTexnika();
        }

        public void LoadTab()
        {
            tabcashed_products1.Items.Clear();
            var btn = new AddProductButton(MainWindow, WorkerPage, this, tab1.Header.ToString()!);
            tabcashed_products1.Items.Add(btn);
            var db = new AppDbContext();
            var products = db.Products.Where(p=>p.TabName == tab1.Header.ToString() && p.ShopId == WorkerPage.ShopId).ToList();

            foreach ( var product in products )
            {
                var tab = new TabProduct(this);
                tab.Height = 100; tab.Width = 100; 
                tab.ProductId = product.Id;
                tab.ProductName = product.Title;
                tabcashed_products1.Items.Add(tab);
            }
        }
        public void LoadTabKiyim()
        {
            tabcashed_products2.Items.Clear();
            var btn = new AddProductButton(MainWindow, WorkerPage, this, tab_kiyimlar.Header.ToString()!);
            tabcashed_products2.Items.Add(btn);
            var db = new AppDbContext();
            var products = db.Products.Where(p => p.TabName == tab_kiyimlar.Header.ToString() && p.ShopId == WorkerPage.ShopId).ToList();

            foreach (var product in products)
            {
                var tab = new TabProduct(this);
                tab.Height = 100; tab.Width = 100;
                tab.ProductId = product.Id;
                tab.ProductName = product.Title;
                tabcashed_products2.Items.Add(tab);
            }
        }

        public void LoadTabTexnika()
        {
            tabcashed_products3.Items.Clear();
            tabcashed_products3.Items.Add(new AddProductButton(MainWindow, WorkerPage, this, tab_texnika.Header.ToString()!));
            var db = new AppDbContext();
            var products = db.Products.Where(p => p.TabName == tab_texnika.Header.ToString() && p.ShopId == WorkerPage.ShopId).ToList();

            foreach (var product in products)
            {
                var tab = new TabProduct(this);
                tab.Height = 100; tab.Width = 100;
                tab.ProductId = product.Id;
                tab.ProductName = product.Title;
                tabcashed_products3.Items.Add(tab);
            }
        }

        public void LoadTabAnimal()
        {
            tabcashed_products4.Items.Clear();
            var btn = new AddProductButton(MainWindow, WorkerPage, this, tab_animal.Header.ToString()!); 
            tabcashed_products4.Items.Add(btn);
            var db = new AppDbContext();
            var products = db.Products.Where(p => p.TabName == tab_animal.Header.ToString() && p.ShopId == WorkerPage.ShopId).ToList();

            foreach (var product in products)
            {
                var tab = new TabProduct(this);
                tab.Height = 100; tab.Width = 100;
                tab.ProductId = product.Id;
                tab.ProductName = product.Title;
                tabcashed_products4.Items.Add(tab);
            }
        }

        public void LoadTabIchimlik()
        {
            tabcashed_products5.Items.Clear();
            var btn = new AddProductButton(MainWindow, WorkerPage, this, tab_ichimlik.Header.ToString()!);
            tabcashed_products5.Items.Add(btn);
            var db = new AppDbContext();
            var products = db.Products.Where(p => p.TabName == tab_ichimlik.Header.ToString() && p.ShopId == WorkerPage.ShopId).ToList();

            foreach (var product in products)
            {
                var tab = new TabProduct(this);
                tab.Height = 100; tab.Width = 100;
                tab.ProductId = product.Id;
                tab.ProductName = product.Title;
                tabcashed_products5.Items.Add(tab);
            }
        }

        public void LoadTabMaishiy()
        {
            tabcashed_products6.Items.Clear();
            var btn = new AddProductButton(MainWindow, WorkerPage, this, tab_maishiy.Header.ToString()!);
            tabcashed_products6.Items.Add(btn);
            var db = new AppDbContext();
            var products = db.Products.Where(p => p.TabName == tab_maishiy.Header.ToString() && p.ShopId == WorkerPage.ShopId).ToList();

            foreach (var product in products)
            {
                var tab = new TabProduct(this);
                tab.Height = 100; tab.Width = 100;
                tab.ProductId = product.Id;
                tab.ProductName = product.Title;
                tabcashed_products6.Items.Add(tab);
            }
        }

        public void LoadCashedProducts()
        {
            cashed_products.Items.Clear();
            var db = new AppDbContext();
            var tabs = new List<CashedProductModel>();
            var products = db.CashedProducts.OrderBy(p=>p.CashedTime).ToList();
            foreach ( var product in products )
            {
                var cashed = new CashedProductModel();
                cashed.Width = 350; cashed.Height = 50;
                cashed.Count = product.Totalcount;
                cashed.Price = product.PriceGo;
                cashed.ProductName = product.Title;
                cashed.Total = product.Totalcount * product.PriceGo;
                cashed_products.Items.Add(cashed);
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var db = new AppDbContext();
            var cashes = db.CashedProducts.ToList();    
            db.CashedProducts.RemoveRange(cashes);
            db.SaveChanges();
            LoadCashedProducts();
        }

        private void back_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var db = new AppDbContext();
            var cashes = db.CashedProducts.ToList();
            db.CashedProducts.RemoveRange(cashes);
            db.SaveChanges();
            NavigationService.GoBack();
        }

        private void tab1_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LoadTab();
        }

        private void tab_kiyimlar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LoadTabKiyim();
        }

        private void tab_texnika_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LoadTabTexnika();
        }

        private void tab_animal_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LoadTabAnimal();
        }

        private void tab_ichimlik_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LoadTabIchimlik();
        }

        private void tab_maishiy_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LoadTabMaishiy();
        }
    }
}
