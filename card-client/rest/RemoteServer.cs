using RestSharp;
using System;

namespace card_client.rest
{
    public class RemoteServer : IDisposable
    {
        private RestClient _client;

        public RemoteServer(string baseUrl)
        {
            _client = new RestClient(baseUrl);
            Cards = new CardContext(_client);
        }

        public CardContext Cards { get; private set; }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
