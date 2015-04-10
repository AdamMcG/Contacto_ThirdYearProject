using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contacto.Model;
using Windows.Storage;
using Windows.Storage.Streams;
using Newtonsoft.Json;
namespace Contacto.ViewModel
{
   //this class is used to handle the business logic of the Group function
    //Add group, update group and delete group should be implimented in this class. 
    class GroupViewModel: INotifyPropertyChanged
    {
        private ObservableCollection<Group> Group = new ObservableCollection<Group>();
        public ObservableCollection<Group> myGroup{ get { return Group; } }
        private ObservableCollection<Contact> globalListOfContacts = new ObservableCollection<Contact>();
        public ObservableCollection<Contact> globalContacts{ get { return globalListOfContacts; } }
        private ObservableCollection<Contact> localListOfContactsForGroups = new ObservableCollection<Contact>();
        public ObservableCollection<Contact> localContacts
        { get { return localListOfContactsForGroups; }
            set
            {
                if (localListOfContactsForGroups != value)
                { localListOfContactsForGroups = value; }
            }
        }

        private string _groupName = " ";
        public string groupName { get { return _groupName; } set { _groupName = value; } }
       public GroupViewModel(){
            
        }

       public void addGroup()
       {
           Group group = new Group();
           group.myGroupName = groupName;
           group.contactList= localListOfContactsForGroups;
           myGroup.Add(group);
       }

       public void addGroup(Group toAdd)
       {
           myGroup.Add(toAdd);
       }


       public void removeGroup(Group toRemove)
       {

           myGroup.Remove(toRemove);

       }




       public void initaliseGroup()
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
               if (myGroup != null)
               {
                   myGroup.Clear();
               }

               foreach (Group g in list)
                   if (g != null)
                       myGroup.Add(g);
               NotifyPropertyChanged("Grouplist");
           }
           catch (Exception e)
           {
               e.ToString();
           }
       }

   
       private async void SerialisingGroupsWithJsonNetAsync()
       {

           string name = "groups.json";
           ObservableCollection<Group> list = myGroup;
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
                   textWriter.Dispose();
               }
               textStream.Dispose();
           }
       }

       public void serailizeGroups()
       {
           SerialisingGroupsWithJsonNetAsync();

       }

       public void fillcontactList()
       {
           pullFromList();
       }

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
               { globalContacts.Add(c); }
           }
           catch (Exception e)
           { e.ToString(); }

       }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handle = PropertyChanged;
            if (null != handle)
            {
                handle(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
