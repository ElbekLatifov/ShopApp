using ShopSystem.Context;
using ShopSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для EditFromGrid.xaml
    /// </summary>
    public partial class EditFromGrid : Window
    {
        DataModel _model;
        StoragesPage _storagesPage;
        public EditFromGrid(MainWindow main, StoragesPage storages, DataModel dataModel)
        {
            InitializeComponent();
            Owner = main;
            _model = dataModel;
            _storagesPage = storages;
            shopname_name.Text = "Магазин: " + dataModel.Магазин;
            product_txt.Text = dataModel.Продукт;
            category_name.Text = "Категория: " + dataModel.Категория;
            subcategory_name.Text = "Подкатегория: " + dataModel.Подкатегория;
            Price_come_txt.Text = dataModel.Прибывшая.ToString();
            Price_go_txt.Text = dataModel.Текущая.ToString();
            Count_txt.Text = dataModel.Количство.ToString();
        }

        public void Edit()
        {
            var db = new AppDbContext();
            var product = db.Products.Where(p=>p.Barcode == _model.Штрихкод).First();
            product.Title = product_txt.Text;
            product.PriceCome = double.Parse(Price_come_txt.Text);
            product.PriceGo = double.Parse(Price_go_txt.Text);
            product.Count = int.Parse(Count_txt.Text);
            db.SaveChanges();
            _storagesPage.Load();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Edit();
            Close();
            _storagesPage.edit_btn.Visibility = Visibility.Collapsed;
            _storagesPage.delete_btn.Visibility = Visibility.Collapsed;
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

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Edit();
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
    }
}
