using System;
using System.Collections.Generic;

namespace EnergyApi2.Models
{
    public partial class Basket
    {
        public Basket()
        {

        }

        public int IdBasket { get; set; }
        public int AppUserId { get; set; }
        public bool? IsExists { get; set; }


    }
}
