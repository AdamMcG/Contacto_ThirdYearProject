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
using Microsoft.WindowsAzure.MobileServices;
using Windows.System.Profile;
using Windows.Security.Cryptography.Core;
using Windows.Security.Cryptography;
namespace Contacto.ViewModel
{
    //This handles the main page business logic.
    class MainPageViewModel : INotifyPropertyChanged
    {
        
        string contactFile;
        string groupFile;
        string name = "contacts.json";
        string name2 = "groups.json";

        private ObservableCollection<Backup> backup = new ObservableCollection<Backup>();
        public ObservableCollection<Backup> myBackup
        {
            get { return backup; }
            set { backup = value;
            NotifyPropertyChanged("backup");
            }
        }
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
            set { groupList = value;

            NotifyPropertyChanged("groupList");

            
            }
        }

        public MainPageViewModel()
        {
        }

        public async void insertItem()
        {
            await insertBackupItem(); 
        }


        //http://stackoverflow.com/questions/23321484/device-unique-id-in-windows-phone-8-1
        private string GetDeviceID()
        {
            HardwareToken token = HardwareIdentification.GetPackageSpecificToken(null);
            IBuffer hardwareId = token.Id;

            HashAlgorithmProvider hasher = HashAlgorithmProvider.OpenAlgorithm("MD5");
            IBuffer hashed = hasher.HashData(hardwareId);

            string hashedString = CryptographicBuffer.EncodeToHexString(hashed);
            return hashedString;
        }


        private async System.Threading.Tasks.Task insertBackupItem()
        {
            try{

                Backup back = new Backup();
                back.itemID = GetDeviceID();
                string name = Windows.Networking.Proximity.PeerFinder.DisplayName;
                back.title = name + " backup";
                back.myContactFile = contactFile;
                back.myGroupFile = groupFile;
                back.date = DateTime.UtcNow.ToString();
                await App.Contacto4Client.GetTable<Backup>().InsertAsync(back);
                
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }


        public async void Fillbackup()
        {
            IMobileServiceTable<Backup> todoTable = App.Contacto4Client.GetTable<Backup>();
            //myBackup = new ObservableCollection<Backup>(await App.Contacto4Client.GetTable<Backup>().ToListAsync());
            List<Backup> items = await todoTable.Where(Backup => Backup.itemID == GetDeviceID()).ToListAsync(); 
            //await todoTable.DeleteAsync(items.ElementAt(0));
            myBackup = new ObservableCollection<Backup>(items); 

   //         List<TodoItem> items = await todoTable
   //.Where(todoItem => todoItem.Complete == false)
   //.ToListAsync();

            
        }
        public void getBackUpitems(int index)
        {
            BackUpitems(index);
        }


        private async void BackUpitems(int index)
        {
            try
            {
                StorageFolder folder = ApplicationData.Current.LocalFolder;
                StorageFile File = await folder.CreateFileAsync(name, CreationCollisionOption.ReplaceExisting);
                StorageFile File2 = await folder.CreateFileAsync(name2, CreationCollisionOption.ReplaceExisting);
                await Windows.Storage.FileIO.WriteTextAsync(File, myBackup.ElementAt(index).myContactFile);
                await Windows.Storage.FileIO.WriteTextAsync(File2, myBackup.ElementAt(index).myGroupFile);
            }
            catch (Exception e)
            { e.ToString(); }
        }

   


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
                groupFile = content;
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
                contactFile = content;
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }


        public void removeFromGroups(Contact toRemove)
        {
            //initalizeGroupsJson();

            for (int i = 0; i < groupList.Count; i++)
            {

                for (int j = 0; j < groupList.ElementAt(i).contactList.Count; j++ )
                {
                    if (toRemove.uniqueContactID == groupList.ElementAt(i).contactList.ElementAt(j).uniqueContactID)
                    {
                        var temp = groupList.ElementAt(i);
                        groupList.RemoveAt(i);
                        temp.contactList.RemoveAt(j);
                        groupList.Add(temp);
                    }
                }

                
            }

            SerialiseGroups();

        }

        public async void deleteUser(int index_To_Delete)
        {
            


            Contact toRemove = contactlist.ElementAt<Contact>(index_To_Delete);
            contactlist.RemoveAt(index_To_Delete);
            removeFromGroups(toRemove);
            await Task.Delay(200);


            NotifyPropertyChanged("contactlist");
            NotifyPropertyChanged("groupList");


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

        public void addGroup(Group toAdd)
        {
            groupList.Add(toAdd);
        }


        public void removeGroup(Group toRemove)
        {

            groupList.Remove(toRemove);

        }


    }
}