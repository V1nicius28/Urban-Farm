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
using System.Windows.Controls.Primitives;


namespace Pim_Desktop
{
    /// <summary>
    /// Interação lógica para PageIluminacao.xam
    /// </summary>
    public partial class PageIluminacao : Page
    {
        private Frame _mainFrame;

        public PageIluminacao(Frame mainFrame)
        {
            InitializeComponent();
            _mainFrame = mainFrame; // Armazena a referência ao Frame
        }

        private void Voltar_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new PageInicio(_mainFrame));
        }

        private void Crescimento_Click(object sender, RoutedEventArgs e)
        {
            DesmarcarOutros((ToggleButton)sender);
            Slider1.Value = 75;
            Slider2.Value = 85;
            Slider3.Value = 75;
        }

        private void Economia_Click(object sender, RoutedEventArgs e)
        {
            DesmarcarOutros((ToggleButton)sender);
            Slider1.Value = 50;
            Slider2.Value = 40;
            Slider3.Value = 45;
        }

        private void Floracao_Click(object sender, RoutedEventArgs e)
        {
            DesmarcarOutros((ToggleButton)sender);
            Slider1.Value = 95;
            Slider2.Value = 75;
            Slider3.Value = 80;
        }

        private void Noturno_Click(object sender, RoutedEventArgs e)
        {
            DesmarcarOutros((ToggleButton)sender);
            Slider1.Value = 25;
            Slider2.Value = 40;
            Slider3.Value = 20;
        }

        private void Sazonal_Click(object sender, RoutedEventArgs e)
        {
            DesmarcarOutros((ToggleButton)sender);
            Slider1.Value = 40;
            Slider2.Value = 55;
            Slider3.Value = 60;
        }

        private void Personalizado_Click(object sender, RoutedEventArgs e)
        {
            DesmarcarOutros((ToggleButton)sender);
            Slider1.Value = 65;
            Slider2.Value = 35;
            Slider3.Value = 80;
        }

        private void DesmarcarOutros(ToggleButton selectedButton)
        {
            foreach (var child in ButtonPanel.Children)
            {
                if (child is ToggleButton button && button != selectedButton)
                {
                    button.IsChecked = false;
                }
            }

            selectedButton.IsChecked = true; 
        }

        private void Azul_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)Ligar.IsChecked)
            {
                DesmarcarOutrosCor((ToggleButton)sender);
                LuzImage.Source = new BitmapImage(new Uri("Images/LuzAzul.png", UriKind.Relative));
            }
            else
            {
                ((ToggleButton)sender).IsChecked = false;
            }
        }


        private void Vermelho_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)Ligar.IsChecked)
            {
                DesmarcarOutrosCor((ToggleButton)sender);
                LuzImage.Source = new BitmapImage(new Uri("Images/LuzVermelha.png", UriKind.Relative));
            }
            else
            {
                ((ToggleButton)sender).IsChecked = false;
            }
        }

        private void Amarelo_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)Ligar.IsChecked)
            {
                DesmarcarOutrosCor((ToggleButton)sender);
                LuzImage.Source = new BitmapImage(new Uri("Images/LuzAmarela.png", UriKind.Relative));
            }
            else
            {
                ((ToggleButton)sender).IsChecked = false;
            }
        }

        private void Laranja_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)Ligar.IsChecked)
            {
                DesmarcarOutrosCor((ToggleButton)sender);
                LuzImage.Source = new BitmapImage(new Uri("Images/LuzLaranja.png", UriKind.Relative));
            }
            else
            {
                ((ToggleButton)sender).IsChecked = false;
            }
        }

        private void Verde_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)Ligar.IsChecked)
            {
                DesmarcarOutrosCor((ToggleButton)sender);
                LuzImage.Source = new BitmapImage(new Uri("Images/LuzVerde.png", UriKind.Relative));
            }
            else
            {
                ((ToggleButton)sender).IsChecked = false;
            }
        }

        private void Rosa_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)Ligar.IsChecked)
            {
                DesmarcarOutrosCor((ToggleButton)sender);
                LuzImage.Source = new BitmapImage(new Uri("Images/LuzRosa.png", UriKind.Relative));
            }
            else
            {
                ((ToggleButton)sender).IsChecked = false;
            }
        }

        private void Branco_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)Ligar.IsChecked)
            {
                DesmarcarOutrosCor((ToggleButton)sender);
                LuzImage.Source = new BitmapImage(new Uri("Images/LuzBranca.png", UriKind.Relative));
            }
            else
            {
                ((ToggleButton)sender).IsChecked = false;
            }
        }

        private void Roxo_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)Ligar.IsChecked)
            {
                DesmarcarOutrosCor((ToggleButton)sender);
                LuzImage.Source = new BitmapImage(new Uri("Images/LuzRoxa.png", UriKind.Relative));
            }
            else
            {
                ((ToggleButton)sender).IsChecked = false;
            }
        }

        private void Ligar_Checked(object sender, RoutedEventArgs e)
        {
            // Aguarda até que o elemento BrancoButton esteja disponível
            Dispatcher.BeginInvoke(new Action(() =>
            {
                if (Branco != null)
                {
                    LuzImage.Source = new BitmapImage(new Uri("Images/LuzBranca.png", UriKind.Relative));
                    DesmarcarOutrosCor(Branco);
                }
            }), System.Windows.Threading.DispatcherPriority.Loaded);
        }



        private void Ligar_Unchecked(object sender, RoutedEventArgs e)
        {
            LuzImage.Source = new BitmapImage(new Uri("Images/Desligado.png", UriKind.Relative));

            // Desmarcar todos os botões de cor
            foreach (var child in ButtonCorPanel.Children)
            {
                if (child is ToggleButton button)
                {
                    button.IsChecked = false; // Desmarca cada botão de cor
                }
            }
        }


        private void DesmarcarOutrosCor(ToggleButton selectedButton)
        {
            foreach (var child in ButtonCorPanel.Children)
            {
                if (child is ToggleButton button && button != selectedButton)
                {
                    button.IsChecked = false;
                }
            }

            selectedButton.IsChecked = true; 
        }
    }
}
