using System;
using System.Collections.Generic;

namespace EnergyApi2.Models
{
    public partial class Favoritebasket
    {
        public int IdFavoriteBasket { get; set; }
        public int EnergyId { get; set; }
        public int BasketId { get; set; }
        public bool? IsExists { get; set; }

        public virtual Basket Basket { get; set; } = null!;
        public virtual Energy Energy { get; set; } = null!;
    }
}
