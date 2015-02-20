using System;
using Contacto;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.IO;

namespace Contacto.Model
{
    //This class is used to create a group made up of select contacts from the contact list. 
    //contacts should be able to be a member of many groups. 
    class Group
    {
        Group(int ID, string name, ObservableCollection<Contact> myContacts)
        {
            uniqueGroupID = ID;
            groupName = name;
            myGroup = myContacts;
        }

        public Group(Contact myContact)
        {
            this.myContact = myContact;
            addToMainGroup(myContact);
            writeSerialiseToJson(myContactList);
        }

        private int uniqueGroupID;
        public int muGroup{ 
        get {return uniqueGroupID;}
        set { uniqueGroupID = value; }
        }
        private string groupName;
        public string myGroupName{
        get { return groupName; }
        set { groupName = value; }
        }

        private ObservableCollection<Contact> myGroup = new ObservableCollection<Contact>();
        private ObservableCollection<Contact> myContactList;
        private Contact myContact;

        public void addToMainGroup(Contact c)
        {
            myContactList.Add(c);
        }
        
        public void writeSerialiseToJson(ObservableCollection<Contact> serialisedContacts)
        {
            Stream jsonStream ;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ObservableCollection<Contact>));
            ser.WriteObject(jsonStream,serialisedContacts);
           
        }
    }
}
