using ShopSystem.Context;
using ShopSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для AddProductToGrid.xaml
    /// </summary>
    public partial class AddProductToGrid : Window
    {
        StoragesPage StoragesPage;
        public AddProductToGrid(MainWindow main, StoragesPage storagesPage)
        {
            InitializeComponent();
            Owner = main;
            this.StoragesPage = storagesPage;
            ComboLoad();
        }

        private void Load()
        {
            var db = new AppDbContext();
            var subcategory = db.Subcategories.First(p=>p.Title == subcategory_name.SelectedValue.ToString());
            var shop = db.Shops.First(p=>p.Name == shop_name.SelectedValue.ToString());
            var product = new Product()
            {
                ShopId = shop.Id,
                Categoryid = subcategory.Id,
                Title = categoryName_txt.Text,
                PriceCome = double.Parse(Price_come_txt.Text),
                Created_time = System.DateTime.UtcNow,
                PriceGo = double.Parse(Price_go_txt.Text),
                Count = int.Parse(Count_txt.Text)
            };
            db.Products.Add(product);
            db.SaveChanges();
            product.Barcode = GetBarcode(product.Id);
            db.Products.Update(product);
            db.SaveChanges();
            StoragesPage.Load();
            Close();
        }

        public void ComboLoad()
        {
            var db = new AppDbContext();
            var categories = db.Categories.ToList();
            var list = new List<string>();
            var list2 = new List<string>();
            foreach ( var category in categories )
            {
                list.Add(category.Title);
            }
            var subcategories = db.Shops.ToList();
            foreach ( var subcategory in subcategories )
            {
                list2.Add(subcategory.Name);
            }

            category_name.ItemsSource = list;
            shop_name.ItemsSource = list2;
        }

        private string GetBarcode(Guid path)
        {
            byte[] generatedBarcode = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(path.ToString()!));
            var res = BitConverter.ToUInt32(generatedBarcode, 0) % 100000;

            string barcode = CalculateEan13("789", "1100", res.ToString());
            return barcode.ToString();
        }

        public static string CalculateEan13(string country, string manufacturer, string product)
        {
            string temp = $"{country}{manufacturer}{product}";
            int sum = 0;
            int digit = 0;

            for (int i = temp.Length; i >= 1; i--)
            {
                digit = Convert.ToInt32(temp.Substring(i - 1, 1));

                if (i % 2 == 0)
                {
                    sum += digit * 3;
                }
                else
                {
                    sum += digit * 1;
                }
            }
            int checkSum = (10 - (sum % 10)) % 10;
            return $"{temp}{checkSum}";
        }

        private void categoryName_txt_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void txt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string pattern = @"^[0-9]+$";

            //// Create a regular expression object with the pattern
            Regex regex = new Regex(pattern);

            //// Check if the entered text matches the pattern
            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true; // Ignore the input
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(categoryName_txt.Text.Length == 0 || Price_come_txt.Text.Length == 0 || Price_go_txt.Text.Length == 0 || Count_txt.Text.Length == 0 || category_name.SelectedValue == null || subcategory_name.SelectedValue == null || shop_name.SelectedValue == null)
            {
                MessageBox.Show("to'ldiring");
                return;
            }
            Load();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Load();
            }
        }

        private void Price_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Price_come_txt.Text != string.Empty)
            {
                var formatted = (String.Format("{0:N}", double.Parse(Price_come_txt.Text)));
                Price_come_txt.Text = formatted.Remove(formatted.Length - 3);
                Price_come_txt.Select(Price_come_txt.Text.Length, 0);
            }
        }

        private void Price_go_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Price_go_txt.Text != string.Empty)
            {
                var formatted = (String.Format("{0:N}", double.Parse(Price_go_txt.Text)));
                Price_go_txt.Text = formatted.Remove(formatted.Length - 3);
                Price_go_txt.Select(Price_go_txt.Text.Length, 0);
            }
        }

        private void Count_txt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void Count_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Count_txt.Text != string.Empty)
            {
                var formatted = (String.Format("{0:N}", double.Parse(Count_txt.Text)));
                Count_txt.Text = formatted.Remove(formatted.Length - 3);
                Count_txt.Select(Count_txt.Text.Length, 0);
            }
        }

        private void category_name_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(shop_name.SelectedValue == null)
            {
                MessageBox.Show("avval shopni tanla"); return;
            }
        }

        private void category_name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var db = new AppDbContext();
            var category = db.Categories.First(p=>p.Title == category_name.SelectedValue.ToString());
            var subcategories = db.Subcategories.Where(p => p.ParentId == category.Id).ToList();
            var lsit  = new List<string>();
            foreach (var item in subcategories)
            {
                lsit.Add(item.Title);
            }

            subcategory_name.ItemsSource = lsit;
        }

        private void subcategory_name_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (category_name.SelectedValue == null)
            {
                MessageBox.Show("avval category tanla"); return;
            }
        }
    }
}
