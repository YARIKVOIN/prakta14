using System;
using System.Collections.Generic;

namespace EnergyApi2.Models
{
    public partial class Energy
    {
        public Energy()
        {
 
        }

        public Energy(int idEnergy, string nameEnergy, string descriptionEnergy, int price, int typeF, int categoryId, int? imageId, bool? isExists)
        {
            IdEnergy = idEnergy;
            NameEnergy = nameEnergy;
            DescriptionEnergy = descriptionEnergy;
            Price = price;
            TypeF = typeF;
            CategoryId = categoryId;
            ImageId = imageId;
            IsExists = isExists;
        }

        public int IdEnergy { get; set; }
        public string NameEnergy { get; set; } = null!;
        public string DescriptionEnergy { get; set; } = null!;
        public int Price { get; set; }
        public int TypeF { get; set; }
        public int CategoryId { get; set; }
        public int? ImageId { get; set; }
        public bool? IsExists { get; set; }


    }
}
