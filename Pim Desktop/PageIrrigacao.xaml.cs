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

namespace Pim_Desktop
{
    public partial class PageIrrigacao : Page
    {
        private Frame _mainFrame;

        public PageIrrigacao(Frame mainFrame)
        {
            InitializeComponent();
            _mainFrame = mainFrame;
            ComboBox4.SelectionChanged += ComboBox_SelectionChanged;
        }

        private void Voltar_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new PageInicio(_mainFrame));
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ComboBox4.SelectedIndex)
            {
                case 0:
                    MinhaImagem.Source = new BitmapImage(new Uri("Images/Agua1.png", UriKind.Relative));
                    break;
                case 1:
                    MinhaImagem.Source = new BitmapImage(new Uri("Images/Agua2.png", UriKind.Relative));
                    break;
                case 2:
                    MinhaImagem.Source = new BitmapImage(new Uri("Images/Agua3.png", UriKind.Relative));
                    break;
                case 3:
                    MinhaImagem.Source = new BitmapImage(new Uri("Images/Agua4.png", UriKind.Relative));
                    break;
            }
        }

        private void ToggleSwitch_Checked(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
            {
                try
                {
                    if (MinhaImagem == null)
                    {
                        MessageBox.Show("MinhaImagem não foi inicializada.");
                        return;
                    }

                    switch (ComboBox4.SelectedIndex)
                    {
                        case 0:
                            MinhaImagem.Source = new BitmapImage(new Uri("Images/Agua1.png", UriKind.Relative));
                            break;
                        case 1:
                            MinhaImagem.Source = new BitmapImage(new Uri("Images/Agua2.png", UriKind.Relative));
                            break;
                        case 2:
                            MinhaImagem.Source = new BitmapImage(new Uri("Images/Agua3.png", UriKind.Relative));
                            break;
                        case 3:
                            MinhaImagem.Source = new BitmapImage(new Uri("Images/Agua4.png", UriKind.Relative));
                            break;
                        default:
                            MinhaImagem.Source = new BitmapImage(new Uri("Images/AguaDesligado.png", UriKind.Relative));
                            break;
                    }

                    ComboBox1.IsEnabled = true;
                    ComboBox2.IsEnabled = true;
                    ComboBox3.IsEnabled = true;
                    ComboBox4.IsEnabled = true;
                    Border1.Background = new SolidColorBrush(Color.FromRgb(37, 167, 84));
                    Border2.Background = new SolidColorBrush(Color.FromRgb(37, 167, 84));
                    Border3.Background = new SolidColorBrush(Color.FromRgb(37, 167, 84));
                    Border4.Background = new SolidColorBrush(Color.FromRgb(37, 167, 84));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocorreu um erro: {ex.Message}");
                }
            }));
        }


        private void ToggleSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            MinhaImagem.Source = new BitmapImage(new Uri("Images/AguaDesligado.png", UriKind.Relative));

            ComboBox1.IsEnabled = false;
            ComboBox2.IsEnabled = false;
            ComboBox3.IsEnabled = false;
            ComboBox4.IsEnabled = false;

            Border1.Background = new SolidColorBrush(Color.FromRgb(68, 68, 68)); 
            Border2.Background = new SolidColorBrush(Color.FromRgb(68, 68, 68)); 
            Border3.Background = new SolidColorBrush(Color.FromRgb(68, 68, 68)); 
            Border4.Background = new SolidColorBrush(Color.FromRgb(68, 68, 68)); 
        }
    }
}
