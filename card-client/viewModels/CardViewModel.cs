using card_client.Models;
using card_client.rest;
using Syncfusion.Windows.Shared;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace card_client.ViewModels
{
    public class CardViewModel : NotificationObject
    {
        private ObservableCollection<CardModel> _cardViewItems;
        private RemoteServer _remoteServer;

        public ObservableCollection<CardModel> CardViewItems
        {
            get { return _cardViewItems; }
            set
            {
                _cardViewItems = value;
                RaisePropertyChanged(nameof(CardViewItems));
            }
        }

        private CardViewModel(RemoteServer server)
        {
            _remoteServer = server;
            CardViewItems = new ObservableCollection<CardModel>();
        }

        public async Task AddCard(CardModel card)
        {
            CardViewItems.Add(await UploadCardToServer(card));
        }

        private async Task<CardModel> UploadCardToServer(CardModel card)
        {
            return await _remoteServer.Cards.CreateCard(card);
        }

        public static Task<CardViewModel> CreateAsync(RemoteServer server)
        {
            var model = new CardViewModel(server);
            return model.InitializeAsync();
        }

        private async Task<CardViewModel> InitializeAsync()
        {
            var cards = await _remoteServer.Cards.GetCards();

            foreach (var card in cards)
            {
                CardViewItems.Add(card);
            }

            return this;
        }

        public async Task<bool> DeleteCard(string cardId)
        {
            var deleteCard = CardViewItems.SingleOrDefault(card => card.Id == cardId);
            if (deleteCard != default(CardModel) && await _remoteServer.Cards.DeleteCard(cardId))
            {
                CardViewItems.Remove(deleteCard);
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateCard(CardModel card)
        {
            return await _remoteServer.Cards.UpdateCard(card);
        }
    }
}
