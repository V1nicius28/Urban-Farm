using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Pim_Desktop
{
 
    public partial class TelaEsqueceuSenha : Window
    {
        public TelaEsqueceuSenha()
        {
            InitializeComponent();
        }

        private void EnviarButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@")) 
            {
                EmailBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(237, 66, 69)); 
                EmailError.Text = "Email inválido";  
                EmailError.Visibility = Visibility.Visible; 
            }
            else
            {
                MostrarPopup($"Um link de redefinição foi enviado para o e-mail: {email}");

            }
        }

        private void MostrarPopup(string mensagem)
        {
            MensagemPopup.Text = mensagem; 
            AvisoPopup.IsOpen = true; 

            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(4) 
            };
            timer.Tick += (s, args) =>
            {
                AvisoPopup.IsOpen = false; 
                timer.Stop(); 
                this.Close(); 
            };
            timer.Start(); 
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

        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                EmailError.Visibility = Visibility.Collapsed; 
            }
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }
    }
}


