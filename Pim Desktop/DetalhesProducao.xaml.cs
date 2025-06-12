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
using System.Windows.Threading;
using System.IO;
using Newtonsoft.Json;


namespace Pim_Desktop
{
    public partial class DetalhesProducao : Window
    {
        private string _mesSelecionado;

        public DetalhesProducao(string mes)
        {
            InitializeComponent();
            _mesSelecionado = mes;
            MesTextBox.Text = mes; 
            CarregarDados();
        }

        private void Salvar_Click(object sender, RoutedEventArgs e)
        {
            var dados = new
            {
                Mes = _mesSelecionado,
                QuantidadeProduzida = QuantidadeTextBox.Text,
                Crescimento = CrescimentoTextBox.Text,
                Volume = VolumeTextBox.Text,
                Recursos = RecursosTextBox.Text
            };

            SalvarDados(_mesSelecionado, dados);

            MensagemPopup.Text = $"Dados para o mês de {_mesSelecionado} foram salvos.";
            AvisoPopup.HorizontalOffset = 240;
            AvisoPopup.VerticalOffset = 40;
            AvisoPopup.IsOpen = true;
            Task.Delay(2000).ContinueWith(_ =>
            {
                Dispatcher.Invoke(() =>
                {
                    AvisoPopup.IsOpen = false;
                });
            });
        }


        private void Close_Click(object sender, RoutedEventArgs e)
        {          
            this.Close();         
        }

        private void SalvarDados(string mes, object dados)
        {
            string filePath = $"{mes}_dados_producao.json";
            string jsonData = JsonConvert.SerializeObject(dados, Formatting.Indented);

            File.WriteAllText(filePath, jsonData);
        }

        private void CarregarDados()
        {
            string filePath = $"{_mesSelecionado}_dados_producao.json";

            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                var dados = JsonConvert.DeserializeObject<dynamic>(jsonData);

                QuantidadeTextBox.Text = dados.QuantidadeProduzida;
                CrescimentoTextBox.Text = dados.Crescimento;
                VolumeTextBox.Text = dados.Volume;
                RecursosTextBox.Text = dados.Recursos;
            }
        }
    }
}
    

