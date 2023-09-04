using ShopSystem.Context;
using ShopSystem.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace ShopSystem.Pages
{
    /// <summary>
    /// Логика взаимодействия для UpdateWindow.xaml
    /// </summary>
    public partial class UpdateWindow : Window
    {
        WorkerPage _shopPage;
        Guid id;
        string type;
        public UpdateWindow(Guid id, WorkerPage shopsPage, string lastname, string type)
        {
            InitializeComponent();
            _shopPage = shopsPage;
            this.id = id;
            this.type = type;   
            addtxt.Text = lastname;
            addtxt.Select(0, lastname.Length);
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            switch (type)
            {
                case "Shop": UpdateShop(); break;
                case "Category": UpdateCategory(); break;
            }
        }

        public void UpdateShop()
        {
            var shopName = addtxt.Text;

            var query = new AppDbContext();

            if (shopName.Length == 0) { MessageBox.Show("Заполните места"); return; }

            try
            {

                var shop = query.Shops.First(p => p.Id == _shopPage.ShopId);

                shop.Name = addtxt.Text;

                query.Shops.Update(shop);
                query.SaveChanges();
                _shopPage.Page_Loaded(_showShops: true);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void UpdateCategory()
        {
            var categoryName = addtxt.Text;

            var query = new AppDbContext();

            if (categoryName.Length == 0) { MessageBox.Show("Заполните места"); return; }

            try
            {
                if (query.Categories.Any(x => x.Id == id))
                {
                    var category = query.Categories.First(x => x.Id == id);
                    category.Title = addtxt.Text;
                    query.Categories.Update(category);
                    query.SaveChanges();
                    _shopPage.Load(true, false, false);
                    Close();
                }
                else if (query.Subcategories.Any(x => x.Id == id))
                {
                    var subcategory = query.Subcategories.First(x => x.Id == id);
                    subcategory.Title = addtxt.Text;
                    query.Subcategories.Update(subcategory);
                    query.SaveChanges();
                    _shopPage.Load(false, true, false);
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void addtxt_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            string pattern = @"^[а-яА-Яa-zA-Z0-9]+$";

            // Create a regular expression object with the pattern
            Regex regex = new Regex(pattern);

            // Check if the entered text matches the pattern
            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter)
            {
                switch (type)
                {
                    case "Shop": UpdateShop(); break;
                    case "Category": UpdateCategory(); break;
                }
            }
            
        }
    }
}
