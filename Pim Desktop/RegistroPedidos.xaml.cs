using Newtonsoft.Json;
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
using System.IO;

namespace Pim_Desktop
{
    public partial class RegistroPedidos : Window
    {
        private List<string> listaDePedidos;

        public RegistroPedidos(Pedidos pedidos, List<string> pedidosList)
        {
            InitializeComponent();
            listaDePedidos = pedidosList;
            PedidosListBox.ItemsSource = listaDePedidos;
        }

        private void Excluir_Click(object sender, RoutedEventArgs e)
        {
            // Verifica se algum item está selecionado no ListBox
            if (PedidosListBox.SelectedItem != null)
            {
                string? pedidoSelecionada = PedidosListBox.SelectedItem?.ToString();
                if (pedidoSelecionada != null)
                {
                    listaDePedidos.Remove(pedidoSelecionada); // Remove da lista interna

                    // Atualiza o ListBox
                    PedidosListBox.ItemsSource = null; // Limpa a fonte atual
                    PedidosListBox.ItemsSource = listaDePedidos; // Define novamente a fonte com a lista atualizada


                    SalvarDoacoes(); 

                }
            }
            else
            {
                MensagemPopup.Text = "Selecione uma doação para excluir.";
                AvisoPopup.HorizontalOffset = 260;
                AvisoPopup.VerticalOffset = 50;
                AvisoPopup.IsOpen = true;
                Task.Delay(2500).ContinueWith(_ =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        AvisoPopup.IsOpen = false;
                    });
                });
                return;

            }
        }

        private void SalvarDoacoes()
        {
            string filePedidos = "pedidos.json";
            string json = JsonConvert.SerializeObject(listaDePedidos);
            File.WriteAllText(filePedidos, json);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
