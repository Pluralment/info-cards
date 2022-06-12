using card_client.dto;
using card_client.Models;
using card_client.utils;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace card_client.rest
{
    public class CardContext
    {
        private RestClient _client;

        public CardContext(RestClient client)
        {
            _client = client;
        }

        public async Task<ICollection<CardModel>> GetCards()
        {
            var request = new RestRequest("cards", Method.Get);

            var serverCards = await _client.GetAsync<ICollection<CardServerModel>>(request);
            var cards = CardMapper.ToCards(serverCards);
            return cards;
        }

        public async Task<CardModel> CreateCard(CardModel card)
        {
            var request = new RestRequest("cards", Method.Post);
            using (var fileStream = File.Open(card.ImagePath, FileMode.Open))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await fileStream.CopyToAsync(memoryStream);
                    request.AddFile("image", memoryStream.ToArray(), Path.GetFileName(card.ImagePath), "image/png");
                    request.AlwaysMultipartFormData = true;
                }
            }
            request.AddParameter("title", card.Title);

            card.Id = await _client.PostAsync<string>(request);
            return card;
        }

        public async Task<bool> DeleteCard(string cardId)
        {
            var request = new RestRequest($"cards/{cardId}", Method.Delete);
            var response = await _client.DeleteAsync(request);
            return (response.ResponseStatus != ResponseStatus.Error);
        }

        public async Task<bool> UpdateCard(CardModel card)
        {
            var request = new RestRequest($"cards/{card.Id}", Method.Put);
            if (card.ImagePath != null)
            {
                using (var fileStream = File.Open(card.ImagePath, FileMode.Open))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        await fileStream.CopyToAsync(memoryStream);
                        request.AddFile("image", memoryStream.ToArray(), Path.GetFileName(card.ImagePath), "image/png");
                        request.AlwaysMultipartFormData = true;
                    }
                }
            }
            request.AlwaysMultipartFormData = true;
            request.AddParameter("id", card.Id);
            request.AddParameter("title", card.Title);

            var response = await _client.PutAsync(request);
            return (response.ResponseStatus != ResponseStatus.Error);
        }
    }
}