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
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Contacto.ViewModel
{
    class AddContactViewModel
    {
        private  List<TextBox> fieldData = new List<TextBox>();

        public List<TextBox> muFieldData
        {
            get { return fieldData;  }
            set { fieldData = value; }

        }

        private List<TextBox> detailsData = new List<TextBox>();

        public List<TextBox> muDetailsData
        {
            get { return detailsData; }
            set { detailsData = value; }

        }




        private ObservableCollection<Contact> contactlist = new ObservableCollection<Contact>();
        public ObservableCollection<Contact> listOfContacts { get { return contactlist; } }

        //This for the static fields first name, last name and phone number
        public StackPanel initalizePage(string fieldText)
        {

            StackPanel stackPan = new StackPanel();
            stackPan.Height = 80;
            stackPan.Width = 370;
            stackPan.Orientation = Orientation.Horizontal;

            Thickness margin = stackPan.Margin;
            margin.Bottom = 10;
            margin.Top = 10;
            stackPan.Margin = margin;

            TextBlock fieldName = new TextBlock();
            fieldName.Width = 180;
            fieldName.FontSize = 20;
            fieldName.VerticalAlignment = VerticalAlignment.Center;
            fieldName.HorizontalAlignment = HorizontalAlignment.Right;
            fieldName.Text = fieldText;
            margin = fieldName.Margin;
            margin.Right = 10;
            margin.Left = 10;
            fieldName.Margin = margin;


            TextBox detailEntry = new TextBox();
            detailEntry.Width = 160;
            detailEntry.VerticalAlignment = VerticalAlignment.Center;
            detailEntry.HorizontalAlignment = HorizontalAlignment.Right;
            detailEntry.PlaceholderText = fieldText;
            margin = detailEntry.Margin;
            margin.Right = 10;
            margin.Left = 10;
            detailEntry.Margin = margin;


            TextBox toAdd = new TextBox();
            toAdd.Text = fieldText;


            fieldData.Add(toAdd);
            detailsData.Add(detailEntry);

            stackPan.Children.Add(fieldName);
            stackPan.Children.Add(detailEntry);


            return stackPan;
        }
        //This is for dynamic fields
        public StackPanel createList()
        {
            StackPanel stackPan = new StackPanel();
            stackPan.Height = 80;
            stackPan.Width = 370;
            stackPan.Orientation = Orientation.Horizontal;

            TextBox fieldEntry = new TextBox();
            fieldEntry.PlaceholderText = "Field Name";
            fieldEntry.FontSize = 20;
            fieldEntry.Width = 180;
            fieldEntry.VerticalAlignment = VerticalAlignment.Center;
            fieldEntry.HorizontalAlignment = HorizontalAlignment.Left;


            Thickness margin = fieldEntry.Margin;
            margin.Right = 10;
            margin.Left = 10;
            fieldEntry.Margin = margin;


            TextBox detailsEntry = new TextBox();
            detailsEntry.Width = 160;
            detailsEntry.PlaceholderText = "Details";
            detailsEntry.VerticalAlignment = VerticalAlignment.Center;
            detailsEntry.HorizontalAlignment = HorizontalAlignment.Right;


            margin = detailsEntry.Margin;
            margin.Right = 10;
            margin.Left = 10;
            detailsEntry.Margin = margin;


            stackPan.Children.Add(fieldEntry);
            stackPan.Children.Add(detailsEntry);

            fieldData.Add(fieldEntry);
            detailsData.Add(detailsEntry);


            return stackPan;

        }


        public void addtocontactlist(Contact c)
        { contactlist.Add(c); }

        public void pullFromJson()
        { pullFromList(); }

        private async void pullFromList()
        {
            ObservableCollection<Contact> list = new ObservableCollection<Contact>();
            try
            {
                foreach (Contact c in list)
                { c.deleteDuplicates(); }
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
                {
                    contactlist.Add(c); }
            }
            catch (Exception e)
            { e.ToString(); }
           
        }

        //This is serialising a list and adding to the json file. 
        private async void SerialiseNewList(){
            string name = "contacts.json";
            ObservableCollection<Contact> list = listOfContacts;
            // Changed to serialze the List
            string jsonContents = JsonConvert.SerializeObject(list);
            // Get the app data folder and create or replace the file we are storing the JSON in.
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile textFile = await localFolder.CreateFileAsync(name, CreationCollisionOption.ReplaceExisting);
            // Open the file...
            using (IRandomAccessStream text = await textFile.OpenAsync(FileAccessMode.ReadWrite)){
                // write the JSON string!
                using (DataWriter textWriter = new DataWriter(text)) {
                    textWriter.WriteString(jsonContents);
                    await textWriter.StoreAsync();
                    textWriter.Dispose();
                }
                text.Dispose();
            }
        }

        public void createNewContactList()
        { SerialiseNewList(); }




    }
}
