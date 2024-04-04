using System;
using System.Collections.Generic;

namespace EnergyApi2.Models
{
    public partial class Appuser
    {
        public Appuser()
        {

        }

        public int IdAppUser { get; set; }
        public string Login { get; set; } = null!;
        public string Mail { get; set; } = null!;
        public string AppPassword { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public bool Status { get; set; }
        public int Balance { get; set; }
        public int? RolesId { get; set; }
        public bool? IsExists { get; set; }


    }
}
