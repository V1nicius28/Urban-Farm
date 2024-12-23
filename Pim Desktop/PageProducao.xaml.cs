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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Pim_Desktop
{
    public partial class PageProducao : Page
    {
        private Frame _mainFrame;
        private string _mesSelecionado;

        public PageProducao(Frame mainFrame)
        {
            InitializeComponent();
            _mainFrame = mainFrame; // Armazena a referência ao Frame
        }

        private void Voltar_Click(object sender, RoutedEventArgs e)
        {
            // Navega de volta para a PageInicio
            _mainFrame.Navigate(new PageInicio(_mainFrame));
        }
        private void Gerar_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_mesSelecionado))
            {
                var detalhesProducao = new DetalhesProducao(_mesSelecionado);
                detalhesProducao.Show();
            }
            else
            {
            MensagemPopup.Text = "Por favor, selecione um mês antes de gerar.";
            AvisoPopup.HorizontalOffset = 260;
            AvisoPopup.VerticalOffset = 40;
            AvisoPopup.IsOpen = true;
            Task.Delay(2000).ContinueWith(_ =>
            {
                Dispatcher.Invoke(() =>
                {
                    AvisoPopup.IsOpen = false;
                });
            });
            return;
        }

        }
        private void Janeiro_Click(object sender, RoutedEventArgs e) => DesmarcarOutros("Janeiro");
        private void Fevereiro_Click(object sender, RoutedEventArgs e) => DesmarcarOutros("Fevereiro");
        private void Março_Click(object sender, RoutedEventArgs e) => DesmarcarOutros("Março");
        private void Abril_Click(object sender, RoutedEventArgs e) => DesmarcarOutros("Abril");
        private void Maio_Click(object sender, RoutedEventArgs e) => DesmarcarOutros("Maio");
        private void Junho_Click(object sender, RoutedEventArgs e) => DesmarcarOutros("Junho");
        private void Julho_Click(object sender, RoutedEventArgs e) => DesmarcarOutros("Julho");
        private void Agosto_Click(object sender, RoutedEventArgs e) => DesmarcarOutros("Agosto");
        private void Setembro_Click(object sender, RoutedEventArgs e) => DesmarcarOutros("Setembro");
        private void Outubro_Click(object sender, RoutedEventArgs e) => DesmarcarOutros("Outubro");
        private void Novembro_Click(object sender, RoutedEventArgs e) => DesmarcarOutros("Novembro");
        private void Dezembro_Click(object sender, RoutedEventArgs e) => DesmarcarOutros("Dezembro");

        private void DesmarcarOutros(string mes)
        {
            // Desmarcar outros meses e marcar o mês selecionado
            Janeiro.IsChecked = mes == "Janeiro";
            Fevereiro.IsChecked = mes == "Fevereiro";
            Março.IsChecked = mes == "Março";
            Abril.IsChecked = mes == "Abril";
            Maio.IsChecked = mes == "Maio";
            Junho.IsChecked = mes == "Junho";
            Julho.IsChecked = mes == "Julho";
            Agosto.IsChecked = mes == "Agosto";
            Setembro.IsChecked = mes == "Setembro";
            Outubro.IsChecked = mes == "Outubro";
            Novembro.IsChecked = mes == "Novembro";
            Dezembro.IsChecked = mes == "Dezembro";

            _mesSelecionado = mes; // Salva o mês selecionado
        }
    }
}
