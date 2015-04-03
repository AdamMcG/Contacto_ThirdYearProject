using System;
using Contacto;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows.Storage;
using System.Collections.ObjectModel;
using Contacto.Model;
using System.Runtime.Serialization.Json;
using Windows.Data.Json;


//this .cs file deals with the data of the app. This class is intended to add to take a set of data(contacts, for example) serialize it, then add it to the json
//database file. 
//It should also allow for retriv functions from the database, to deserialize the json data and add it to the model classes. 
namespace Contacto.Data
{
      
        public sealed class ContactDataSource{

            private static ContactDataSource contData = new ContactDataSource();
           
            public ContactDataSource() { }

            private ObservableCollection<Contact> _contactList = new ObservableCollection<Contact>();
            public ObservableCollection<Contact> contactList
            {
                get { return _contactList; }
            }

            //This method should return the entire list of contacts from the JSON file
            public static async Task<IEnumerable<Contact>> getContactListAsync()
            {
                await contData.getContactDataAsync();

                return contData.contactList;
            }

            //This method should return the contact that matches the uniqueID in the JSON file
         /*   public static async Task<Contact> getContactAsync(string uniqueID)
            {
                await contData.getContactDataAsync();
                var match = contData.contactList.Where((Contact) => Contact.contactAttributes.ContainsKey(uniqueID));
                if (match.Count() == 1) { return match.First(); }
                return null;
            }*/

            //this is the method which pulls the data from the JSON file. 
            private async Task getContactDataAsync()
            {
                if (this.contactList.Count() != 0)
                    return;

                Uri dataUri = new Uri("ms-appx///Data/ContactData.json");
                StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
                string jsonText = await FileIO.ReadTextAsync(file);
                JsonObject jsonObject = JsonObject.Parse(jsonText);
                JsonArray jsonArray = jsonObject["Contacts"].GetArray();

                foreach (JsonValue groupValue in jsonArray)
                {
                    JsonObject contactObject = groupValue.GetObject();
                    Contact c = new Contact(
                        contactObject["uniqueID"].GetString(),
                       contactObject["firstName"].GetString(),
                       contactObject["lastName"].GetString());
                    contactList.Add(c);
                }
            }

            public async Task writeSerialiseToJson(ObservableCollection<Contact> myContact)
            {
                string jsonFile = "ContactData.json";
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ObservableCollection<Contact>));
                using (var stream = await ApplicationData.Current
                .LocalFolder.OpenStreamForWriteAsync(jsonFile, CreationCollisionOption.ReplaceExisting))
                {
                    ser.WriteObject(stream, myContact);
                }
                
            }


            public async Task readFromSerialisedJson()
            {
                String myString = "";
                var myStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync("ContactData.json");
                using (StreamReader reader = new StreamReader(myStream))
                {
                    myString = await reader.ReadToEndAsync();

                }
            }

            public  void deserialiseJson(string a)
            {

                return;
            }
    
        }

}
