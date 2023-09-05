using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Security.Cryptography;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Linq;
using ShopSystem.Context;
using ShopSystem.Models;

namespace ShopSystem.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        private int eye_counter = 0;
        private int eye_counter2 = 0;
        MainWindow MainWindow { get; set; }
        public RegistrationPage(MainWindow main)
        {
            InitializeComponent();
            MainWindow = main;
            pass2.Visibility = Visibility.Collapsed;  
            con_pass2.Visibility = Visibility.Collapsed;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            RegisterPage.NavigationService.Navigate(new MainMenuPage(MainWindow));
        }

        private void ClickRegstr()
        {
            var username = login_txt.Text;
            var password = pass.Password;
            var confirm_password = con_pass.Password;

            var query = new AppDbContext();

            if (username.Length == 0 || (password.Length == 0 && pass2.Text.Length == 0) || (confirm_password.Length == 0 && con_pass2.Text.Length == 0))
            {
                MessageBox.Show("Заполните необходимые поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (query.Users.Any(p => p.Name == username))
            {
                MessageBox.Show("Пользователь уже существует");
                return;
            }

            if (!IsValidPassword(password))
            {
                    valid_lbl.Content = "Минимум 8 символов, минимум одна заглавная буква, одна строчная буква и одна цифра";
                    return;
            }

            if ((password != confirm_password))
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }
            var hash = GenerateHash(password);
            var user = new User()
            {
                Name = username,
                Password = hash,
            };
            try
            {
                query.Users.Add(user);
                query.SaveChanges();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

            NavigationService.Navigate(new MainMenuPage(MainWindow, true, username, password));
        }
        private void reg_btn_Click(object sender, RoutedEventArgs e)
        {
            ClickRegstr();
        }

        public string GenerateHash(string password)
        {
            var hasher = new SHA256Managed();
            var unhashed = System.Text.Encoding.Unicode.GetBytes(password);
            var hashed = hasher.ComputeHash(unhashed);

            var hashedPassword = Convert.ToBase64String(hashed);
            return hashedPassword;
        }

        private bool IsValidPassword(string password)
        {
            var pattern = @"^(?=.*[a-zа-я])(?=.*[A-ZА-Я])(?=.*\d)[А-яa-zA-Z\d]{8,}$";
            if (string.IsNullOrWhiteSpace(password))
                return false;
            var result = Regex.Match(password, pattern);

            if (result.Success)
            {
                return true;
            }
            return false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegisterPage.NavigationService.Navigate(new MainMenuPage(MainWindow));
        }

        private void pass_name_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (eye_counter == 2)
            {
                eye_counter = 0;
            }
            if (eye_counter == 0)
            {
                pass2.Visibility = Visibility.Visible;
                pass.Visibility = Visibility.Hidden;
                pass_name.Source =
                    new BitmapImage(new Uri("pack://application:,,,/Images/eye close.png"));
                eye_counter = 1;
            }
            else if (eye_counter == 1)
            {
                pass2.Visibility = Visibility.Hidden;
                pass.Visibility = Visibility.Visible;
                pass_name.Source =
                    new BitmapImage(new Uri("pack://application:,,,/Images/eye open.png"));
                eye_counter = 2;
            }
        }

        private void conpassname_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (eye_counter2 == 2)
            {
                eye_counter2 = 0;
            }
            if (eye_counter2 == 0)
            {
                con_pass2.Visibility = Visibility.Visible;
                con_pass.Visibility = Visibility.Hidden;
                conpassname.Source =
                    new BitmapImage(new Uri("pack://application:,,,/Images/eye close.png"));
                eye_counter2 = 1;
            }
            else if (eye_counter2 == 1)
            {
                con_pass2.Visibility = Visibility.Hidden;
                con_pass.Visibility = Visibility.Visible;
                conpassname.Source =
                    new BitmapImage(new Uri("pack://application:,,,/Images/eye open.png"));
                eye_counter2 = 2;
            }
        }

        private void pass2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((pass.Password.Length == 0 || pass2.Text.Length == 0))
            {
                valid_lbl.Content = "";
            }
                pass.Password = pass2.Text;
        }

        private void con_pass2_TextChanged(object sender, TextChangedEventArgs e)
        {
            con_pass.Password = con_pass2.Text;
        }

        private void pass_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!pass.IsVisible)
            {
                pass2.Text = pass.Password;
            }
        }

        private void con_pass_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!con_pass.IsVisible)
            {
                con_pass2.Text = con_pass.Password;
            }
        }

        private void login_txt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string pattern = @"^[a-zA-Z0-9]+$";

            // Create a regular expression object with the pattern
            Regex regex = new Regex(pattern);

            // Check if the entered text matches the pattern
            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void pass2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            login_txt_PreviewTextInput(sender, e);
        }

        private void con_pass2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            login_txt_PreviewTextInput(sender, e);
        }

        private void pass_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            login_txt_PreviewTextInput(sender, e);
        }

        private void con_pass_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            login_txt_PreviewTextInput(sender, e);
        }

        private void RegisterPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ClickRegstr();
            }
        }
    }
}
