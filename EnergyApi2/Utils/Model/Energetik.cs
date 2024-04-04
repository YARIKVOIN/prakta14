namespace EnergyApi2.Utils.Model
{
    public class Energetik
    {
        public Energetik(string id, string imageUrl, string title, int price, int[] types, int[] sizes, string description)
        {
            this.id = id;
            this.imageUrl = imageUrl;
            this.title = title;
            this.price = price;
            this.types = types;
            this.sizes = sizes;
            this.description = description;
        }

        public Energetik()
        {
        }

        public string id { get; set; }
        public string imageUrl { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public int[] types { get; set; }
        public int[] sizes { get; set; }
    }
}
