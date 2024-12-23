using System.Windows;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace Pim_Desktop
{
    /// <summary>
    /// Lógica interna para RegistroDoacoes.xaml
    /// </summary>
    public partial class RegistroDoacoes : Window
    {
        private PageDoacoes _pageDoacoes; // Adicione a referência à PageDoacoes
        private List<string> listaDeDoacoes;

        public RegistroDoacoes(PageDoacoes pageDoacoes, List<string> doacoes)
        {
            InitializeComponent();
            listaDeDoacoes = doacoes; // Inicializa a lista com as doações
            DoacoesListBox.ItemsSource = listaDeDoacoes; // Define a fonte do ListBox
            _pageDoacoes = pageDoacoes; // Inicialize a referência
        }

        private void Excluir_Click(object sender, RoutedEventArgs e)
        {
            // Verifica se algum item está selecionado no ListBox
            if (DoacoesListBox.SelectedItem != null)
            {
                string? doacaoSelecionada = DoacoesListBox.SelectedItem?.ToString();
                if (doacaoSelecionada != null)
                {
                    listaDeDoacoes.Remove(doacaoSelecionada); // Remove da lista interna

                    // Atualiza o ListBox
                    DoacoesListBox.ItemsSource = null; // Limpa a fonte atual
                    DoacoesListBox.ItemsSource = listaDeDoacoes; // Define novamente a fonte com a lista atualizada

                    // Chama SalvarDoacoes na instância original de PageDoacoes
                    _pageDoacoes.SalvarDoacoes(); // Utilize a referência

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
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

