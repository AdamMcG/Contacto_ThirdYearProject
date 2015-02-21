using System;
using Contacto;
using System.Collections.Generic;
using Windows.Storage;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.IO;
using Contacto.Data;

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
            myContactList.Add(myContact);
           
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
        private ObservableCollection<Contact> myContactList = new ObservableCollection<Contact>();
        private Contact myContact;

        private async Task writeSerialiseToJson(ObservableCollection<Contact> serialisedContacts)
        {
         string jsonFile = "ContactData.json";
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ObservableCollection<Contact>));
                using (var stream = await ApplicationData.Current
                .LocalFolder.OpenStreamForWriteAsync(jsonFile, CreationCollisionOption.ReplaceExisting)){
               ser.WriteObject(stream,serialisedContacts);
            } 
        }


        private async Task readFromSerialisedJson(ObservableCollection<Contact>myContactList) 
        {
            String myString = "";
            var myStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync("ContactData.json");
            using (StreamReader reader = new StreamReader(myStream))
            {
                myString = await reader.ReadToEndAsync();

            }
        }

        private async void deserialiseJson(string a)
        { String content = string.Empty;
        ObservableCollection<Contact> contList;
        var jsonSerialiser = new DataContractJsonSerializer(typeof(ObservableCollection<Contact>));
        var myStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync("ContactData.json");
        contList = (ObservableCollection<Contact>)jsonSerialiser.ReadObject(myStream);
        foreach (var contact in contList)
        {
            content += String.Format("ID:{0}, Firstname:{1},Lastname:{2},phoneNumber{3}", 
                contact.contactAttributes.ContainsKey("Unique ID"),
                contact.contactAttributes.ContainsKey("First Name:"),
                contact.contactAttributes.ContainsKey("Last Name:"), 
                contact.contactAttributes.ContainsKey("Phone Number: "));
        }
        a = content;
        }
    
    }
}
