using ShopSystem.Context;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ShopSystem.Pages
{
    /// <summary>
    /// Логика взаимодействия для KassaControl.xaml
    /// </summary>
    public partial class KassaControl : UserControl
    {
        public object CashId
        {
            get
            {
                return idd.Content;
            }
            set
            {
                idd.Content = value;
            }
        }

        public object CashName
        {
            get
            {
                return label_name.Text;
            }
            set
            {
                label_name.Text = value.ToString();
            }
        }
        CashBoxPage CashBoxPage { get; set; }
        MainWindow MainWindow { get; set; }
        public KassaControl(MainWindow main, CashBoxPage cashBox)
        {
            InitializeComponent();
            CashBoxPage = cashBox;
            MainWindow = main;
        }

        private void editbtn_Click(object sender, RoutedEventArgs e)
        {
            var lastName = label_name.Text;
            UpdateCashRegister updateWindow = new UpdateCashRegister(MainWindow, CashBoxPage, this);
            updateWindow.addtxt.Select(0, lastName.Length);
            updateWindow.ShowDialog();
        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
            var question = MessageBox.Show("Вы уверены, что хотите удалить касса", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (question == MessageBoxResult.Yes)
            {
                var query = new AppDbContext();
                var cash = query.Кассы.First(x => x.Id == (Guid)idd.Content);
                query.Кассы.Remove(cash);
                query.SaveChanges();
                CashBoxPage.PageCashLoad();
            }
        }

        private void UserControl_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            editbtn.Visibility = Visibility.Visible;
            deletebtn.Visibility = Visibility.Visible;
        }

        private void UserControl_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            editbtn.Visibility= Visibility.Collapsed;
            deletebtn.Visibility= Visibility.Collapsed; 
        }

        private void UserControl_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MainWindow.mainframe.Navigate(new Kassa());
        }
    }
}
