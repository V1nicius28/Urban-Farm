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
    public partial class PageFeedback : Page
    {
        public PageFeedback()
        {
            InitializeComponent();
        }

        private void EmailBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EmailText.Visibility = Visibility.Collapsed;
        }

        private void EmailBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                EmailText.Visibility = Visibility.Visible;
            }
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

        private int selectedRating = 0;

        private void Star_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image clickedStar)
            {
                int starNumber = int.Parse(clickedStar.Name.Replace("Star", ""));

                selectedRating = starNumber;

                UpdateStarImages();
            }
        }

        private void UpdateStarImages()
        {
            for (int i = 1; i <= 5; i++)
            {
                Image star = (Image)FindName($"Star{i}");
                if (star != null)
                {
                    star.Source = new BitmapImage(new Uri(i <= selectedRating ? "/Images/StarFilled.png" : "/Images/Star.png", UriKind.Relative));
                }
            }
        }

        private void Enviar_Click(object sender, RoutedEventArgs e)
        {
            if (selectedRating == 0)
            {
                MensagemPopup.Text = "Por favor, selecione pelo menos 1 estrela para enviar sua avaliação.";
                AvisoPopup.HorizontalOffset = 185;
                AvisoPopup.VerticalOffset = 80;
                AvisoPopup.IsOpen = true;
                Task.Delay(3000).ContinueWith(_ =>
                {
                    Dispatcher.Invoke(() => AvisoPopup.IsOpen = false);
                });
                return;
            }

            MensagemPopup.Text = "Avaliação enviada!";
            AvisoPopup.HorizontalOffset = 338;
            AvisoPopup.VerticalOffset = 80;
            AvisoPopup.IsOpen = true;

            selectedRating = 0;
            CommentTextBox.Text = string.Empty;
            EmailTextBox.Text = string.Empty;

            UpdateStarImages();

            EmailText.Visibility = Visibility.Visible;
            CommentText.Visibility = Visibility.Visible;

            Task.Delay(3000).ContinueWith(_ =>
            {
                Dispatcher.Invoke(() =>
                {
                    AvisoPopup.IsOpen = false;
                });
            });
        }
    }
}



