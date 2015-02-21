using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contacto.Data;
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

        private string phoneNumber;
        public string muPhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
        private string lastName;
        public string muLastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
    
        public Dictionary<string, string> contactAttributes = new Dictionary<string,string>();

        public Contact(Dictionary<string, string> myNewContact)
        { contactAttributes = myNewContact; }
        
        public Contact(string first, string last, string uniqueID)
        {
            firstName = first;
            uniqueContactID = uniqueID;
            contactAttributes.Add("Unique ID", uniqueContactID);
            contactAttributes.Add("First Name:", firstName);
            contactAttributes.Add("Last Name:", lastName);
            contactAttributes.Add("Phone Number:", phoneNumber);
        }

      public Contact()
      {
      
      }
    }
}
