using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalStoreApplication.Entities
{
    [Serializable]
    public class Customer : IComparable<Customer> ,IEquatable<Customer>
    {
        private int id;
        private string name;
        private int? age;
        private List<Medicine> medicines;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int? Age { get => age; set => age = value; }
        public List<Medicine> Medicines { get => medicines; set => medicines = value; }

        public int CompareTo(Customer other)
        {
            if (this.Id > other.Id)
            {
                return 1;
            }
            else if (this.Id < other.Id)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        public bool Equals(Customer other)
        {
            return (this.Id == other.Id);
        }
    }
}
