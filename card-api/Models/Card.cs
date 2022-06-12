using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json.Serialization;

namespace card_api.Models
{
    public class Card
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ImageBase64String { get; set; }
    }
}
