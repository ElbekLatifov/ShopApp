using System;
using System.Windows;
using System.Windows.Controls;

namespace ShopSystem.Pages
{
    /// <summary>
    /// Логика взаимодействия для WorkerPage.xaml
    /// </summary>
    public partial class WorkerPage : Page
    {
        MainWindow MainWindow;
        public WorkerPage(MainWindow main)
        {
            InitializeComponent();
            MainWindow = main;
            belgi_moymagazin.Visibility = Visibility.Hidden;
            belgi_sklad.Visibility = Visibility.Hidden;
            belgi_addproduct.Visibility = Visibility.Hidden;
        }

        private void myshops_btn_Click(object sender, RoutedEventArgs e)
        {
            Salom.Navigate(new ShopsPage(MainWindow, this, allShops: true));
            belgi_moymagazin.Visibility = Visibility.Visible;
            belgi_sklad.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Salom.Navigate(new StoragesPage(MainWindow));
            belgi_moymagazin.Visibility = Visibility.Hidden;
            belgi_sklad.Visibility = Visibility.Visible; 
        }
    }
}
