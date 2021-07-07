using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalStoreApplication.StackAllokDemo
{
    class Class1
    {
        unsafe static string GetStringStackalloc()
        {
            
            char* arr = stackalloc char[10];
            for (int i = 0; i < 10; i++)
            {
                arr[i] = 'a';
            }
         
            arr[10] = '\0';
            // return new string(buffer);
            return new string(arr);
        }

        static void Main()
        {
           
            Console.WriteLine(GetStringStackalloc());
           
        }
    }
}
