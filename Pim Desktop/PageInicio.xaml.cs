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
 
    public partial class PageInicio : Page
    {
        private Frame _mainFrame;
        public PageInicio(Frame mainFrame)
        {
            InitializeComponent();
            _mainFrame = mainFrame;
        }


        private void Producao_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new PageProducao(_mainFrame));
        }

        private void Irrigaçao_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new PageIrrigacao(_mainFrame));
        }

        private void Consumo_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new PageConsumo(_mainFrame));
        }

        private void Iluminacao_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new PageIluminacao(_mainFrame));
        }
        private void Plantas_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new PagePlantas(_mainFrame));
        }

        private void Fornecedores_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new PageFornecedores(_mainFrame));
        }

    }
}
