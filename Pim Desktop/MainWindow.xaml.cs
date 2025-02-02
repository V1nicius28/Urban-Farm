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

        // Evento para arrastar a janela ao clicar e arrastar na barra de título
        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        // Evento para fechar a janela
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Evento para minimizar a janela
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        // Evento para maximizar ou restaurar a janela
        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
                MaximizeButton.Content = "❐"; // Altera o conteúdo do botão para restaurar
            }
            else
            {
                WindowState = WindowState.Normal;
                MaximizeButton.Content = "⃞"; // Altera o conteúdo do botão para maximizar
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
            // Obter as credenciais
            string email = EmailTextBox.Text;
            string senha = SenhaTextBox.Password;

            // Validação simples de credenciais
            if (email == "admin@gmail.com" && senha == "admin")
            {
                // Abrir a nova janela se as credenciais forem válidas
                TelaInicio novaJanela = new TelaInicio();
                novaJanela.Show(); // Abre a nova janela
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

            // Se o campo de senha estiver visível, atualize o TextBox visível
            if (VisiblePasswordBox.Visibility == Visibility.Collapsed)
            {
                VisiblePasswordBox.Text = SenhaTextBox.Password; // Atualiza o TextBox visível
            }
        }

        // Método para alternar entre mostrar/ocultar senha
        private void TogglePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (SenhaTextBox.Visibility == Visibility.Visible)
            {
                // Mostrar a senha como texto
                SenhaTextBox.Visibility = Visibility.Collapsed;
                VisiblePasswordBox.Visibility = Visibility.Visible;
                VisiblePasswordBox.Text = SenhaTextBox.Password;

                // Altera a imagem do botão para o ícone "ocultar"
                TogglePasswordButton.Content = new Image
                {
                    Source = new BitmapImage(new Uri("Images/HideVisible.png", UriKind.Relative)),
                    Width = 17,
                    Height = 17
                };
            }
            else
            {
                // Ocultar a senha
                SenhaTextBox.Visibility = Visibility.Visible;
                VisiblePasswordBox.Visibility = Visibility.Collapsed;
                SenhaTextBox.Password = VisiblePasswordBox.Text;

                // Altera a imagem do botão para o ícone "mostrar"
                TogglePasswordButton.Content = new Image
                {
                    Source = new BitmapImage(new Uri("Images/HideCollapsed.png", UriKind.Relative)),
                    Width = 32,
                    Height = 32
                };
            }
        }

        // Método para sincronizar o conteúdo do TextBox visível ao digitar
        private void VisiblePasswordBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Atualizar PasswordBox com o conteúdo do TextBox visível
            if (SenhaTextBox.Visibility == Visibility.Collapsed)
            {
                SenhaTextBox.Password = VisiblePasswordBox.Text; // Atualizar PasswordBox
            }
        }

        private void EsqueceuSenha_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TelaEsqueceuSenha redefinirSenhaWindow = new TelaEsqueceuSenha();
            redefinirSenhaWindow.ShowDialog(); // Abre a nova janela e espera o fechamento
        }

        private void Inscrever_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TelaCadastro fazercadastroWindow = new TelaCadastro();
            fazercadastroWindow.Show(); // Abre a nova janela e espera o fechamento
            this.Close();
        }

        private void Google_Click(object sender, RoutedEventArgs e)
        {
            LoginGoogle LoginWindow = new LoginGoogle();
            LoginWindow.Show(); // Abre a nova janela e espera o fechamento
        }

        private void Convidado_Click(object sender, RoutedEventArgs e)
        {
            TelaInicio LoginWindow = new TelaInicio();
            LoginWindow.Show(); // Abre a nova janela e espera o fechamento
            Application.Current.MainWindow.Close(); // Fecha a MainWindow
            this.Close();

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Verifica se há uma janela do tipo LoginGoogle aberta e a fecha
            var loginGoogle = Application.Current.Windows.OfType<LoginGoogle>().FirstOrDefault();
            if (loginGoogle != null)
            {
                loginGoogle.Close(); // Fecha a janela de Login Google
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














