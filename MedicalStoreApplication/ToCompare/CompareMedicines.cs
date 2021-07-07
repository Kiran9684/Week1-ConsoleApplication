using MedicalStoreApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalStoreApplication.ToCompare
{
    class CompareMedicines : IComparer<Medicine>
    {
        public int Compare(Medicine x, Medicine y)
        {
            if (x.Name.CompareTo(y.Name) > 0)
            {
                return 1;
            }
            else if (x.Name.CompareTo(y.Name) < 0)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
