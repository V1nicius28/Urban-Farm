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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.IO;


namespace Pim_Desktop
{

    public partial class PageDoacoes : Page
    {
        private List<string> listaDeDoacoes = new List<string>();

        public PageDoacoes()
        {
            InitializeComponent();
            CarregarDoacoes();
        }

        private void LocalBox_GotFocus(object sender, RoutedEventArgs e)
        {
            LocalText.Visibility = Visibility.Collapsed;
        }

        private void LocalBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LocalTextBox.Text))
            {
                LocalText.Visibility = Visibility.Visible;
            }
        }

        private void AlimentoBox_GotFocus(object sender, RoutedEventArgs e)
        {
            AlimentoText.Visibility = Visibility.Collapsed;
        }

        private void AlimentoBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AlimentoTextBox.Text))
            {
                AlimentoText.Visibility = Visibility.Visible;
            }
        }

        private void DataBox_GotFocus(object sender, RoutedEventArgs e)
        {
            DataText.Visibility = Visibility.Collapsed;
        }

        private void DataBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DataTextBox.Text))
            {
                DataText.Visibility = Visibility.Visible;
            }
        }

        private void Enviar_Click(object sender, RoutedEventArgs e)
        {
            string local = LocalTextBox.Text;
            string alimento = AlimentoTextBox.Text;
            string data = DataTextBox.Text;
            string horario = HorarioTextBox.Text;
            string quantidade = QuantTextBox.Text;

            if (string.IsNullOrWhiteSpace(local) ||
                string.IsNullOrWhiteSpace(alimento) ||
                string.IsNullOrWhiteSpace(data) ||
                string.IsNullOrWhiteSpace(horario) ||
                string.IsNullOrWhiteSpace(quantidade))
            {
                MensagemPopup.Text = "Por favor, preencha todos os campos.";
                AvisoPopup.HorizontalOffset = 260;
                AvisoPopup.VerticalOffset = 80;
                AvisoPopup.IsOpen = true;
                Task.Delay(1500).ContinueWith(_ =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        AvisoPopup.IsOpen = false;
                    });
                });
                return;
            }

            Mensagem confirmacao = new Mensagem($"Local: {local}\nAlimento: {alimento}\nData: {data}\nHorário: {horario}\nQuantidade: {quantidade}");
            confirmacao.ShowDialog();

            if (confirmacao.Resultado)
            {
                string registroDoacao = $"Local: {local}, Alimento: {alimento}, Data: {data}, Horário: {horario}, Quantidade: {quantidade}";
                listaDeDoacoes.Add(registroDoacao);

                SalvarDoacoes();

                MensagemPopup.Text = "Enviado!";
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
            }
        }

        private void RegistroButton_Click(object sender, RoutedEventArgs e)
        {
            if (listaDeDoacoes.Count == 0)
            {
                MensagemPopup.Text = "Nenhuma doação registrada.";
                AvisoPopup.HorizontalOffset = 300;
                AvisoPopup.VerticalOffset = 80;
                AvisoPopup.IsOpen = true;
                Task.Delay(1500).ContinueWith(_ =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        AvisoPopup.IsOpen = false;
                    });
                });
                return;
            }

            RegistroDoacoes registroWindow = new RegistroDoacoes(this, listaDeDoacoes);
            registroWindow.Show();
        }

        public void SalvarDoacoes()
        {
            string filePath = "doacoes.json";
            string json = JsonConvert.SerializeObject(listaDeDoacoes);
            File.WriteAllText(filePath, json);
        }

        public void CarregarDoacoes()
        {
            string filePath = "doacoes.json";
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                listaDeDoacoes = JsonConvert.DeserializeObject<List<string>>(json) ?? new List<string>();
            }
        }
    }
}

