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

        // Evento para o botão Enviar
        private void EnviarButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;

            // Verifica se o e-mail é válido
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@")) // Ajuste para verificar se contém "@" corretamente
            {
                EmailBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(237, 66, 69)); // Borda vermelha
                EmailError.Text = "Email inválido";  // Mensagem personalizada
                EmailError.Visibility = Visibility.Visible; // Mostrar mensagem de erro
            }
            else
            {
                // Aqui você pode adicionar a lógica para enviar o e-mail de redefinição de senha

                // Exibir o Popup com a mensagem
                MostrarPopup($"Um link de redefinição foi enviado para o e-mail: {email}");

            }
        }

        private void MostrarPopup(string mensagem)
        {
            MensagemPopup.Text = mensagem; // Atualiza a mensagem do Popup
            AvisoPopup.IsOpen = true; // Abre o Popup

            // Cria um DispatcherTimer para fechar o Popup após 4 segundos
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(4) // Define o intervalo de 4 segundos
            };
            timer.Tick += (s, args) =>
            {
                AvisoPopup.IsOpen = false; // Fecha o Popup
                timer.Stop(); // Para o timer
                this.Close(); // Fecha a janela
            };
            timer.Start(); // Inicia o timer
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

        // Quando o texto no campo de email é alterado, reseta a mensagem de erro
        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                EmailError.Visibility = Visibility.Collapsed; // Oculta mensagem de erro
            }
        }

        // Evento para o botão Cancelar
        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Fechar a janela
        }
    }
}


