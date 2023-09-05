using ShopSystem.Context;
using ShopSystem.Models;
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
    /// Логика взаимодействия для CashBoxPage.xaml
    /// </summary>
    public partial class CashBoxPage : Page
    {
        WorkerPage workerPage;
        MainWindow Main;
        public CashBoxPage(MainWindow main, WorkerPage worker)
        {
            InitializeComponent();
            Main = main;
            workerPage = worker;
            PageCashLoad();
        }

        public void PageCashLoad()
        {
            var db = new AppDbContext();
            var cashs = db.Кассы.Where(p=>p.ShopId == workerPage.ShopId).ToList();
            var cashslist = new List<KassaControl>();

            foreach (var cash in cashs)
            {
                KassaControl cashModel = new KassaControl(Main, this);

                cashModel.Width = 200;
                cashModel.Height = 70;
                cashModel.CashName = cash.Name;
                cashModel.CashId = cash.Id;
                cashslist.Add(cashModel);
            }

            cashlists.ItemsSource = cashslist;
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            AddKassa addKassa = new AddKassa(Main, workerPage, this);
            addKassa.ShowDialog();
        }
    }
}
