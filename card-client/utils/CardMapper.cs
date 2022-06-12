using card_client.dto;
using card_client.extensions;
using card_client.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace card_client.utils
{
    public static class CardMapper
    {
        public static CardModel ToCard(CardServerModel serverModel)
        {
            var cardModel = new CardModel
            {
                Id = serverModel.Id,
                Title = serverModel.Title,
                Image = new BitmapImage()
            };

            if (serverModel.Image.Any())
            {
                using (MemoryStream memoryStream = new MemoryStream(serverModel.Image))
                {
                    cardModel.Image.InitBitmapWithStream(memoryStream);
                }

            }

            return cardModel;
        }

        public static ICollection<CardModel> ToCards(ICollection<CardServerModel> serverCards)
        {
            ICollection<CardModel> cards = new List<CardModel>();
            foreach (var serverCard in serverCards)
            {
                cards.Add(ToCard(serverCard));
            }
            return cards;
        }
    }
}
