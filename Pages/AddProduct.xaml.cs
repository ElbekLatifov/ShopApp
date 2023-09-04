using IronBarCode;
using ShopSystem.Context;
using ShopSystem.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShopSystem.Pages
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        WorkerPage shopsPage;
        Guid? CategoryId;
        public AddProduct(MainWindow main, WorkerPage shopsPage, Guid? CategoryId)
        {
            InitializeComponent();
            this.shopsPage = shopsPage;
            this.CategoryId = CategoryId;
            Names(shopsPage);
            Owner = main;
        }

        private void Names(WorkerPage workerPage)
        {
            var db = new AppDbContext();
            var category = db.Categories.First(p => p.Id == workerPage.CategoryId);
            var subcategory = db.Subcategories.First(p => p.Id == workerPage.SubCategoryId);

            category_name.Text = category.Title;
            subcategory_name.Text = subcategory.Title;
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

        private async void AddProductMethod()
        {
            if (categoryName_txt.Text.Length == 0 || Price_come_txt.Text.Length == 0 || Price_go_txt.Text.Length == 0) { MessageBox.Show("Заполните необходимые поля"); return; }

            var db = new AppDbContext();

            var product = new Product()
            {
                ShopId = shopsPage.ShopId,
                Categoryid = CategoryId,
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
            await shopsPage.Load(_category: false, _subcategory: false, _products: true);
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddProductMethod();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                AddProductMethod();
            }
        }

        private void Price_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Price_come_txt.Text != string.Empty)
            {
                var formatted = (String.Format("{0:N}", double.Parse(Price_come_txt.Text)));
                Price_come_txt.Text = formatted.Remove(formatted.Length-3);
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
            if(e.Key == Key.Space)
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
    }
}
