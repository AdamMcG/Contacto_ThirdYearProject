//using Contacto.Model;
//using Contacto.ViewModel;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Windows.Storage;
//using Windows.Storage.Streams;

//namespace Contacto.Common
//{
//    class JSON_work
//    {

//        string name = "contacts.json";
//        MainPageViewModel main = new MainPageViewModel();

//        public async void SerialisingListWithJsonNetAsync(ObservableCollection<Contact> listOfContacts)
//        {
//            ObservableCollection<Contact> list = listOfContacts;

//            // Serialize our Product class into a string
//            // Changed to serialze the List
//            string jsonContents = JsonConvert.SerializeObject(list);

//            // Get the app data folder and create or replace the file we are storing the JSON in.
//            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
//            StorageFile textFile = await localFolder.CreateFileAsync(name, CreationCollisionOption.ReplaceExisting);

//            // Open the file...
//            using (IRandomAccessStream textStream = await textFile.OpenAsync(FileAccessMode.ReadWrite))
//            {
//                // write the JSON string!
//                using (DataWriter textWriter = new DataWriter(textStream))
//                {
//                    textWriter.WriteString(jsonContents);
//                    await textWriter.StoreAsync();
//                }
//            }
//        }

//        //This method deserialises a list and sets it as the contact list.
//        public async void buildMyListWithJson()
//        {
//            ObservableCollection<Contact> list = new ObservableCollection<Contact>();
//            try
//            {
//                string JSONFILENAME = "contacts.json";
//                string content = " ";
//                StorageFile File = await ApplicationData.Current.LocalFolder.GetFileAsync(JSONFILENAME);
//                using (IRandomAccessStream testStream = await File.OpenAsync(FileAccessMode.Read))
//                {
//                    using (DataReader dreader = new DataReader(testStream))
//                    {
//                        uint length = (uint)testStream.Size;
//                        await dreader.LoadAsync(length);
//                        content = dreader.ReadString(length);
//                        list = JsonConvert.DeserializeObject<ObservableCollection<Contact>>(content);
//                    }
//                }
//              // main.listOfContacts = list;
//            }
//            catch (Exception e)
//            { e.ToString(); }
//        }
            

//    }
//}
