using ShopSystem.Context;
using ShopSystem.Models;
using System.Text.RegularExpressions;
using System.Windows;

namespace ShopSystem.Pages
{
    /// <summary>
    /// Логика взаимодействия для CreateWindow.xaml
    /// </summary>
    public partial class CreateWindow : Window
    {
        ShopsPage ShopsPage { get; set; }

        public CreateWindow(MainWindow main, ShopsPage shopsPage)
        {
            InitializeComponent();
            Owner = main;
            this.ShopsPage = shopsPage;
            addtxt.Select(0, 0);
        }

        private void CreateShop()
        {
            var shopName = addtxt.Text;

            var query = new AppDbContext();

            if (shopName.Length == 0) { MessageBox.Show("Заполните необходимые поля"); return; }
            var owner = Properties.Settings.Default.Name;

            var shop = new Shop()
            {
                Name = shopName,
                Owner = owner,
                Created_time = System.DateTime.UtcNow
            };
            query.Shops.Add(shop);
            query.SaveChanges();

            createWin.Close();

            ShopsPage.Page_Loaded(_showShops: true);
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
           CreateShop();
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

        private void createWin_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter)
            {
                CreateShop();
            }
        }
    }
}
