using ShopSystem.Context;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ShopSystem.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        private int eye_counter = 0;
        private string password_change = string.Empty;
        MainWindow MainWindow { get; set; }
        public MainMenuPage(MainWindow main, bool enter = false, string login = "", string parol = "")
        {
            InitializeComponent();
            MainWindow = main;
            EnterTextBox(enter, login, parol);
        }

        private void EnterTextBox(bool enter = false, string login = "", string parol = "")
        {
            if (enter)
            {
                parol_txt.Password = parol;
                register_txt.Text = login;
            }
            else
            {
                if (Properties.Settings.Default.RememberMe)
                {
                    register_txt.Text = Properties.Settings.Default.Owner;
                    parol_txt.Password = Properties.Settings.Default.Password;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainPage.NavigationService.Navigate(new RegistrationPage(MainWindow));
        }

        private void ClickEnter()
        {
            var username = register_txt.Text;
            var password = parol_txt.Password;

            if (username.Length == 0 || password.Length == 0)
            {
                MessageBox.Show("Введите логин и пароль", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var qury = new AppDbContext();

            if (qury.Users.Any(p => p.Name == username))
            {
                try
                {
                    var user = qury.Users.Where(p => p.Name == username).First();
                    var page = new RegistrationPage(MainWindow);
                    var hash = page.GenerateHash(password);

                    if (hash == user.Password)
                    {
                        if ((bool)checkMe.IsChecked!)
                        {
                            Properties.Settings.Default.Password = password;
                            Properties.Settings.Default.Owner = username;
                            Properties.Settings.Default.RememberMe = true;
                            Properties.Settings.Default.Save();
                            Properties.Settings.Default.Name = username;
                        }
                        else
                        {
                            Properties.Settings.Default.Name = username;
                        }
                        MainPage.NavigationService.Navigate(new WorkerPage(MainWindow));
                    }
                    else
                    {
                        MessageBox.Show("Неверный пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void enter_btn_Click(object sender, RoutedEventArgs e)
        {
            ClickEnter();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (eye_counter == 2)
            {
                eye_counter = 0;
            }
            if (eye_counter == 0)
            {
                parol_tx.Visibility = Visibility.Visible;
                parol_txt.Visibility = Visibility.Hidden;
                eye.Source =
                    new BitmapImage(new Uri("pack://application:,,,/Images/eye close.png"));
                eye_counter = 1;
            }
            else if (eye_counter == 1)
            {
                parol_tx.Visibility = Visibility.Hidden;
                parol_txt.Visibility = Visibility.Visible;
                eye.Source =
                    new BitmapImage(new Uri("pack://application:,,,/Images/eye open.png"));
                eye_counter = 2;
            }
        }

        private void parol_tx_TextChanged(object sender, TextChangedEventArgs e)
        {
            parol_txt.Password = parol_tx.Text;
        }

        private void parol_txt_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!parol_txt.IsVisible)
            {
                parol_tx.Text = parol_txt.Password;
            }
        }

        private void MainPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ClickEnter();
            }
        }
    }
}
