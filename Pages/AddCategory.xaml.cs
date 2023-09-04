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
    /// Логика взаимодействия для AddCategory.xaml
    /// </summary>
    public partial class AddCategory : Window
    {
        WorkerPage page;
        MainWindow MainWindow;
        public AddCategory(MainWindow main, WorkerPage categoriesPage)
        {
            InitializeComponent();
            MainWindow = main;
            page = categoriesPage;
            categoryName_txt.Select(0, 0);
            Owner = main;
        }

        private void AddCategoryMethod()
        {
            if (categoryName_txt.Text.Length == 0) { MessageBox.Show("Заполните необходимые поля"); return; }
            var db = new AppDbContext();
            if(db.Categories.Any(p=>p.Id==page.CategoryId))
            {
                var subcategory = new Subcategory()
                {
                    ShopId = page.ShopId,
                    Created_time = System.DateTime.UtcNow,
                    Title = categoryName_txt.Text,
                    ParentId = page.CategoryId
                };
                db.Subcategories.Add(subcategory);
                db.SaveChanges();
                page.Load(false,true,false);
                Close();
            }
            else
            {
                var category = new Category()
                {
                    ShopId = page.ShopId,
                    Created_time = System.DateTime.UtcNow,
                    Title = categoryName_txt.Text
                };
                db.Categories.Add(category);
                db.SaveChanges();
                page.Load(true,false,false);
                Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddCategoryMethod();
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

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                AddCategoryMethod();
            }
        }
    }
}
