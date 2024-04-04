using System;
using System.Collections.Generic;

namespace EnergyApi2.Models
{
    public partial class Role
    {
        public Role()
        {
            
        }

        public int IdRoles { get; set; }
        public int NameRoles { get; set; }
        public bool? IsExists { get; set; }


    }
}
