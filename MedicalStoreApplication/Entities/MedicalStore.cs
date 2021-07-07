using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalStoreApplication.Entities
{
    [Serializable]
    public class MedicalStore :Owner
    {
        
        private string Address;
        List<Customer> customers;
        private const int pincode = 562102;

        public string Address1 { get => Address; set => Address = value; }
        public List<Customer> Customers { get => customers; set => customers = value; }

        public static int Pincode => pincode;

        public MedicalStore()
        {

        }

        public MedicalStore(string address, List<Customer> customers ,string ownerName) : base(ownerName)
        {
            Address1 = address;
            this.Customers = customers;
        }
    }
}
