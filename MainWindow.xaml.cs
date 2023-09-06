using ShopSystem.Context;
using ShopSystem.Pages;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace ShopSystem
{     
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainframe.Navigate(new MainMenuPage(this));
        }

        private void Page1_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите закрыть?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                var db = new AppDbContext();
                var cashes = db.CashedProducts.ToList();
                db.CashedProducts.RemoveRange(cashes);
                db.SaveChanges();
                Application.Current.Shutdown();
            }
        }

        private void exit_btn_MouseEnter(object sender, MouseEventArgs e)
        {
            exit_btn.Background = Brushes.Red;
        }

        private void exit_btn_MouseLeave(object sender, MouseEventArgs e)
        {
            exit_btn.Background = Brushes.Navy;
        }

        private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
