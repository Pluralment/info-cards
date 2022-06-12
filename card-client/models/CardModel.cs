using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace card_client.Models
{
    public class CardModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public BitmapImage Image { get; set; }
    }
}
