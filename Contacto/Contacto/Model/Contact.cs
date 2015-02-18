using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacto
{
    //This class is used to create a contact object to be added to the contact list. 
    class Contact
    {
        private int _uniqueContactID;
     public int uniqueContactID
        {
        get { return _uniqueContactID; }
        set { _uniqueContactID = value; }
}
        private string firstName;
        public string mufirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        private Dictionary<string, string> contactList = new Dictionary<string,string>();
      public  Contact(string first, string last, int uniqueID)
        {
            firstName = first;
            uniqueContactID = uniqueID;
            contactList.Add("first name:", "");
            contactList.Add("last name:", "");
            contactList.Add("Phone number:", "");
        }
    }
}
