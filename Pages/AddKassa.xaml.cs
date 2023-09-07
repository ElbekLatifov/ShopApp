using ShopSystem.Context;
using ShopSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для AddKassa.xaml
    /// </summary>
    public partial class AddKassa : Window
    {
        WorkerPage worrker;
        CashBoxPage cashBoxPage;
        public AddKassa(MainWindow main, WorkerPage worker, CashBoxPage cashBox)
        {
            InitializeComponent();
            Owner = main;
            worrker = worker;
            cashBoxPage = cashBox;
        }

        private void AddKassaa()
        {
            var cashname = addtxt.Text;
            var db = new AppDbContext();
            var cashs = db.Кассы.Where(p=>p.ShopId == worrker.ShopId).ToList();
            if(cashs.Any(p=>p.Name == cashname))
            {
                MessageBox.Show("Касса с таким названием уже существует");
                return;
            }
            var cashbox = new Cashbox()
            {
                Name = cashname,
                ShopId = worrker.ShopId,
                CahsregisterId = GetBarcode(Guid.NewGuid())
            };
            db.Кассы.Add(cashbox);
            db.SaveChanges();
            cashbox.CahsregisterId = GetBarcode(cashbox.Id);
            db.Кассы.Update(cashbox);
            db.SaveChanges();
            cashBoxPage.PageCashLoad();
            Close();
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            AddKassaa();
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

        private string GetBarcode(Guid path)
        {
            byte[] generatedBarcode = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(path.ToString()!));
            var res = BitConverter.ToUInt32(generatedBarcode, 0) % 100000;

            string barcode = CalculateEan13("43", res.ToString());
            return barcode.ToString();
        }

        public static string CalculateEan13(string manufacturer, string product)
        {
            string temp = $"{manufacturer}{product}";
            int sum = 0;
            int digit = 0;

            for (int i = temp.Length; i >= 1; i--)
            {
                digit = Convert.ToInt32(temp.Substring(i - 1, 1));

                if (i % 2 == 0)
                {
                    sum += digit * 3;
                }
                else
                {
                    sum += digit * 1;
                }
            }
            int checkSum = (10 - (sum % 10)) % 10;
            return $"{temp}{checkSum}";
        }
    }
}
