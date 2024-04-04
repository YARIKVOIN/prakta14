using System;
using System.Collections.Generic;

namespace EnergyApi2.Models
{
    public partial class Infoappenergy
    {
        public string НазваниеЭнергетика { get; set; } = null!;
        public string ОписаниеЭнергетика { get; set; } = null!;
        public int Стоимость { get; set; }
        public int КатегорияЭнергетика { get; set; }
    }
}
