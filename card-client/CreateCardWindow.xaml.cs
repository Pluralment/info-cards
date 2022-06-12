using card_client.extensions;
using card_client.Models;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Imaging;

namespace card_client
{
    /// <summary>
    /// Логика взаимодействия для CreateCardWindow.xaml
    /// </summary>
    public partial class CreateCardWindow : Window
    {
        private string _imagePath;
        public CardModel Card { get; set; }
        public BitmapImage CardImage { get; set; }

        public CreateCardWindow()
        {
            InitializeComponent();
        }

        private void AddCardToCollection()
        {
            Card = new CardModel { Title = TitleBox.Text, ImagePath = _imagePath, Image = CardImage };
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            AddCardToCollection();
            DialogResult = true;
        }

        private async void PickPicture_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog imageDialog = new OpenFileDialog();
            imageDialog.Filter = "Картинки(*.PNG)|*.PNG";
            imageDialog.CheckFileExists = true;
            imageDialog.Multiselect = false;
            if (imageDialog.ShowDialog() == true)
            {
                _imagePath = imageDialog.FileName;
                CardImage = await imageDialog.LoadBitmapImage();
                Img.Source = CardImage;
            }
        }
    }
}
