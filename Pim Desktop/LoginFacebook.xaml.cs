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
using System.Windows.Shapes;

namespace Pim_Desktop
{
    public partial class LoginFacebook : Window
    {
        public LoginFacebook()
        {
            InitializeComponent();
        }

        private void EmailBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EmailText.Visibility = Visibility.Collapsed;
            EmailBorder.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#90949C");

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
            EmailBorder.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#90949C");
            SenhaBorder.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#90949C");
        }

        private void SenhaBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SenhaTextBox.Password))
            {
                SenhaText.Visibility = Visibility.Visible;
            }
        }

        // Quando o texto no campo de email é alterado, reseta a mensagem de erro
        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                EmailError.Visibility = Visibility.Collapsed; // Oculta mensagem de erro
                SenhaError.Visibility = Visibility.Collapsed; // Oculta mensagem de erro
            }
        }

        // Quando o texto no campo de senha é alterado, reseta a mensagem de erro
        private void SenhaPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SenhaTextBox.Password))
            {
                SenhaError.Visibility = Visibility.Collapsed; // Oculta mensagem de erro
            }
        }

        private void Entrar_Click(object sender, RoutedEventArgs e)
        {
            // Obter as credenciais
            string email = EmailTextBox.Text;
            string senha = SenhaTextBox.Password;

            // Validação simples de credenciais
            if (email == "admin@gmail.com" && senha == "admin")
            {
                // Abrir a nova janela se as credenciais forem válidas
                TelaInicio novaJanela = new TelaInicio();
                novaJanela.Show(); // Abre a nova janela
                Application.Current.MainWindow.Close();
                this.Close(); // Fecha a janela de login
            }
            else
            {
                // Exibir mensagens de erro apropriadas
                bool emailInvalido = false;
                bool senhaInvalida = false;

                // Exibir mensagens de erro apropriadas
                if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
                {
                    EmailBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(237, 66, 69)); ; // Borda vermelha
                    EmailError.Text = "Email inválido";  // Mensagem personalizada
                    EmailError.Visibility = Visibility.Visible; // Mostrar mensagem de erro
                    emailInvalido = true; // Marcar email como inválido
                }


                // Se ambos os campos estão preenchidos, verificar credenciais
                if (!emailInvalido && !senhaInvalida)
                {
                    if (senha != "admin" || email != "admin@gmail.com")

                    SenhaBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(237, 66, 69)); ; // Borda vermelha
                    EmailBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(237, 66, 69)); ; // Borda vermelha
                    SenhaError.Text = "Suas credenciais estão incorretas ou expiraram";  // Mensagem personalizada
                    SenhaError.Visibility = Visibility.Visible; // Mostrar mensagem de erro
                }
            }
        }
    }
}
