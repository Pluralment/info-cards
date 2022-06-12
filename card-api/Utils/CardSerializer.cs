using card_api.Extensions;
using card_api.Interfaces;
using card_api.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace card_api.Utils
{
    public class CardSerializer
    {
        private string _fileName;

        public CardSerializer(IConfiguration config)
        {
            _fileName = config.GetCardStorageName();
        }

        public async Task<bool> SerializeItemsToFile(ICollection<Card> items)
        {
            try
            {
                using (FileStream fs = new FileStream(_fileName, FileMode.Truncate))
                {
                    await JsonSerializer.SerializeAsync(fs, items);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<ICollection<Card>> DeserializeItemsFromFile()
        {
            ICollection<Card> items = new List<Card>();
            try
            {
                using (FileStream fs = new FileStream(_fileName, FileMode.Open))
                {
                    items = await JsonSerializer.DeserializeAsync<ICollection<Card>>(fs);
                }
            }
            catch (FileNotFoundException) { }
            //catch (JsonException) { }
            return items;
        }

        public async Task<bool> SerializeItem(Card item)
        {
            try
            {
                var items = await DeserializeItemsFromFile();
                items.Add(item);

                using (FileStream fs = new FileStream(_fileName, FileMode.OpenOrCreate))
                    await JsonSerializer.SerializeAsync(fs, items);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
