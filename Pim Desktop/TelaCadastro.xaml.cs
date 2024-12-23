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

        // Evento para quando o TextBox "Person" ganha foco
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

        // Evento para quando o TextBox "Email" ganha foco
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

        // Evento para validação de Nome
        private void PersonTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(PersonTextBox.Text))
            {
                UserError.Visibility = Visibility.Collapsed; // Oculta mensagem de erro
            }
        
        }

        // Evento para validação de Email
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

        // Validação de formato de Email
        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        // Evento para quando o PasswordBox "Senha" ganha foco
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

        // Evento para quando o PasswordBox "Confirmar Senha" ganha foco
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

        // Evento para quando o conteúdo do PasswordBox muda
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

        // Evento para o botão de cadastro
        private void Cadastrar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                TelaInicio fazercadastroWindow = new TelaInicio();
                fazercadastroWindow.Show(); // Abre a nova janela e espera o fechamento
                this.Close();
            }
        }

        // Validação de todos os campos
        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(PersonTextBox.Text))
            {
                UserBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(237, 66, 69)); ; // Borda vermelha
                UserError.Text = "O campo Nome é obrigatório";  // Mensagem personalizada
                UserError.Visibility = Visibility.Visible; // Mostrar mensagem de erro
                return false;
            }

            if (!IsValidEmail(EmailTextBox.Text))
            {
                EmailBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(237, 66, 69)); ; // Borda vermelha
                EmailError.Text = "Email inválido";  // Mensagem personalizada
                EmailError.Visibility = Visibility.Visible; // Mostrar mensagem de erro
                return false;
            }

            if (SenhaTextBox.Password.Length < 6)
            {
                SenhaBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(237, 66, 69)); ; // Borda vermelha
                SenhaError.Text = "A senha deve ter pelo menos 6 caracteres"; // Mensagem personalizada
                SenhaError.Visibility = Visibility.Visible; // Mostrar mensagem de erro
                return false;
            }

            if (SenhaTextBox.Password != SenhaTextBox2.Password)
            {
                SenhaBorder2.BorderBrush = new SolidColorBrush(Color.FromRgb(237, 66, 69)); ; // Borda vermelha
                SenhaError2.Text = "As senhas não correspondem";
                SenhaError2.Visibility = Visibility.Visible; // Mostrar mensagem de erro
                return false;
            }

            return true;
        }

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

        private void TogglePasswordButton2_Click(object sender, RoutedEventArgs e)
        {
            if (SenhaTextBox2.Visibility == Visibility.Visible)
            {
                // Mostrar a senha como texto
                SenhaTextBox2.Visibility = Visibility.Collapsed;
                VisiblePasswordBox2.Visibility = Visibility.Visible;
                VisiblePasswordBox2.Text = SenhaTextBox2.Password;

                // Altera a imagem do botão para o ícone "ocultar"
                TogglePasswordButton2.Content = new Image
                {
                    Source = new BitmapImage(new Uri("Images/HideVisible.png", UriKind.Relative)),
                    Width = 17,
                    Height = 17
                };
            }
            else
            {
                // Ocultar a senha
                SenhaTextBox2.Visibility = Visibility.Visible;
                VisiblePasswordBox2.Visibility = Visibility.Collapsed;
                SenhaTextBox2.Password = VisiblePasswordBox2.Text;

                // Altera a imagem do botão para o ícone "mostrar"
                TogglePasswordButton2.Content = new Image
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

        // Método para sincronizar o conteúdo do TextBox visível ao digitar
        private void VisiblePasswordBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Atualizar PasswordBox com o conteúdo do TextBox visível
            if (SenhaTextBox2.Visibility == Visibility.Collapsed)
            {
                SenhaTextBox2.Password = VisiblePasswordBox2.Text; // Atualizar PasswordBox
            }
        }

        // Evento para mudar para a tela de Login
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            MainWindow fazercadastroWindow = new MainWindow();
            fazercadastroWindow.Show(); // Abre a nova janela e espera o fechamento
            this.Close();
        }
    }
}
