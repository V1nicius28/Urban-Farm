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

namespace Pim_Desktop
{
    public partial class Report : Window
    {
        public bool Resultado { get; private set; } = false;
        public string ?Comentario { get; private set; }

        public Report()
        {
            InitializeComponent();
        }

        private void AceitarButton_Click(object sender, RoutedEventArgs e)
        {
            string descricaoErro = CommentTextBox.Text;

            if (string.IsNullOrWhiteSpace(descricaoErro))
            {
                MensagemPopup.Text = "Por favor, descreva o erro antes de enviar!";
                AvisoPopup.HorizontalOffset = 0;
                AvisoPopup.VerticalOffset = -200;
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

            Comentario = CommentTextBox.Text;
            this.DialogResult = true; 
            this.Close();
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            Resultado = false; 
            this.Close(); 
        }

        private void CommentBox_GotFocus(object sender, RoutedEventArgs e)
        {
            CommentText.Visibility = Visibility.Collapsed;
        }

        private void CommentBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CommentTextBox.Text))
            {
                CommentText.Visibility = Visibility.Visible;
            }
        }
    }
}
