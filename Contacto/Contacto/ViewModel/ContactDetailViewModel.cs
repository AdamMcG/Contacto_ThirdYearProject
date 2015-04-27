using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Contacto.Model;
using Windows.Storage;
using Windows.Storage.Streams;
using Newtonsoft.Json;

namespace Contacto.ViewModel
{

    class ContactDetailViewModel
    {
        private ObservableCollection<Contact> contactlist = new ObservableCollection<Contact>();
        public ObservableCollection<Contact> listOfContacts { get { return contactlist; } }



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

        private async void SerialiseNewList()
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

        public void createNewContactList()
        { SerialiseNewList(); }


        public void openAppointmentCalendar()
        { }

    }
}