using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacto.Model
{
    //This class is used to create a contact object to be added to the contact list. 
    class Contact
    {
        private string _uniqueContactID;
     public string uniqueContactID
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


        private Dictionary<string, string> contactAttributes = new Dictionary<string,string>();
      public  Contact(string first, string last, string uniqueID)
        {
            this.firstName = first;
            uniqueContactID = uniqueID;
            contactAttributes.Add("Unique ID", uniqueContactID);
            contactAttributes.Add("First Name:", firstName);
        }

      public Contact()
      {
      
      }
    }
}
