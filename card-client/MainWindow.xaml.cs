using card_client.extensions;
using card_client.Models;
using card_client.rest;
using card_client.ViewModels;
using Microsoft.Win32;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace card_client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
        public CardViewModel CardViewModel { get; private set; }
        public bool IsEditingCard { get; set; } = false;

        private RemoteServer remoteServer = new RemoteServer("http://localhost:5000/api");

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void CreateCardBtn_Click(object sender, RoutedEventArgs e)
        {
            var createCardWindow = new CreateCardWindow { Owner = this };
            if (createCardWindow.ShowDialog() == true)
            {
                if (createCardWindow.Card.Title != "") await CardViewModel.AddCard(createCardWindow.Card);
            }
        }

        private async Task InitViewModel()
        {
            CardViewModel = await CardViewModel.CreateAsync(remoteServer);

            CardView.DataContext = CardViewModel;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await InitViewModel();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Dispose();
        }

        public void Dispose()
        {
            remoteServer.Dispose();
        }

        private async void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!await CardViewModel.DeleteCard(((Button)sender).DataContext.ToString()))
                MessageBox.Show("Some problems while deleting card");
        }

        private async void ChangeCardImageBtn_Click(object sender, RoutedEventArgs e)
        {
            var editedCard = (CardModel)CardView.SelectedItem;

            OpenFileDialog imageDialog = new OpenFileDialog();
            imageDialog.Filter = "Картинки(*.PNG)|*.PNG";
            imageDialog.CheckFileExists = true;
            imageDialog.Multiselect = false;
            if (imageDialog.ShowDialog() == true)
            {
                editedCard.ImagePath = imageDialog.FileName;
                editedCard.Image = await imageDialog.LoadBitmapImage();
            }
        }

        private async void SaveEditedCard_ClickAsync(object sender, RoutedEventArgs e)
        {
            var editedCard = (CardModel)CardView.SelectedItem;

            _ = await CardViewModel.UpdateCard(editedCard) ?
            MessageBox.Show("Card updated successfully!") : MessageBox.Show("Could not update card");
        }
    }
}
