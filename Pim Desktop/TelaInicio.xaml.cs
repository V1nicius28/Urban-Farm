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
        private WeatherService weatherService = new WeatherService();


        public TelaInicio()
        {
            InitializeComponent();
            MainFrame.Navigate(new PageInicio(MainFrame));
            MainFrame.SizeChanged += MainFrame_SizeChanged;
            LoadWeather(); 
            InicioButton.IsChecked = true;
            this.Closing += MainWindow_Closing;
        }

        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
                MaximizeButton.Content = "❐"; 
            }
            else
            {
                WindowState = WindowState.Normal;
                MaximizeButton.Content = "⃞"; 
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
            RectangleGeometry rectangleGeometry = new RectangleGeometry
            {
                RadiusX = 20,
                RadiusY = 20,
                Rect = new Rect(0, 0, MainFrame.ActualWidth, MainFrame.ActualHeight)
            };
            MainFrame.Clip = rectangleGeometry;
        }

        private async void LoadWeather()
        {
            double latitude = -23.5505; 
            double longitude = -46.6333;

            WeatherInfo? weatherInfo = await weatherService.GetWeatherAsync(latitude, longitude);

            if (weatherInfo != null)
            {
                this.DataContext = weatherInfo;
            }
            else
            {
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

        private void OnUpdateWeatherClick(object sender, RoutedEventArgs e)
        {
            LoadWeather(); 
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
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
        
    

