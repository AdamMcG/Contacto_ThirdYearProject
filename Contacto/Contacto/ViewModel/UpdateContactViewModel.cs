using Contacto.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Contacto.ViewModel
{
    class UpdateContactViewModel
    {
        string name2 = "groups.json";

        private ObservableCollection<Contact> contactlist = new ObservableCollection<Contact>();
        public ObservableCollection<Contact> listOfContacts { get { return contactlist; } }


        private ObservableCollection<Group> groupList = new ObservableCollection<Group>();
        public ObservableCollection<Group> listOfGroups
        {
            get { return groupList; }
            set
            {
                groupList = value;
            }
        }



        public void addtocontactlist(Contact c)
        { contactlist.Add(c); }

        public void removeFromList(Contact c)
        {
            for (int i = 0; i < contactlist.Count; i++)
            {
                if (c.uniqueContactID == contactlist.ElementAt<Contact>(i).uniqueContactID)
                {
                    contactlist.RemoveAt(i);
                }

            }

        }




        public void pullFromJson()
        { pullFromList(); }


        private async void pullFromList()
        {
            ObservableCollection<Contact> list = new ObservableCollection<Contact>();
            try
            {
                string JSONFILENAME = "contacts.json";
                string content = " ";
                StorageFile File = await ApplicationData.Current.LocalFolder.GetFileAsync(JSONFILENAME);
                using (IRandomAccessStream testS = await File.OpenAsync(FileAccessMode.Read))
                {
                    using (DataReader dreader = new DataReader(testS))
                    {
                        uint length = (uint)testS.Size;
                        await dreader.LoadAsync(length);
                        content = dreader.ReadString(length);
                        list = JsonConvert.DeserializeObject<ObservableCollection<Contact>>(content) as ObservableCollection<Contact>;
                        dreader.Dispose();
                    }
                    testS.Dispose();
                }
                foreach (Contact c in list)
                { contactlist.Add(c); }
            }
            catch (Exception e)
            { e.ToString(); }

        }

        private async Task SerialiseNewList()
        {
            string name = "contacts.json";
            ObservableCollection<Contact> list = listOfContacts;
            // Changed to serialze the List
            string jsonContents = JsonConvert.SerializeObject(list);
            // Get the app data folder and create or replace the file we are storing the JSON in.
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile textFile = await localFolder.CreateFileAsync(name, CreationCollisionOption.ReplaceExisting);
            // Open the file...
            using (IRandomAccessStream text = await textFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                // write the JSON string!
                using (DataWriter textWriter = new DataWriter(text))
                {
                    textWriter.WriteString(jsonContents);
                    await textWriter.StoreAsync();
                    textWriter.Dispose();
                }
                text.Dispose();
            }
        }


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
                    using (DataReader dreader = new DataReader(testStream))
                    {
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
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }


        public async Task createNewContactList()
        { await SerialiseNewList(); }


        public void RefreshGroups(Contact toRefresh)
        {
            for (int i = 0; i < groupList.Count; i++)
            {

                for (int j = 0; j < groupList.ElementAt(i).contactList.Count; j++)
                {
                    if (toRefresh.uniqueContactID == groupList.ElementAt(i).contactList.ElementAt(j).uniqueContactID)
                    {
                        var temp = groupList.ElementAt(i);
                        groupList.RemoveAt(i);
                        temp.contactList.RemoveAt(j);
                        temp.contactList.Add(toRefresh);
                        groupList.Add(temp);
                    }
                }


            }

            SerialiseGroups();

        }


    }
}
