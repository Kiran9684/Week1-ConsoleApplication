using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalStoreApplication.Entities
{
    [Serializable]
    public class Medicine
    {
        private string name;
        private int quantity;
        private float pricePerMedicine;

        public string Name { get => name; set => name = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public float PricePerMedicine { get => pricePerMedicine; set => pricePerMedicine = value; }

       
    }
}
