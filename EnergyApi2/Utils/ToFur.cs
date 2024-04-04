using EnergyApi2.Models;
using EnergyApi2.Utils.Model;
using System;

namespace EnergyApi2.Utils
{
    public class ToFur
    {
        public static Energy ToFurGo(Energetik energetik, Energy energy)
        {
            return new Energy(energy.IdEnergy, energetik.title, energetik.description, energetik.price, energy.TypeF, energy.CategoryId, energy.ImageId, energy.IsExists);
        }
    }
}
