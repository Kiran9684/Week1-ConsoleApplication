using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalStoreApplication.Entities
{
    [Serializable]
    public abstract class Owner
    {
        private string OwnerName;

        public Owner()
        {

        }
        public Owner(string ownerName)
        {
            this.OwnerName1 = ownerName;
        }

        public string OwnerName1 { get => OwnerName; set => OwnerName = value; }
    }
}
