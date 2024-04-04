using EnergyApi2.Utils.Model;

namespace EnergyApi2.Utils
{
    public class ToEnergetik
    {
        public static Energetik ToEnergetikGo(string id,
        string imageUrl,
        string title,
        int price,
        int typesF, string description)
        {
            Energetik energetik = new Energetik();
            int[] sizes = { 36, 42 };
            if (typesF == 0)
            {
                int[] types = { 0 };
                energetik = new Energetik(id, imageUrl, title, price, types, sizes, description);
            }
            else if(typesF == 1)
            {
                int[] types = { 1 };
                energetik = new Energetik(id, imageUrl, title, price, types, sizes, description);
            }
            else if (typesF == 2)
            {
                int[] types = { 0 ,1 };
                energetik = new Energetik(id, imageUrl, title, price, types, sizes, description);
            }
            return energetik;
        }
    }
}
