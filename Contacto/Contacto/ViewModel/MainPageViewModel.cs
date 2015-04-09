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
    class MainPageViewModel : INotifyPropertyChanged
    {



        private ObservableCollection<Contact> contactlist = new ObservableCollection<Contact>();
        public ObservableCollection<Contact> listOfContacts
        {
            get { return contactlist; }
            set
            {
                contactlist = value;

                NotifyPropertyChanged("contactlist");
            }
        }

        private ObservableCollection<Group> groupList = new ObservableCollection<Group>();
        public ObservableCollection<Group> listOfGroups
        {
            get { return groupList; }
        }

        public MainPageViewModel()
        {
        }


        String name = "contacts.json";
        String name2 = "groups.json";


        //This is serialising a list and adding to the json file. 
        private async void SerialisingGroupsWithJsonNetAsync()
        {
            ObservableCollection<Group> list = listOfGroups;
            // Changed to serialze the List
            string jsonContents = JsonConvert.SerializeObject(list);

            // Get the app data folder and create or replace the file we are storing the JSON in.
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile textFile = await localFolder.CreateFileAsync(name2, CreationCollisionOption.ReplaceExisting);

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

        public void SerialiseGroups()
        { SerialisingGroupsWithJsonNetAsync(); }

        public void InitalizeGroups()
        { initalizeGroupsJson(); }
        private async void initalizeGroupsJson()
        {
            List<Group> list = new List<Group>();
            try
            {
                string JSONFILENAME = "groups.json";
                string content = " ";
                StorageFile File = await ApplicationData.Current.LocalFolder.GetFileAsync(JSONFILENAME);
                IRandomAccessStream testStream = await File.OpenAsync(FileAccessMode.Read);

                using (testStream)
                {
                    using (DataReader dreader = new DataReader(testStream)) {
                        uint length = (uint)testStream.Size;
                        await dreader.LoadAsync(length);
                        content = dreader.ReadString(length);
                        list = JsonConvert.DeserializeObject<List<Group>>(content);
                        dreader.Dispose();
                    }

                }
                testStream.Dispose();
                if (listOfGroups != null)
                {
                    listOfGroups.Clear();
                }

                foreach (Group g in list)
                    if (g != null)
                        listOfGroups.Add(g);
                NotifyPropertyChanged("Grouplist");
            }
            catch (Exception e){
                e.ToString();
            }
        }

        //This is serialising a list and adding to the json file. 
        private async void SerialisingListWithJsonNetAsync()
        {
            ObservableCollection<Contact> list = listOfContacts;
            // Changed to serialze the List
            string jsonContents = JsonConvert.SerializeObject(list);

            // Get the app data folder and create or replace the file we are storing the JSON in.
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile textFile = await localFolder.CreateFileAsync(name, CreationCollisionOption.ReplaceExisting);

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


        public void serialiseList()
        { SerialisingListWithJsonNetAsync(); }
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

                foreach (Contact c in list)
                    if (c != null)
                        contactlist.Add(c);


                NotifyPropertyChanged("contactlist");
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        public void deleteUser(int index_To_Delete)
        {

            contactlist.RemoveAt(index_To_Delete);
            NotifyPropertyChanged("contactlist");


            List<Contact> temp = new List<Contact>();

            for (int i = 0; i < contactlist.Count(); i++)
            {

                temp.Add(contactlist.ElementAt<Contact>(i));
            }

            contactlist.Clear();

            for (int i = 0; i < temp.Count(); i++)
            {
                contactlist.Add(temp.ElementAt<Contact>(i));
            }


            NotifyPropertyChanged("contactlist");
            SerialisingListWithJsonNetAsync();



        }


        public void initalizeList()
        {
            initalizeListJson();

        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(String propertyName)
        {

            PropertyChangedEventHandler handle = PropertyChanged;
            if (null != handle)
            {
                handle(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}