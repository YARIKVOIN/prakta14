using System;
using System.Collections.Generic;

namespace EnergyApi2.Models
{
    public partial class Infoappuser
    {
        public string Логин { get; set; } = null!;
        public string Пароль { get; set; } = null!;
        public string Соль { get; set; } = null!;
        public string Почта { get; set; } = null!;
        public bool СтатусАккаунта { get; set; }
        public int Баланс { get; set; }
        public int Роль { get; set; }
    }
}
