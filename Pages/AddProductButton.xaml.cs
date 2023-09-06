using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShopSystem.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddProductButton.xaml
    /// </summary>
    public partial class AddProductButton : UserControl
    {
        public MainWindow MainWindow { get; set; }  
        WorkerPage workerPage;
        string header;
        public Kassa Kassa { get; set; }
        public AddProductButton(MainWindow main, WorkerPage worker, Kassa kassa, string header)
        {
            InitializeComponent();
            workerPage = worker;
            MainWindow = main;
            this.header = header;   
            Kassa = kassa;
        }

        private void Border_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ProductsWindow productsWindow = new ProductsWindow(MainWindow, workerPage, Kassa, header);
            productsWindow.ShowDialog();
        }
    }
}
