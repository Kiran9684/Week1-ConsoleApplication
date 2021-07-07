using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalStoreApplication.CustomExceptions
{
    public class MedicineNotFoundException : Exception
    {

        public MedicineNotFoundException():base()
        {

        }

        public MedicineNotFoundException(string message): base(message)
        {

        }
    }
}
