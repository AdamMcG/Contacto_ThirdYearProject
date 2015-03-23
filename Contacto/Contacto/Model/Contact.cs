using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contacto.Data;
using System.Collections.ObjectModel;
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

        struct DynamicFields
        {
            private string key;
            public string muKey
            { 
                get { return key; }
                set { key = value; }
            }
            string dValue;
            public string muValue
            {
                get { return dValue; }
                set { value = dValue; }
            }
            public string toString()
            { return "testing"; }
        }

        private ObservableCollection<DynamicFields> Dynamic = new ObservableCollection<DynamicFields>();

        public void fillDynamicFields()
        {
            for (int i = 0; i < customFields.Count; i++)
            {
                DynamicFields myfields = new DynamicFields();
                myfields.muKey = customFields.ElementAt(i).Key;
                myfields.muValue = customFields.ElementAt(i).Value;
                Dynamic.Add(myfields);
            }
        
        }

        
        private Dictionary<string, string> customFields = new Dictionary<string, string>();

        public Dictionary<string, string> muCustomFields 
        {

            get { return customFields; }
            set { customFields = value;  }
        
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
