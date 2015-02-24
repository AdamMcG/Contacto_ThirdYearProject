using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contacto.Data;
using System.Collections.ObjectModel;
using Contacto.Model;
using System.Collections;
using Windows.Storage;
using Windows.Data.Json;
namespace Contacto.ViewModel
{
    //This handles the main page business logic. It should 
    class MainPageViewModel:INotifyPropertyChanged
    {
        private string data = "testing";
        public string dating
        {
            get { return data; }
            set
            {
                {
                    if (value != data)
                    {
                        data = value;
                        NotifyPropertyChanged("data");
                    }
                }
            }
        }
       private static MainPageViewModel contData = new MainPageViewModel();
        
        private ObservableCollection<Contact> contactlist = new ObservableCollection<Contact>();
        public ObservableCollection<Contact> listOfContacts
        {
            get { return contactlist; }
        }

        public void addtolist()
        {
            Contact testContact = new Contact("1", "Testing 123 testing","Ad","1232");
            contactlist.Add(testContact);
        }
            public MainPageViewModel() {
                addtolist();
                
        }

            public ObservableCollection<Contact> Convert(IEnumerable original)
            {
                return new ObservableCollection<Contact>(original.Cast<Contact>());
            }

            public async void contactListFill()
            {
                var myContact = await ContactDataSource.getContactListAsync();
                contactlist = Convert(myContact);
            }

            public static async Task<IEnumerable<Contact>> getContactListAsync()
            {
                await contData.getContactDataAsync();

                return contData.contactlist;
            }

            //This method should return the contact that matches the uniqueID in the JSON file
            public static async Task<Contact> getContactAsync(string uniqueID)
            {
                await contData.getContactDataAsync();
                var match = contData.contactlist.Where((Contact) => Contact.contactAttributes.ContainsKey(uniqueID));
                if (match.Count() == 1) { return match.First(); }
                return null;
            }

            //this is the method which pulls the data from the JSON file. 
            private async Task getContactDataAsync()
            {
                if (this.contactlist.Count() != 0)
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
                       contactObject["lastName"].GetString(),
                       contactObject["phoneNumber"].GetString());
                    contactlist.Add(c);
                }
            }



        public event PropertyChangedEventHandler PropertyChanged;


        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handle = PropertyChanged;
            if (null != handle)
            { handle(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}
