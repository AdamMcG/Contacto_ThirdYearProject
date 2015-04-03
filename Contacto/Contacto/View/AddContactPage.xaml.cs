using System;
using System.Collections.Generic;
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Contacto.ViewModel;
using Contacto.Model;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Contacto
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddContactPage : Page
    {
        
        private static int fieldCounter = 0;
        private static int indexLocation = -1;
        ListView addList = new ListView();

        public List<string> MenuOptions { get; private set; }


        AddContactViewModel AddViewModel = new AddContactViewModel();


        public AddContactPage()
        {


            this.InitializeComponent();

            MenuOptions = new List<string>
            {
                "Home Phone",
                "Work Phone",
                "Mobile Phone",
                "Email Address",
                "House Address",
                "Work Address",
                "Custom"
            };

            addList.HorizontalAlignment = HorizontalAlignment.Stretch;
            addList.Height = 500;
            addList.VerticalAlignment = VerticalAlignment.Top;
            toAddGrid.Children.Add(addList);




            addList.Items.Add(AddViewModel.initalizePage("First Name"));
            addList.Items.Add(AddViewModel.initalizePage("Last Name"));




            fieldCounter = fieldCounter + 2;
            indexLocation = indexLocation + 2;
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        //    var nav = (NavigationContext)e.Parameter;

            AddViewModel.pullFromJson();
        }



        public string getFieldData(int i)
        {
            try
            {
                string toGet;
                if (i < AddViewModel.muFieldData.Count())
                {
                    toGet = AddViewModel.muFieldData.ElementAt<TextBox>(i).Text;
                    return toGet;
                }
                else
                {
                    toGet = "";
                    return toGet;
                }
            }
            catch (Exception ex)
            {

                ex.ToString();
            }

            return "";
        }

        public string getDetailsData(int i)
        {
            try
            {
                string toGet;
                if (i < AddViewModel.muDetailsData.Count())
                {
                    toGet = AddViewModel.muDetailsData.ElementAt<TextBox>(i).Text;
                    return toGet;
                }
                else
                {
                    toGet = "";
                    return toGet;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return "";
        }

        private void ListPickerFlyout_ItemsPicked(ListPickerFlyout sender, ItemsPickedEventArgs args)
        {
            string selection = (string)sender.SelectedItem;

            if (selection != "Custom")
            {
                fieldCounter++;
                indexLocation++;
                addList.Items.Add(AddViewModel.initalizePage(selection));
            }
            else
            {
                fieldCounter++;
                indexLocation++;
                StackPanel stackPan = AddViewModel.createList(); 
                addList.Items.Add(stackPan);

            }

        }


        //private void AddFieldBtn_Click(object sender, RoutedEventArgs e)
        //{




        //fieldCounter++;
        //indexLocation++;

        //StackPanel stackPan = AddViewModel.createList(); 

        //addList.Items.Add(stackPan);

        //}

        private void RemoveFieldBtn_Click(object sender, RoutedEventArgs e)
        {
            if (fieldCounter > 2)
            {
                addList.Items.RemoveAt(indexLocation);
                fieldCounter--;
                indexLocation--;
            }
            else
            {
                return;
            }
        }
 
        private void FinishBtn_Click(object sender, RoutedEventArgs e)
        {

            string ID = Guid.NewGuid().ToString();
            string Fname = getDetailsData(0);
            string Lname = getDetailsData(1);

            Contact newContact = new Contact(ID, Fname, Lname);

            for (int i = 2; i <= indexLocation; i++)
            {

                string s = getFieldData(i);
                string t = getDetailsData(i);

                newContact.muCustomFields.Add(s, t);
            }

                newContact.fillDynamicFields();

            AddViewModel.addtocontactlist(newContact);
            AddViewModel.createNewContactList();

            
            Frame.Navigate(typeof(MainPage));
        }
 
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

 
 
    }
}