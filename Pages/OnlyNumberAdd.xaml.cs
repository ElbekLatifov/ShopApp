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
    /// Логика взаимодействия для OnlyNumberAdd.xaml
    /// </summary>
    public partial class OnlyNumberAdd : Window
    {
        StoragesPage StoragesPage { get; set; }
        DataModel DataModel { get; set; }
        public OnlyNumberAdd(MainWindow main, StoragesPage storages, DataModel model)
        {
            InitializeComponent();
            DataModel = model;
            StoragesPage = storages;
            Owner = main;
            Price_come_txt.Text = model.Прибывшая.ToString();
            Price_go_txt.Text = model.Текущая.ToString();
            old_number.Content = $"Доступное количество: {model.Количство}";
            product_name_lbl.Content = model.Продукт;
        }

        private void Load()
        {
            var db = new AppDbContext();
            var product = db.Products.Where(p => p.Barcode == DataModel.Штрихкод).First();
            product.PriceCome = double.Parse(Price_come_txt.Text);
            product.PriceGo = double.Parse(Price_go_txt.Text);
            product.Count = int.Parse(Count_txt.Text) + DataModel.Количство;
            db.SaveChanges();
            StoragesPage.Load();
            Close();
            StoragesPage.edit_btn.Visibility = Visibility.Collapsed;
            StoragesPage.delete_btn.Visibility = Visibility.Collapsed;
            StoragesPage.only_add.Visibility = Visibility.Collapsed;
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
            if (Price_come_txt.Text.Length == 0 || Price_go_txt.Text.Length == 0 || Count_txt.Text.Length == 0)
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
    }
}
