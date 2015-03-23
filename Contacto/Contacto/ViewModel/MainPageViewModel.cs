using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Contacto.Data;
using System.Collections.ObjectModel;
using Contacto.Model;
using System.Collections;
using Windows.Storage;
using Windows.Data.Json;
using System.IO;
using Newtonsoft.Json;
using Windows.Storage.Streams;
using System.Collections.Specialized;
namespace Contacto.ViewModel
{
    //This handles the main page business logic.
    class MainPageViewModel:INotifyPropertyChanged
    {


        private ObservableCollection<Contact> contactlist = new ObservableCollection<Contact>();
        public ObservableCollection<Contact> listOfContacts{
            get { return contactlist; }
            set { contactlist = value;

            NotifyPropertyChanged("contactlist");
            }
        }

        private ObservableCollection<Group> groupList = new ObservableCollection<Group>();
        public ObservableCollection<Group> listOfGroups{
            get { return groupList; }
        }

            public MainPageViewModel() {
              
        }
 
        
        String name = "contacts.json";
        //This creates a contact, then serialises it into JSON and writes to the JSON file.
            private async void buildContactDataAsync()
            {
                try
                {
                    Contact c = new Contact();
                    string jsoncontent = JsonConvert.SerializeObject(c);
                   
                    StorageFile File = await ApplicationData.Current.LocalFolder.CreateFileAsync(name, CreationCollisionOption.ReplaceExisting);

                    using (IRandomAccessStream testStream = await File.OpenAsync(FileAccessMode.ReadWrite)){
                        using (DataWriter dwriter = new DataWriter(testStream)) {
                            dwriter.WriteString(jsoncontent);
                            await dwriter.StoreAsync();
                            
                        }
                    }
                }
                catch (IOException e)
                { e.ToString(); }
            }

        //This is serialising a list and adding to the json file. 
            private async void SerialisingListWithJsonNetAsync()
            {
                ObservableCollection<Contact> list = listOfContacts;
            // Changed to serialze the List
            string jsonContents = JsonConvert.SerializeObject(list);

            // Get the app data folder and create or replace the file we are storing the JSON in.
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile textFile = await localFolder.CreateFileAsync(name, CreationCollisionOption.OpenIfExists);

            // Open the file...
            using (IRandomAccessStream textStream = await textFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                // write the JSON string!
                using (DataWriter textWriter = new DataWriter(textStream))
                {
                    textWriter.WriteString(jsonContents);
                    await textWriter.StoreAsync();
                }
            }
        }

        //This method deserialises a list and sets it as the contact list.
            private async void initalizeListJson()
            {
                List<Contact> list = new List<Contact>();
                try
                {
                    string JSONFILENAME = "contacts.json";
                    string content = " ";
                    StorageFile File = await ApplicationData.Current.LocalFolder.GetFileAsync(JSONFILENAME);
                    IRandomAccessStream testStream = await File.OpenAsync(FileAccessMode.Read);

                    using (testStream)
                    {
                        using (DataReader dreader = new DataReader(testStream))
                        {
                            uint length = (uint)testStream.Size;
                            await dreader.LoadAsync(length);
                            content = dreader.ReadString(length);
                            list = JsonConvert.DeserializeObject<List<Contact>>(content);
                            dreader.Dispose();
                        }
                        
                    }
                    testStream.Dispose();
                    if (contactlist != null)
                    {
                        contactlist.Clear();
                    }
                    
                    foreach(Contact c in list)
                        if(c != null)
                         contactlist.Add(c);
                    
                    
                    NotifyPropertyChanged("contactlist");
                }
                catch (Exception e)
                {
                    e.ToString();
                }
            }

            public void deleteUser(String contactToRemove){
                string check = null;
                int a = 0;
                foreach (Contact c in contactlist)
                {
                    if (c.mufirstName == contactToRemove)
                    {
                        check = "to delete";
                        a = contactlist.IndexOf(c); }
                }
                if(check != null)
                contactlist.RemoveAt(a);
                NotifyPropertyChanged("contactlist");
            }

        public void initalizeList()
        {
           initalizeListJson();

        }


            
        //Deserialises the contact and adds xer to the list.
            private async void buildWithJsonNetAsync()
            {
                Contact c = new Contact();
                try {
                    string JSONFILENAME = "contacts.json";
                    string content = " ";
                    StorageFile File = await ApplicationData.Current.LocalFolder.GetFileAsync(JSONFILENAME);
                    using (IRandomAccessStream testStream = await File.OpenAsync(FileAccessMode.Read)){
                        using (DataReader dreader = new DataReader(testStream)){
                            uint length = (uint)testStream.Size;
                            await dreader.LoadAsync(length);
                            content = dreader.ReadString(length);
                            c = JsonConvert.DeserializeObject<Contact>(content);
                         
                        }
                    }
                listOfContacts.Add(c);       
                }
                catch (Exception e)
                {

                    e.ToString();
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
