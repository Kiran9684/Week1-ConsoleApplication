using MedicalStoreApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalStoreApplication.Driver
{
    interface IMedicalStore
    {
        void addMedicines(Medicine medicine);

        void addCustomer(Customer customer);

        void displayMedicines(List<Medicine> mList)
        {
            foreach(Medicine item in mList)
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine("Name "+item.Name);
                Console.WriteLine("Quantity "+item.Quantity);
                Console.WriteLine("Price Per Quantity "+item.PricePerMedicine);
                Console.WriteLine("-------------------------");
            }
        }

        void displayCustomers(List<Customer> cList)
        {
            foreach(Customer item in cList)
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("Name "+item.Name);
                Console.WriteLine("Id "+item.Id);
                Console.WriteLine("Age "+item.Age.GetValueOrDefault()); //Y becoz age is nullable type.
               foreach(Medicine item2 in item.Medicines)
               {
                    Console.WriteLine(item2.Name);
                    Console.WriteLine(item2.Quantity);
                    Console.WriteLine(item2.PricePerMedicine);
               }
                Console.WriteLine("--------------------------");
            }
        }

        void displayStoreDetails(MedicalStore store)
        {
            Console.WriteLine(store.OwnerName1);
            Console.WriteLine(store.Address1);
            Console.WriteLine(MedicalStore.Pincode);
        }
    }
}
