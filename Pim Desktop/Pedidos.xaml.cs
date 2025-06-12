using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.IO;

namespace Pim_Desktop
{
    public partial class Pedidos : Window
    {
        private List<string> listaDePedidos;
        private string nomeFornecedor;

        public Pedidos(List<string> pedidos, string fornecedor)
        {
            InitializeComponent();
            listaDePedidos = pedidos;
            nomeFornecedor = fornecedor;
            FornecedorTextBlock.Text = nomeFornecedor;
        }


        private void Enviar_Click(object sender, RoutedEventArgs e)
        {
            string local = LocalTextBox.Text;
            string Semente = SementeTextBox.Text;
            string Solo = SoloTextBox.Text;
            string Fertilizantes = FertilizantesTextBox.Text;
            string quantsemente = QuantSementes.Text;
            string quantsolo = QuantSolo.Text;
            string quantadubo = QuantAdubo.Text;

            if (string.IsNullOrWhiteSpace(local) ||
                string.IsNullOrWhiteSpace(Semente) ||
                string.IsNullOrWhiteSpace(Solo) ||
                string.IsNullOrWhiteSpace(Fertilizantes) ||
                string.IsNullOrWhiteSpace(quantsemente) ||
                string.IsNullOrWhiteSpace(quantsolo) ||
                string.IsNullOrWhiteSpace(quantadubo))
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

            Mensagem confirmacao = new Mensagem($"Fornecedor: {nomeFornecedor}\nLocal: {local}\nSementes e Mudas: {Semente}\nQuantidade: {quantsemente}\nSubstratos e solo especial: {Solo}\nQuantidade: {quantsolo}\nFertilizantes e adubos: {Fertilizantes}\nQuantidade: {quantadubo}");
            confirmacao.ShowDialog();

            if (confirmacao.Resultado)
            {
                string registroPedidos = $"Fornecedor: {nomeFornecedor}\nLocal: {local}\nSementes e Mudas: {Semente}\nQuantidade: {quantsemente}\nSubstratos e solo especial: {Solo}\nQuantidade: {quantsolo}\nFertilizantes e adubos: {Fertilizantes}\nQuantidade: {quantadubo}";
                listaDePedidos.Add(registroPedidos);

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


        public void SalvarDoacoes()
        {
            string filePedidos = "pedidos.json";
            string json = JsonConvert.SerializeObject(listaDePedidos);
            File.WriteAllText(filePedidos, json);
        }



        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void SementeBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SementeText.Visibility = Visibility.Collapsed;
        }

        private void SementeBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SementeTextBox.Text))
            {
                SementeText.Visibility = Visibility.Visible;
            }
        }

        private void SoloBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SoloText.Visibility = Visibility.Collapsed;
        }

        private void SoloBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SoloTextBox.Text))
            {
                SoloText.Visibility = Visibility.Visible;
            }
        }

        private void FertilizantesBox_GotFocus(object sender, RoutedEventArgs e)
        {
            FertilizantesText.Visibility = Visibility.Collapsed;
        }

        private void FertilizantesBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FertilizantesTextBox.Text))
            {
                FertilizantesText.Visibility = Visibility.Visible;
            }
        }
    }
}
