using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace card_client.dto
{
    public class CardServerModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public byte[] Image { get; set; }
    }
}
