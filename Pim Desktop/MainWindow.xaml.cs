using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pim_Desktop
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Closing += MainWindow_Closing;
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

        private void EmailBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EmailText.Visibility = Visibility.Collapsed;
            EmailBorder.BorderBrush = Brushes.White;
        }

        private void EmailBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                EmailText.Visibility = Visibility.Visible;

            }
        }

        private void SenhaBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SenhaText.Visibility = Visibility.Collapsed;
            SenhaBorder.BorderBrush = Brushes.White;
            EmailBorder.BorderBrush = Brushes.White;
        }

        private void SenhaBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SenhaTextBox.Password) && string.IsNullOrWhiteSpace(VisiblePasswordBox.Text))
            {
                SenhaText.Visibility = Visibility.Visible;

            }
        }

        private void Acessar_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string senha = SenhaTextBox.Password;

            if (email == "admin@gmail.com" && senha == "admin")
            {
                TelaInicio novaJanela = new TelaInicio();
                novaJanela.Show(); 
                this.Close(); 
            }
            else
            {
                bool emailInvalido = false;
                bool senhaInvalida = false;

                if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
                {
                    EmailBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(237, 66, 69)); 
                    EmailError.Text = "Email inválido"; 
                    EmailError.Visibility = Visibility.Visible; 
                    emailInvalido = true; 
                }


                if (!emailInvalido && !senhaInvalida)
                {
                    if (senha != "admin" || email != "admin@gmail.com")

                    SenhaBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(237, 66, 69)); 
                    EmailBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(237, 66, 69)); 
                    SenhaError.Text = "Suas credenciais estão incorretas ou expiraram";  
                    SenhaError.Visibility = Visibility.Visible; 
                }
            }
        }

        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                EmailError.Visibility = Visibility.Collapsed; 
                SenhaError.Visibility = Visibility.Collapsed; 
            }
        }

        private void SenhaPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SenhaTextBox.Password))
            {
                SenhaError.Visibility = Visibility.Collapsed;
            }

            if (VisiblePasswordBox.Visibility == Visibility.Collapsed)
            {
                VisiblePasswordBox.Text = SenhaTextBox.Password;
            }
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

        private void VisiblePasswordBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SenhaTextBox.Visibility == Visibility.Collapsed)
            {
                SenhaTextBox.Password = VisiblePasswordBox.Text; 
            }
        }

        private void EsqueceuSenha_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TelaEsqueceuSenha redefinirSenhaWindow = new TelaEsqueceuSenha();
            redefinirSenhaWindow.ShowDialog(); 
        }

        private void Inscrever_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TelaCadastro fazercadastroWindow = new TelaCadastro();
            fazercadastroWindow.Show(); 
            this.Close();
        }

        private void Google_Click(object sender, RoutedEventArgs e)
        {
            LoginGoogle LoginWindow = new LoginGoogle();
            LoginWindow.Show(); 
        }

        private void Convidado_Click(object sender, RoutedEventArgs e)
        {
            TelaInicio LoginWindow = new TelaInicio();
            LoginWindow.Show(); 
            Application.Current.MainWindow.Close(); 
            this.Close();

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var loginGoogle = Application.Current.Windows.OfType<LoginGoogle>().FirstOrDefault();
            if (loginGoogle != null)
            {
                loginGoogle.Close(); 
            }
        }

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {          
            var google = Application.Current.Windows.OfType<LoginGoogle>().FirstOrDefault();
            if (google != null)
            {
                google.Close();
            }
        }

    }
}














