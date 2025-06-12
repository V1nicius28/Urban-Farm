using System.Windows;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace Pim_Desktop
{
    public partial class RegistroDoacoes : Window
    {
        private PageDoacoes _pageDoacoes; 
        private List<string> listaDeDoacoes;

        public RegistroDoacoes(PageDoacoes pageDoacoes, List<string> doacoes)
        {
            InitializeComponent();
            listaDeDoacoes = doacoes; 
            DoacoesListBox.ItemsSource = listaDeDoacoes; 
            _pageDoacoes = pageDoacoes; 
        }

        private void Excluir_Click(object sender, RoutedEventArgs e)
        {
            if (DoacoesListBox.SelectedItem != null)
            {
                string? doacaoSelecionada = DoacoesListBox.SelectedItem?.ToString();
                if (doacaoSelecionada != null)
                {
                    listaDeDoacoes.Remove(doacaoSelecionada); 

                    DoacoesListBox.ItemsSource = null; 
                    DoacoesListBox.ItemsSource = listaDeDoacoes; 

                    _pageDoacoes.SalvarDoacoes(); 

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

