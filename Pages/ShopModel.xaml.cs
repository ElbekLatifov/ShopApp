using ShopSystem.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShopSystem.Pages
{
    /// <summary>
    /// Логика взаимодействия для ShopModel.xaml
    /// </summary>
    public partial class ShopModel : UserControl
    {
        WorkerPage workerPage;
        public ShopModel(WorkerPage worker)
        {
            InitializeComponent();
            workerPage = worker;
        }

        public object labelcha0
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

        public object labelcha1
        {
            get
            {
                return label_name.Text;
            }
            set
            {
                label_name.Text = value.ToString();
            }
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            editbtn.Visibility = Visibility.Visible;
            deletebtn.Visibility = Visibility.Visible;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            editbtn.Visibility= Visibility.Collapsed;
            deletebtn.Visibility= Visibility.Collapsed;
        }

        private void deletebtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var question = MessageBox.Show("Вы уверены, что хотите удалить магазин, при этом будут удалены и принадлежащие ему товары?!", "Warning",MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(question == MessageBoxResult.Yes)
            {
                var query = new AppDbContext();
                var shop = query.Shops.First(x => x.Id == (Guid)id.Content);
                query.Shops.Remove(shop);

                if (query.Products.Any(p => p.ShopId == shop.Id))
                {
                    var products = query.Products.Where(p => p.ShopId == shop.Id).ToList();
                    foreach (var product in products)
                    {
                        query.Products.Remove(product);
                    }
                }
                query.SaveChanges();
                workerPage.Page_Loaded(_showShops: true);
            }
        }

        private void editbtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var lastName = label_name.Text;
            UpdateWindow updateWindow = new UpdateWindow((Guid)id.Content, workerPage, lastName, "Shop");
            updateWindow.addtxt.Select(0, lastName.Length);
            updateWindow.ShowDialog();
        }


        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            workerPage.sklad_btn.Visibility = Visibility.Visible;
            workerPage.shops_btn.Visibility = Visibility.Hidden;
            workerPage.nazad_btn.Visibility = Visibility.Visible;
            workerPage.addproduct_btn.Visibility = Visibility.Visible;
            workerPage.ShopId = (Guid)id.Content;
            workerPage.main_lbl.Content = "Мой магазин: ";
            workerPage.shopName.Text = label_name.Text;
            workerPage.ShopId = (Guid)id.Content;
            workerPage.Load(true, false, false);
        }
    }
}
