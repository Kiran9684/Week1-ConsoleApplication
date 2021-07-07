using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalStoreApplication.CustomExceptions
{
    class InnerExceptionDemo : Exception
    {
        public InnerExceptionDemo() : base()
        {

        }

        public InnerExceptionDemo(string message) : base(message)
        {

        }

        //to provide our class the ability to track inner exceptions 
        public InnerExceptionDemo(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
