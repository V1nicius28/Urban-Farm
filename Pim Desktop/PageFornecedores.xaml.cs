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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Pim_Desktop
{
    public partial class PageFornecedores : Page
    {
        private List<string> listaDePedidos = new List<string>();
        private Frame _mainFrame;

        public PageFornecedores(Frame mainFrame)
        {
            InitializeComponent();
            _mainFrame = mainFrame;
            CarregarPedidos();
        }

        private void Voltar_Click(object sender, RoutedEventArgs e)
        {
            // Navega de volta para a PageInicio
            _mainFrame.Navigate(new PageInicio(_mainFrame));
        }

        private void PesquisaBox_GotFocus(object sender, RoutedEventArgs e)
        {
            LocalText.Visibility = Visibility.Collapsed;
        }

        private void PesquisaBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LocalTextBox.Text))
            {
                LocalText.Visibility = Visibility.Visible;
            }
        }

        private void Pedido_Click(object sender, RoutedEventArgs e)
        {
            // Obtenha o nome do fornecedor a partir do botão clicado
            var button = sender as Button;
            string? nomeFornecedor = button?.Tag?.ToString();

            // Abra a tela Pedidos passando o nome do fornecedor
            Pedidos pedidosWindow = new Pedidos(listaDePedidos, nomeFornecedor);
            pedidosWindow.Show();
        }

        private void RegistroButton_Click(object sender, RoutedEventArgs e)
        {
            if (listaDePedidos.Count == 0)
            {
                // Exibir mensagem se não houver registros
                MensagemPopup.Text = "Nenhum pedido registrado.";
                AvisoPopup.HorizontalOffset = 300;
                AvisoPopup.VerticalOffset = 50;
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

            // Abre a janela de registro dos pedidos e passa a lista dinâmica
            RegistroPedidos registroWindow = new RegistroPedidos(new Pedidos(listaDePedidos, ""), listaDePedidos); // Passa a lista
            registroWindow.Show();
        }

        public void CarregarPedidos()
        {
            string filePedidos = "pedidos.json";
            if (File.Exists(filePedidos))
            {
                string json = File.ReadAllText(filePedidos);
                listaDePedidos = JsonConvert.DeserializeObject<List<string>>(json) ?? new List<string>();
            }
        }

        private void LocalTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string pesquisa = LocalTextBox.Text.ToLower(); // Texto da pesquisa em minúsculas para não diferenciar maiúsculas/minúsculas

            // Percorre todos os itens dentro do WrapPanel
            foreach (var item in WrapPanelFornecedores.Children)
            {
                if (item is Border border)
                {
                    // Procura o TextBlock dentro do Border (onde está o nome do fornecedor)
                    var textBlock = FindTextBlockInBorder(border);

                    if (textBlock != null && textBlock.Text.ToLower().Contains(pesquisa))
                    {
                        border.Visibility = Visibility.Visible; // Exibe o Border se corresponder à pesquisa
                    }
                    else
                    {
                        border.Visibility = Visibility.Collapsed; // Oculta o Border se não corresponder
                    }
                }
            }
        }

        private TextBlock? FindTextBlockInBorder(Border border)
        {
            // Procura o TextBlock dentro do Border, que está dentro de cada item no WrapPanel
            foreach (var child in ((StackPanel)border.Child).Children)
            {
                if (child is TextBlock textBlock)
                {
                    return textBlock;
                }
            }
            return null;
        }

    }
}
