using Pim_Desktop.Models;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Pim_Desktop
{
    
    public partial class TelaInicio : Window
    {
        // Instância do serviço de clima
        private WeatherService weatherService = new WeatherService();


        public TelaInicio()
        {
            InitializeComponent();
            MainFrame.Navigate(new PageInicio(MainFrame));
            MainFrame.SizeChanged += MainFrame_SizeChanged;
            LoadWeather(); // Chama o método para carregar o tempo na inicialização
            InicioButton.IsChecked = true;
            this.Closing += MainWindow_Closing;
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
        private void InicioButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageInicio(MainFrame));
            DeselectOtherButtons((ToggleButton)sender);
            ((ToggleButton)sender).IsChecked = true;
        }

        private void VendasButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageVendas());
            DeselectOtherButtons((ToggleButton)sender);
            ((ToggleButton)sender).IsChecked = true;
        }

        private void DoacoesButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageDoacoes());
            DeselectOtherButtons((ToggleButton)sender);
            ((ToggleButton)sender).IsChecked = true;
        }

        private void FeedbackButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageFeedback());
            DeselectOtherButtons((ToggleButton)sender);
            ((ToggleButton)sender).IsChecked = true;
        }

        private void ConfiguracaoButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageConfiguracao());
            DeselectOtherButtons((ToggleButton)sender);
            ((ToggleButton)sender).IsChecked = true;
        }

        private void MainFrame_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            // Defina o clip com base no tamanho atual do Frame
            RectangleGeometry rectangleGeometry = new RectangleGeometry
            {
                RadiusX = 20,
                RadiusY = 20,
                Rect = new Rect(0, 0, MainFrame.ActualWidth, MainFrame.ActualHeight)
            };
            MainFrame.Clip = rectangleGeometry;
        }

        // Método para carregar o tempo automaticamente
        private async void LoadWeather()
        {
            // Defina as coordenadas (latitude e longitude)
            double latitude = -23.5505; // Exemplo: São Paulo
            double longitude = -46.6333;

            // Obtém os dados do tempo
            WeatherInfo? weatherInfo = await weatherService.GetWeatherAsync(latitude, longitude);

            // Verifica se a informação foi obtida
            if (weatherInfo != null)
            {
                this.DataContext = weatherInfo;
            }
            else
            {
                // Lida com o caso em que o retorno é nulo (API falhou)
                MessageBox.Show("Não foi possível obter as informações meteorológicas. Tente novamente mais tarde.");
            }
        }

        private void DeselectOtherButtons(ToggleButton selectedButton)
        {
            foreach (var child in NavigationStackPanel.Children)
            {
                if (child is ToggleButton toggleButton && toggleButton != selectedButton)
                {
                    toggleButton.IsChecked = false;
                }
            }
        }

        // Evento ao clicar no botão de atualizar tempo
        private void OnUpdateWeatherClick(object sender, RoutedEventArgs e)
        {
            LoadWeather(); // Atualiza as informações meteorológicas
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Verifica se há uma janela aberta e fecha
            var registro = Application.Current.Windows.OfType<RegistroDoacoes>().FirstOrDefault();
            if (registro != null)
            {
                registro.Close();
            }

            var registropedidos = Application.Current.Windows.OfType<RegistroPedidos>().FirstOrDefault();
            if (registropedidos != null)
            {
                registropedidos.Close();
            }

            var pedidos = Application.Current.Windows.OfType<Pedidos>().FirstOrDefault();
            if (pedidos != null)
            {
                pedidos.Close();
            }

            var producao = Application.Current.Windows.OfType<DetalhesProducao>().FirstOrDefault();
            if (producao != null)
            {
                producao.Close();
            }

            var consumo = Application.Current.Windows.OfType<DetalhesConsumo>().FirstOrDefault();
            if (consumo != null)
            {
                consumo.Close();
            }
        }

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            // Verifica se a janela 'RegistroDoacoes' está aberta e a fecha
            var registro = Application.Current.Windows.OfType<RegistroDoacoes>().FirstOrDefault();
            if (registro != null)
            {
                registro.Close();
            }

            var registropedidos = Application.Current.Windows.OfType<RegistroPedidos>().FirstOrDefault();
            if (registropedidos != null)
            {
                registropedidos.Close();
            }

            var pedidos = Application.Current.Windows.OfType<Pedidos>().FirstOrDefault();
            if (pedidos != null)
            {
                pedidos.Close();
            }

            var producao = Application.Current.Windows.OfType<DetalhesProducao>().FirstOrDefault();
            if (producao != null)
            {
                producao.Close();
            }

            var consumo = Application.Current.Windows.OfType<DetalhesConsumo>().FirstOrDefault();
            if (consumo != null)
            {
                consumo.Close();
            }
        }
    }
}
        
    

