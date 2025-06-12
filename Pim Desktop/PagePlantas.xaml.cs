using Microsoft.Win32;
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
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.IO;

namespace Pim_Desktop
{
    public partial class PagePlantas : Page
    {
        private List<Border> selectedBorders = new List<Border>();
        private ComboBox estacaoComboBox;
        private ComboBox frutasComboBox;
        private string filtroAtual = "Todos";
        private Frame _mainFrame;

        public PagePlantas(Frame mainFrame)
        {
            InitializeComponent();
            CarregarImagem();
            _mainFrame = mainFrame;
        }

        private void Voltar_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new PageInicio(_mainFrame));
        }


        public class Venda
        {
            public int Id { get; set; }
            public string Alimento { get; set; }
            public string Descricao { get; set; }
            public string Tipo { get; set; }
            public string Estacao { get; set; }
        }


        private void AdicionarVenda(Venda venda)
        {
            Border foodBorder = new Border
            {
                Name = "FoodBorder",
                HorizontalAlignment = HorizontalAlignment.Center,
                Height = 206,
                Background = (Brush)FindResource("BackgroundLinear"),
                VerticalAlignment = VerticalAlignment.Top,
                Width = 170,
                Margin = new Thickness(0, 0, 20, 20),
                CornerRadius = new CornerRadius(5),
                BorderBrush = new SolidColorBrush(Color.FromRgb(37, 167, 84)), 
                BorderThickness = new Thickness(1),
                Tag = venda.Id
            };

            Border descricaoBorder = new Border
            {
                Name = "DescricaoBorder",
                HorizontalAlignment = HorizontalAlignment.Center,
                Height = 206,
                VerticalAlignment = VerticalAlignment.Top,
                Width = 360,
                Margin = new Thickness(0, 0, 20, 20),
                CornerRadius = new CornerRadius(5),
                BorderBrush = new SolidColorBrush(Color.FromRgb(37, 167, 84)), 
                BorderThickness = new Thickness(1),
                Tag = venda.Id
            };

            descricaoBorder.SetResourceReference(Border.BackgroundProperty, "Background");


            Grid contentGrid = new Grid();


            Image buttonImage = new Image
            {
                Source = new BitmapImage(new Uri("/Images/AddImage.png", UriKind.RelativeOrAbsolute)), 
                Width = 50, 
                Height = 50, 
                Stretch = Stretch.Uniform,
                Margin = new Thickness(0, 0, 0, 10)
            };

            Button imageButton = new Button
            {
                Width = 102, 
                Height = 102, 
                Content = buttonImage,
                BorderBrush = Brushes.Transparent,
                Background = Brushes.Transparent,
                Style = (Style)FindResource("RoundedButtonStyle"),
                Padding = new Thickness(0), 
                Margin = new Thickness(0, 0, 0, 50),
                Cursor = Cursors.Hand
            };
            imageButton.Click += ImageButton_Click; 


            TextBox descricaoTextBox = new TextBox
            {
                Name = "Descrição",
                Text = venda.Descricao, 
                Style = (Style)FindResource("CustomTextBoxStyle"),
                Width = 350,
                Height = 200,
                FontFamily = new FontFamily("Tahoma"),
                FontSize = 14,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 0, 0, 0) 
            };
            descricaoBorder.Child = descricaoTextBox; 


            TextBox alimentoTextBox = new TextBox
            {
                Name = "Alimento",
                Text = venda.Alimento, 
                Style = (Style)FindResource("CustomTextBoxStyle"),
                Width = 80,
                Height = 20,
                FontFamily = new FontFamily("Tahoma"),
                FontSize = 14,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 80, -115) 
            };


            frutasComboBox = new ComboBox
            {
                Name = "escolha",
                Width = 78,
                Height = 23,
                FontFamily = new FontFamily("Tahoma"),
                FontSize = 14,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(90, 0, 0, -115),
                Style = (Style)FindResource("CustomComboBoxStyle")
            };

 
            frutasComboBox.Items.Add("Tipo");
            frutasComboBox.Items.Add("Fruta");
            frutasComboBox.Items.Add("Legume");
            frutasComboBox.Items.Add("Verdura");

    
            frutasComboBox.SelectedItem = venda.Tipo;

     
            estacaoComboBox = new ComboBox
            {
                Name = "estacao",
                Width = 90,
                Height = 23,
                FontFamily = new FontFamily("Tahoma"),
                FontSize = 14,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(75, 0, 0, -175), 
                Style = (Style)FindResource("CustomComboBoxStyle")
            };


            estacaoComboBox.Items.Add("Tipo");
            estacaoComboBox.Items.Add("Verão");
            estacaoComboBox.Items.Add("Primavera");
            estacaoComboBox.Items.Add("Outono");
            estacaoComboBox.Items.Add("Inverno");

 
            estacaoComboBox.SelectedItem = venda.Estacao;


            contentGrid.Children.Add(imageButton);
            contentGrid.Children.Add(alimentoTextBox);
            contentGrid.Children.Add(frutasComboBox);
            contentGrid.Children.Add(estacaoComboBox);


            foodBorder.Child = contentGrid;

   
            foodBorder.MouseLeftButtonDown += FoodBorder_MouseLeftButtonDown;

  
            StackPanel stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center
            };

     
            stackPanel.Children.Add(foodBorder);
            stackPanel.Children.Add(descricaoBorder);


            VendasWrapPanel.Children.Add(stackPanel);

        }

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                Button clickedButton = sender as Button;

                Border parentBorder = FindParent<Border>(clickedButton);
                if (parentBorder != null)
                {
                    int borderId = (int)parentBorder.Tag;

                    Image selectedImage = new Image
                    {
                        Source = new BitmapImage(new Uri(openFileDialog.FileName)),
                        Stretch = Stretch.Uniform,
                        Height = 100,
                        Width = 100
                    };

                    clickedButton.Content = selectedImage;

                    SaveImagePath(borderId, openFileDialog.FileName);
                }
            }
        }

        private void AdicionarVenda_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Venda novaVenda = new Venda
                {
                    Alimento = "Alimento",
                    Estacao = "Tipo",
                    Tipo = "Tipo",
                    Descricao = "Descrição"
                };

                AdicionarVenda(novaVenda);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao adicionar venda: {ex.Message}");
            }
        }
      

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedBorders.Count > 0)
            {
                foreach (var border in selectedBorders.ToList())
                {
                    if (border.Tag is int idVenda)
                    {

                        var stackPanel = border.Parent as StackPanel;
                        if (stackPanel != null)
                        {
                            var parent = stackPanel.Parent as Panel;
                            parent?.Children.Remove(stackPanel);
                        }
                    }
                }
                selectedBorders.Clear();
            }
            else
            {
                MensagemPopup.Text = "Nenhum item selecionado para remover";
                AvisoPopup.HorizontalOffset = 260;
                AvisoPopup.VerticalOffset = 80;
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


        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;
            T? parent = parentObject as T;
            return parent != null ? parent : FindParent<T>(parentObject);
        }


        private void FoodBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border clickedBorder = sender as Border;

            if (selectedBorders.Contains(clickedBorder))
            {
                clickedBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(37, 167, 84)); 
                clickedBorder.BorderThickness = new Thickness(1);
                selectedBorders.Remove(clickedBorder); 
            }
            else
            {
                clickedBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(237, 66, 69)); 
                clickedBorder.BorderThickness = new Thickness(2);
                selectedBorders.Add(clickedBorder); 
            }
        }


        private void LocalTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            {
                FiltrarBorders(filtroAtual);
            }
        }


        private T? FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child != null && child is T)
                {
                    return (T)child;
                }

                T? childOfChild = FindVisualChild<T>(parent: child);
                if (childOfChild != null)
                {
                    return childOfChild;
                }
            }
            return null;
        }


        private void FiltrarBorders(string tipo)
        {
            filtroAtual = tipo;
            string searchText = LocalTextBox.Text.ToLower();

            bool tipoSelecionado = false;
            bool estacaoSelecionada = false;

            var tiposSelecionados = new List<string>();
            if (Frutas.IsChecked == true) { tiposSelecionados.Add("Fruta"); tipoSelecionado = true; }
            if (Verduras.IsChecked == true) { tiposSelecionados.Add("Verdura"); tipoSelecionado = true; }
            if (Legumes.IsChecked == true) { tiposSelecionados.Add("Legume"); tipoSelecionado = true; }

            var estacoesSelecionadas = new List<string>();
            if (Verao.IsChecked == true) { estacoesSelecionadas.Add("Verão"); estacaoSelecionada = true; }
            if (Primavera.IsChecked == true) { estacoesSelecionadas.Add("Primavera"); estacaoSelecionada = true; }
            if (Outono.IsChecked == true) { estacoesSelecionadas.Add("Outono"); estacaoSelecionada = true; }
            if (Inverno.IsChecked == true) { estacoesSelecionadas.Add("Inverno"); estacaoSelecionada = true; }

            foreach (StackPanel stackPanel in VendasWrapPanel.Children.OfType<StackPanel>())
            {
                var comboBoxes = FindVisualChildren<ComboBox>(stackPanel).ToList();
                var frutasComboBox = comboBoxes.FirstOrDefault(cb => cb.Name == "escolha");
                var estacaoComboBox = comboBoxes.FirstOrDefault(cb => cb.Name == "estacao");
                var alimentoTextBox = FindVisualChild<TextBox>(stackPanel);

                if (frutasComboBox != null && estacaoComboBox != null && alimentoTextBox != null)
                {
                    bool tipoCorresponde = false;
                    bool estacaoCorresponde = false;

 
                    if (tipoSelecionado)
                        tipoCorresponde = tiposSelecionados.Contains(frutasComboBox.SelectedItem?.ToString()) || tiposSelecionados.Contains("Todos");

    
                    if (estacaoSelecionada)
                        estacaoCorresponde = estacoesSelecionadas.Contains(estacaoComboBox.SelectedItem?.ToString()) || estacoesSelecionadas.Contains("Todos");

                  
                    bool pesquisaCorresponde = alimentoTextBox.Text.ToLower().Contains(searchText);

                   
                    if ((tipoSelecionado && estacaoSelecionada) && tipoCorresponde && estacaoCorresponde && pesquisaCorresponde)
                    {
                        stackPanel.Visibility = Visibility.Visible;
                    }
                    else if ((tipoSelecionado && !estacaoSelecionada) && tipoCorresponde && pesquisaCorresponde)
                    {
                        stackPanel.Visibility = Visibility.Visible;
                    }
                    else if ((!tipoSelecionado && estacaoSelecionada) && estacaoCorresponde && pesquisaCorresponde)
                    {
                        stackPanel.Visibility = Visibility.Visible;
                    }
                    else if (!tipoSelecionado && !estacaoSelecionada && pesquisaCorresponde)
                    {
                        stackPanel.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        stackPanel.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }


        private IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                if (child is T)
                    yield return (T)child;

                foreach (var childOfChild in FindVisualChildren<T>(child))
                    yield return childOfChild;
            }
        }


        private void Frutas_Click(object sender, RoutedEventArgs e)
        {
            DesmarcarFruta("Fruta");
            FiltrarBorders("Fruta");
        }
        private void Verduras_Click(object sender, RoutedEventArgs e)
        {
            DesmarcarFruta("Verdura");
            FiltrarBorders("Verdura");
        }

        private void Legumes_Click(object sender, RoutedEventArgs e)
        {
            DesmarcarFruta("Legume");
            FiltrarBorders("Legume");
        }

        private void Verao_Click(object sender, RoutedEventArgs e)
        {
            DesmarcarEstacao("Verão");
            FiltrarBorders("Verão");
        }

        private void Primavera_Click(object sender, RoutedEventArgs e)
        {
            DesmarcarEstacao("Primavera");
            FiltrarBorders("Primavera");
        }

        private void Outono_Click(object sender, RoutedEventArgs e)
        {
            DesmarcarEstacao("Outono");
            FiltrarBorders("Outono");
        }

        private void Inverno_Click(object sender, RoutedEventArgs e)
        {
            DesmarcarEstacao("Inverno");
            FiltrarBorders("Inverno");
        }

        private void Todos_Click(object sender, RoutedEventArgs e)
        {
            DesmarcarEstacao("Todos");
            DesmarcarFruta("Todos");
            FiltrarBorders("Todos");
        }

        private void DesmarcarEstacao(string estacaoClicked)
        {
            if (estacaoClicked != "Verão") Verao.IsChecked = false;
            if (estacaoClicked != "Primavera") Primavera.IsChecked = false;
            if (estacaoClicked != "Outono") Outono.IsChecked = false;
            if (estacaoClicked != "Inverno") Inverno.IsChecked = false;
            if (estacaoClicked != "Todos") Todos.IsChecked = false;    
        }

        private void DesmarcarFruta(string escolhaClicked)
        {
            if (escolhaClicked != "Fruta") Frutas.IsChecked = false;
            if (escolhaClicked != "Verdura") Verduras.IsChecked = false;
            if (escolhaClicked != "Legume") Legumes.IsChecked = false;
            if (escolhaClicked != "Todos") Todos.IsChecked = false;

            switch (escolhaClicked)
            {
                case "Fruta":
                    Frutas.IsChecked = true;
                    break;
                case "Verdura":
                    Verduras.IsChecked = true;
                    break;
                case "Legumes":
                    Legumes.IsChecked = true;
                    break;
                case "Todos":
                    Todos.IsChecked = true;
                    break;
            }
        }

        private void Verao_Checked(object sender, RoutedEventArgs e)
        {
            VeraoImage.Source = new BitmapImage(new Uri("/Images/Summer2.png", UriKind.Relative));
        }

        private void Verao_Unchecked(object sender, RoutedEventArgs e)
        {
            VeraoImage.Source = new BitmapImage(new Uri("/Images/Summer1.png", UriKind.Relative));
        }

        private void Primavera_Checked(object sender, RoutedEventArgs e)
        {
            PrimaveraImage.Source = new BitmapImage(new Uri("/Images/Spring2.png", UriKind.Relative));
        }

        private void Primavera_Unchecked(object sender, RoutedEventArgs e)
        {
            PrimaveraImage.Source = new BitmapImage(new Uri("/Images/Spring1.png", UriKind.Relative));
        }

        private void Outono_Checked(object sender, RoutedEventArgs e)
        {
            OutonoImage.Source = new BitmapImage(new Uri("/Images/Fall2.png", UriKind.Relative));
        }

        private void Outono_Unchecked(object sender, RoutedEventArgs e)
        {
            OutonoImage.Source = new BitmapImage(new Uri("/Images/Fall1.png", UriKind.Relative));
        }

        private void Inverno_Checked(object sender, RoutedEventArgs e)
        {
            InvernoImage.Source = new BitmapImage(new Uri("/Images/Winter2.png", UriKind.Relative));
        }

        private void Inverno_Unchecked(object sender, RoutedEventArgs e)
        {
            InvernoImage.Source = new BitmapImage(new Uri("/Images/Winter1.png", UriKind.Relative));
        }



        private void PesquisaBox_GotFocus(object sender, RoutedEventArgs e)
        {
            LocalText.Visibility = Visibility.Collapsed;
        }

        private void PesquisaBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LocalTextBox.Text))
            {
                LocalText.Visibility = Visibility.Visible;
            }
        }

        public class ImageSettings
        {
            public Dictionary<int, string> PlantaImages { get; set; } = new Dictionary<int, string>();
        }


        public void SaveImagePath(int borderId, string path)
        {
            var filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PagePlantasSettings.json");

            ImageSettings settings;

            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                settings = JsonConvert.DeserializeObject<ImageSettings>(json);
            }
            else
            {
                settings = new ImageSettings();
            }

            settings.PlantaImages[borderId] = path;  

            var jsonSettings = JsonConvert.SerializeObject(settings);
            File.WriteAllText(filePath, jsonSettings);
        }

        public void CarregarImagem()
        {
            string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PagePlantasSettings.json");

            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var settings = JsonConvert.DeserializeObject<ImageSettings>(json);

                foreach (StackPanel stackPanel in VendasWrapPanel.Children)
                {
                    Border foodBorder = stackPanel.Children.OfType<Border>().FirstOrDefault();

                    if (foodBorder != null)
                    {
                        int borderId = (int)foodBorder.Tag;

                        if (settings?.PlantaImages.ContainsKey(borderId) == true)
                        {
                            string imagePath = settings.PlantaImages[borderId];

                            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                            {
                                Button imageButton = foodBorder?.Child is Grid grid ? grid.Children.OfType<Button>().FirstOrDefault() : null;

                                if (imageButton != null)
                                {
                                    Image selectedImage = new Image
                                    {
                                        Source = new BitmapImage(new Uri(imagePath)),
                                        Stretch = Stretch.Uniform,
                                        Height = 100, 
                                        Width = 100 
                                    };

                                    imageButton.Content = selectedImage;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}




