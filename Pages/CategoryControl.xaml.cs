using ShopSystem.Context;
using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace ShopSystem.Pages
{
    /// <summary>
    /// Логика взаимодействия для CategoryControl.xaml
    /// </summary>
    public partial class CategoryControl : UserControl
    {
        public object labelcha0
        {
            get
            {
                return category_lbl.Text;
            }
            set
            {
                category_lbl.Text = value.ToString();
            }
        }
        public object labelcha1
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
        WorkerPage workerPage;
        public CategoryControl(WorkerPage worker) 
        { 
            InitializeComponent();
            workerPage = worker;
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var query = new AppDbContext();

            if (query.Subcategories.Any(x => x.Id == (Guid)id.Content))
            {
                workerPage.addCategory_btn.Content = "+Продукт";
                workerPage.subcategoryname.Text = $"   Подкатегория: {category_lbl.Text}";
                workerPage.Load(false,false,true);
                workerPage.SubCategoryId = (Guid)id.Content;
            }
            else if (query.Categories.Any(x => x.Id == (Guid)id.Content))
            {
                workerPage.addCategory_btn.Content = "+Категория";
                workerPage.categoryname.Text = $"   Категория: {category_lbl.Text}";
                workerPage.Load(false,true,false);
                workerPage.CategoryId = (Guid)id.Content;
            }
        }

        private void editbtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var lastName = category_lbl.Text;
            UpdateWindow updateWindow = new UpdateWindow((Guid)id.Content, workerPage, lastName, "Category");
            updateWindow.ShowDialog();
        }

        private void deletebtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var query = new AppDbContext();
            if(query.Categories.Any(x => x.Id == (Guid)id.Content))
            {
                var question = MessageBox.Show("Вы уверены, что хотите удалить категорию, тогда подкатегория категории также будет удалена?!", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Hand);
                if (question == MessageBoxResult.Yes)
                {
                    var question2 = MessageBox.Show("Вы уверены, что хотите удалить подкатегорию, при этом будут удалены и товары, принадлежащие к подкатегории?!", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Hand);
                    if(question2 == MessageBoxResult.Yes)
                    {
                        var category = query.Categories.First(x => x.Id == (Guid)id.Content);
                        var subcategories = query.Subcategories.Where(p => p.ParentId == category.Id).ToList();
                        if(subcategories.Count>0)
                        {
                            foreach (var item in subcategories)
                            {
                                query.Subcategories.Remove(item);
                                var products = query.Products.Where(p=>p.Categoryid == item.Id).ToList();
                                if (products.Count>0)
                                query.RemoveRange(products);
                            }
                        }
                        query.Categories.Remove(category);
                        query.SaveChanges();
                        workerPage.Load(true, false, false);
                    }
                }
               
            }
            else if (query.Subcategories.Any(x => x.Id == (Guid)id.Content))
            {
                var question = MessageBox.Show("Вы уверены, что хотите удалить подкатегорию, при этом будут удалены и товары, принадлежащие к подкатегории?!", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Hand);
                if (question == MessageBoxResult.Yes)
                {
                    var subcategory = query.Subcategories.First(x => x.Id == (Guid)id.Content);
                            var products = query.Products.Where(p => p.Categoryid == subcategory.Id).ToList();
                            if (products.Count > 0)
                                query.RemoveRange(products);
                        
                    
                    query.Subcategories.Remove(subcategory);
                    query.SaveChanges();
                    workerPage.Load(false, true, false);
                }
            }            
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            editbtn.Visibility = Visibility.Visible;
            deletebtn.Visibility = Visibility.Visible;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            editbtn.Visibility = Visibility.Hidden;
            deletebtn.Visibility = Visibility.Hidden;
        }
    }
}
