using System;
using System.Collections.Generic;

namespace EnergyApi2.Models
{
    public partial class Category
    {
        public Category()
        {
          
        }

        public int IdCategory { get; set; }
        public int NameCategory { get; set; }
        public bool? IsExists { get; set; }


    }
}
