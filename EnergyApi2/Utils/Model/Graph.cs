using System;

namespace EnergyApi2.Utils.Model
{
    public class Graph
    {
        public Graph(DateTime? date, int price)
        {
            this.date = date;
            this.price = price;
        }

        public Graph()
        {
        }

        public DateTime? date { get; set; }
        public int price { get; set; }
    }
}
