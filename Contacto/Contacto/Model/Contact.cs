using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacto
{
    class ContactModel
    {

        private int ID { get; set; }
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private string PhoneNumber { get; set; }


        private Dictionary<string, string> contactList = new Dictionary<string,string>();
        ContactModel()
        { 
        
        }
    }
}
