using System.Windows;
using System.Windows.Controls;
using System.IO.IsolatedStorage;
using System.IO;
using Newtonsoft.Json;

namespace Pim_Desktop
{
    public partial class PageConfiguracao : Page
    {
        public class UserData
        {
            public string Nome { get; set; }
            public string Usuario { get; set; }
            public string Email { get; set; }
            public string Telefone { get; set; }
            public string Pais { get; set; }
            public string Cidade { get; set; }
            public string Estado { get; set; }
        }

        public PageConfiguracao()
        {
            InitializeComponent();
            Loaded += PageConfiguracao_Loaded; // Adiciona o evento Loaded para garantir que o UpdateThemeSelection seja chamado
            LoadData();
        }

        private void PageConfiguracao_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateThemeSelection(); // Atualiza a seleção do RadioButton sempre que a página é carregada
            SalvarButton.Visibility = Visibility.Collapsed; // Garante que o botão comece como Collapsed
        }

        private void UpdateThemeSelection()
        {
            // Atualiza a seleção dos RadioButtons com base no tema salvo
            string currentTheme = ((App)Application.Current).SelectedTheme;
            DefaultRadioButton.IsChecked = currentTheme == "Dark";
            LightRadioButton.IsChecked = currentTheme == "Light";
        }

        private void ThemeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton && radioButton.IsChecked == true)
            {
                // Atualiza o tema selecionado com base no RadioButton marcado
                ((App)Application.Current).SelectedTheme = radioButton.Name == "DefaultRadioButton" ? "Dark" : "Light";
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SalvarButton.Visibility = Visibility.Visible;
        }

        private void SalvarButton_Click(object sender, RoutedEventArgs e)
        {
            SaveData();
            MensagemPopup.Text = "Informações salvas!";
            AvisoPopup.HorizontalOffset = 338;
            AvisoPopup.VerticalOffset = 80;
            AvisoPopup.IsOpen = true;
            Task.Delay(1500).ContinueWith(_ =>
            {
                Dispatcher.Invoke(() =>
                {
                    AvisoPopup.IsOpen = false;
                });
            });
            SalvarButton.Visibility = Visibility.Collapsed;
        }

        private void SaveData()
        {
            var data = new UserData
            {
                Nome = NomeTextBox.Text,
                Usuario = UsuarioTextBox.Text,
                Email = EmailTextBox.Text,
                Telefone = TelefoneTextBox.Text,
                Pais = PaisTextBox.Text,
                Cidade = CidadeTextBox.Text,
                Estado = EstadoTextBox.Text
            };

            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("dataprofile.json", FileMode.Create, storage))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                string jsonData = JsonConvert.SerializeObject(data);
                writer.Write(jsonData);
            }
        }


        private void LoadData()
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (storage.FileExists("dataprofile.json"))
                {
                    using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("dataprofile.json", FileMode.Open, storage))
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string jsonData = reader.ReadToEnd();
                        var data = JsonConvert.DeserializeObject<UserData>(jsonData);

                        NomeTextBox.Text = data?.Nome;
                        UsuarioTextBox.Text = data?.Usuario;
                        EmailTextBox.Text = data?.Email;
                        TelefoneTextBox.Text = data?.Telefone;
                        PaisTextBox.Text = data?.Pais;
                        CidadeTextBox.Text = data?.Cidade;
                        EstadoTextBox.Text = data?.Estado;
                    }
                }
            }
        }

        private void SairButton_Click(object sender, RoutedEventArgs e)
        {
            // Abre a caixa de diálogo personalizada com uma mensagem específica
            Mensagem confirmacao = new Mensagem("Tem certeza que\n deseja sair?");
            confirmacao.ShowDialog();

            if (confirmacao.Resultado)
            {
                MainWindow novaJanela = new MainWindow();
                novaJanela.Show();
                Window.GetWindow(this).Close();
            }
        }

        private void DeletarButton_Click(object sender, RoutedEventArgs e)
        {
            // Abre a caixa de diálogo personalizada com uma mensagem específica
            Mensagem confirmacao = new Mensagem("Deletar a conta?");
            confirmacao.ShowDialog();

            if (confirmacao.Resultado)
            {
                MainWindow novaJanela = new MainWindow();
                novaJanela.Show();
                Window.GetWindow(this).Close();
            }
        }

        private void ReportarButton_Click(object sender, RoutedEventArgs e)
        {
            Report novaJanela = new Report();
            bool? result = novaJanela.ShowDialog();

            if (result == true)
            {
                string ?descricaoErro = novaJanela.Comentario; // Obtém o comentário da janela Report

                MensagemPopup.Text = "Erro reportado com sucesso!";
                AvisoPopup.HorizontalOffset = 250;
                AvisoPopup.VerticalOffset = 50;
                AvisoPopup.IsOpen = true;
                Task.Delay(2000).ContinueWith(_ =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        AvisoPopup.IsOpen = false;
                    });
                });
            }
        }
    }
}





