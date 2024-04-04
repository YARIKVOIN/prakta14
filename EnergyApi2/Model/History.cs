using System;
using System.Collections.Generic;

namespace EnergyApi2.Models
{
    public partial class History
    {
        public int IdHistory { get; set; }
        public DateTime? DataHistory { get; set; }
        public int PriceHistory { get; set; }
        public string DataBuy { get; set; } = null!;
        public int BasketId { get; set; }
        public int AppUserId { get; set; }
        public bool? IsExists { get; set; }


    }
}
