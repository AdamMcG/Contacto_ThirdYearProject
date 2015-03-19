using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contacto.Data;
namespace Contacto.Model
{
    //This class is used to create a contact object to be added to the contact list. 
    public class Contact
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
        public string mulastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        private Dictionary<string, string> customFields = new Dictionary<string, string>();

        public void addNewField(string fieldName, string fieldData)
        {
            customFields.Add(fieldName, fieldData);

        }

        public Dictionary<string, string> getDictionary() {

            return customFields;
        }

        private string testing = "Testing";
        public string test
        {
            get { return testing; }
            set { testing = value; }
        }

        public override string ToString()
        {
            return mufirstName;
        }


        
        public Contact(string uniqueID,string first, string last, string phoneNumber)
        {
            firstName = first;
            uniqueContactID = uniqueID;
            lastName = last;
            this.phoneNumber = phoneNumber;
        }

      public Contact() {
          firstName = "test";
          lastName = "testing";
          uniqueContactID = "12";
            phoneNumber = "0000112";

      }
    }
}
