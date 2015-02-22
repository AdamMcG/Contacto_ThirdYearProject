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


//this .cs file deals with the data of the app. This class is intended to add to take a set of data(contacts, for example) serialize it, then add it to the json
//database file. 
//It should also allow for retriv functions from the database, to deserialize the json data and add it to the model classes. 
namespace Contacto.Data
{
      
        public sealed class ContactDataSource{
            private async Task writeSerialiseToJson(ObservableCollection<Contact> serialisedContacts)
            {
                string jsonFile = "ContactData.json";
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ObservableCollection<Contact>));
                using (var stream = await ApplicationData.Current
                .LocalFolder.OpenStreamForWriteAsync(jsonFile, CreationCollisionOption.ReplaceExisting))
                {
                    ser.WriteObject(stream, serialisedContacts);
                }
            }


            private async Task readFromSerialisedJson(ObservableCollection<Contact> myContactList)
            {
                String myString = "";
                var myStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync("ContactData.json");
                using (StreamReader reader = new StreamReader(myStream))
                {
                    myString = await reader.ReadToEndAsync();

                }
            }

            private async void deserialiseJson(string a)
            {
                String content = string.Empty;
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
