using System;
using System.Collections.Generic;

namespace EnergyApi2.Models
{
    public partial class Image
    {
        public Image()
        {
            
        }

        public int IdImage { get; set; }
        public string LinkImage { get; set; } = null!;
        public bool? IsExists { get; set; }

    }
}
