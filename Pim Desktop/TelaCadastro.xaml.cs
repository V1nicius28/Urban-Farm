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

namespace Pim_Desktop
{

    public partial class TelaCadastro : Window
    {
        public TelaCadastro()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
                MaximizeButton.Content = "❐"; 
            }
            else
            {
                WindowState = WindowState.Normal;
                MaximizeButton.Content = "⃞"; 
                MaximizeButton.Width = 30;
            }
        }

        private void PersonBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PersonText.Visibility = Visibility.Collapsed;
            UserBorder.BorderBrush = Brushes.White;
        }

        private void Person_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PersonTextBox.Text))
                PersonText.Visibility = Visibility.Visible;
        }

        private void EmailBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EmailText.Visibility = Visibility.Collapsed;
            EmailBorder.BorderBrush = Brushes.White;
        }

        private void EmailBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
                EmailText.Visibility = Visibility.Visible;
        }

        private void PersonTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(PersonTextBox.Text))
            {
                UserError.Visibility = Visibility.Collapsed; 
            }
        
        }

        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsValidEmail(EmailTextBox.Text))
            {
                EmailError.Visibility = Visibility.Collapsed;
            }
            else
            {
                EmailError.Visibility = Visibility.Visible;
            }
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        private void SenhaBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SenhaText.Visibility = Visibility.Collapsed;
            SenhaBorder.BorderBrush = Brushes.White;
        }

        private void SenhaBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SenhaTextBox.Password))
                SenhaText.Visibility = Visibility.Visible;
        }

        private void SenhaBox2_GotFocus(object sender, RoutedEventArgs e)
        {
            SenhaText2.Visibility = Visibility.Collapsed;
            SenhaBorder2.BorderBrush = Brushes.White;
        }

        private void SenhaBox2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SenhaTextBox2.Password))
                SenhaText2.Visibility = Visibility.Visible;               
        }

        private void SenhaPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            SenhaError.Visibility = SenhaTextBox.Password.Length < 6 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void SenhaPasswordBox2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (SenhaTextBox2.Password != SenhaTextBox.Password)
            {
                SenhaError2.Visibility = Visibility.Visible;
            }
            else
            {
                SenhaError2.Visibility = Visibility.Collapsed;
            }
        }

        private void Cadastrar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                TelaInicio fazercadastroWindow = new TelaInicio();
                fazercadastroWindow.Show(); 
                this.Close();
            }
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(PersonTextBox.Text))
            {
                UserBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(237, 66, 69)); 
                UserError.Text = "O campo Nome é obrigatório";  
                UserError.Visibility = Visibility.Visible; 
                return false;
            }

            if (!IsValidEmail(EmailTextBox.Text))
            {
                EmailBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(237, 66, 69)); 
                EmailError.Text = "Email inválido";  
                EmailError.Visibility = Visibility.Visible; 
                return false;
            }

            if (SenhaTextBox.Password.Length < 6)
            {
                SenhaBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(237, 66, 69)); 
                SenhaError.Text = "A senha deve ter pelo menos 6 caracteres"; 
                SenhaError.Visibility = Visibility.Visible; 
                return false;
            }

            if (SenhaTextBox.Password != SenhaTextBox2.Password)
            {
                SenhaBorder2.BorderBrush = new SolidColorBrush(Color.FromRgb(237, 66, 69)); 
                SenhaError2.Text = "As senhas não correspondem";
                SenhaError2.Visibility = Visibility.Visible; 
                return false;
            }

            return true;
        }

        private void TogglePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (SenhaTextBox.Visibility == Visibility.Visible)
            {
                SenhaTextBox.Visibility = Visibility.Collapsed;
                VisiblePasswordBox.Visibility = Visibility.Visible;
                VisiblePasswordBox.Text = SenhaTextBox.Password;

                TogglePasswordButton.Content = new Image
                {
                    Source = new BitmapImage(new Uri("Images/HideVisible.png", UriKind.Relative)),
                    Width = 17,
                    Height = 17
                };
            }
            else
            {
                SenhaTextBox.Visibility = Visibility.Visible;
                VisiblePasswordBox.Visibility = Visibility.Collapsed;
                SenhaTextBox.Password = VisiblePasswordBox.Text;

                TogglePasswordButton.Content = new Image
                {
                    Source = new BitmapImage(new Uri("Images/HideCollapsed.png", UriKind.Relative)),
                    Width = 32,
                    Height = 32
                };
            }
        }

        private void TogglePasswordButton2_Click(object sender, RoutedEventArgs e)
        {
            if (SenhaTextBox2.Visibility == Visibility.Visible)
            {
                SenhaTextBox2.Visibility = Visibility.Collapsed;
                VisiblePasswordBox2.Visibility = Visibility.Visible;
                VisiblePasswordBox2.Text = SenhaTextBox2.Password;

                TogglePasswordButton2.Content = new Image
                {
                    Source = new BitmapImage(new Uri("Images/HideVisible.png", UriKind.Relative)),
                    Width = 17,
                    Height = 17
                };
            }
            else
            {
                SenhaTextBox2.Visibility = Visibility.Visible;
                VisiblePasswordBox2.Visibility = Visibility.Collapsed;
                SenhaTextBox2.Password = VisiblePasswordBox2.Text;

                TogglePasswordButton2.Content = new Image
                {
                    Source = new BitmapImage(new Uri("Images/HideCollapsed.png", UriKind.Relative)),
                    Width = 32,
                    Height = 32
                };
            }
        }

        private void VisiblePasswordBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SenhaTextBox.Visibility == Visibility.Collapsed)
            {
                SenhaTextBox.Password = VisiblePasswordBox.Text; 
            }
        }

        private void VisiblePasswordBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SenhaTextBox2.Visibility == Visibility.Collapsed)
            {
                SenhaTextBox2.Password = VisiblePasswordBox2.Text; 
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            MainWindow fazercadastroWindow = new MainWindow();
            fazercadastroWindow.Show(); 
            this.Close();
        }
    }
}
