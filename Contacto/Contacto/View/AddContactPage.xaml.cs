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
using Windows.UI.Popups;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Contacto
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddContactPage : Page
    {
        
        private int fieldCounter = 0;
        private int indexLocation = -1;
        ListView addList = new ListView();
        public List<string> MenuOptions { get; set; }


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
            {   InputBox.Visibility = Visibility.Visible;

            }

        
        }


        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            // YesButton Clicked! Let's hide our InputBox and handle the input text.
            InputBox.Visibility = Visibility.Collapsed;

            // Do something with the Input
            String input = InputTextBox.Text;


            fieldCounter++;
            indexLocation++;
            addList.Items.Add(AddViewModel.initalizePage(input));

            // Clear InputBox.
            InputTextBox.Text = String.Empty;
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            // NoButton Clicked! Let's hide our InputBox.
            InputBox.Visibility = Visibility.Collapsed;

            // Clear InputBox.
            InputTextBox.Text = String.Empty;
        }


        private void RemoveFieldBtn_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (NullReferenceException ex)
            {

                string message = ex.Message;
            }
        }

        private async void FinishBtn_Click(object sender, RoutedEventArgs e)
        {

            
            string ID = Guid.NewGuid().ToString();
            string Fname = getDetailsData(0);
            string Lname = getDetailsData(1);

            try
            {

                Contact newContact = new Contact(ID, Fname, Lname);


                for (int i = 2; i <= indexLocation; i++)
                {



                    string s = getFieldData(i);
                    string t = getDetailsData(i);

                    if (s == "" || t == "")
                    {
                        string dialog = "All fields must have entries";

                        MessageDialog messageDialog = new MessageDialog(dialog, "Empty Field");
                        messageDialog.Commands.Add(new UICommand("Ok", new UICommandInvokedHandler(CommandHandlers)));
                        await messageDialog.ShowAsync();
                        return;
                    }
                    else if (AddViewModel.muFieldData.Count != AddViewModel.muFieldData.Distinct().Count())
                    {

                        string dialog = "Fields cannot be duplicates";

                        MessageDialog messageDialog = new MessageDialog(dialog, "Duplicate");
                        messageDialog.Commands.Add(new UICommand("Ok", new UICommandInvokedHandler(CommandHandlers)));
                        await messageDialog.ShowAsync();
                        return;

                    }
                    else
                        newContact.muCustomFields.Add(s, t);
                }

                newContact.fillDynamicFields();

                if (newContact.muCustomFields.Keys.Contains<string>("Email Address"))
                {

                    for (int i = 0; i < newContact.muCustomFields.Keys.Count; i++)
                    {
                        if (newContact.muCustomFields.Keys.ElementAt<String>(i) == "Email Address")
                        {
                            newContact.muprimary_email_address = newContact.muCustomFields.Values.ElementAt<String>(i);
                        }

                    }
                }
                else
                {
                    newContact.muprimary_email_address = " ";
                }

                
                
                
                if (newContact.muCustomFields.Keys.Contains<String>("Home Phone"))
                {

                    for (int i = 0; i < newContact.muCustomFields.Keys.Count; i++)
                    {
                        if (newContact.muCustomFields.Keys.ElementAt<String>(i) == "Home Phone")
                        {
                            newContact.muprimary_contact_num = newContact.muCustomFields.Values.ElementAt<String>(i);

                        }

                    }
                }else if (newContact.muCustomFields.Keys.Contains<String>("Work Phone")){

                     for (int i = 0; i < newContact.muCustomFields.Keys.Count; i++)
                    {
                        if (newContact.muCustomFields.Keys.ElementAt<String>(i) == "Work Phone")
                        {
                            newContact.muprimary_contact_num = newContact.muCustomFields.Values.ElementAt<String>(i);

                        }
                     }
                }
                else if (newContact.muCustomFields.Keys.Contains<String>("Mobile Phone"))
                {

                    for (int i = 0; i < newContact.muCustomFields.Keys.Count; i++)
                    {
                        if (newContact.muCustomFields.Keys.ElementAt<String>(i) == "Mobile Phone")
                        {
                            newContact.muprimary_contact_num = newContact.muCustomFields.Values.ElementAt<String>(i);

                        }
                    }
                }
                else
                {

                    newContact.muprimary_contact_num = " ";
                }



                AddViewModel.addtocontactlist(newContact);
                AddViewModel.createNewContactList();


                Frame.Navigate(typeof(MainPage));
            }
            catch (Exception ex)
            {
                showException(ex.Message);
            }
    }


        public async void showException(string message){

            
                MessageDialog messageDialog = new MessageDialog(message, "Error");
                messageDialog.Commands.Add(new UICommand("Ok", new UICommandInvokedHandler(CommandHandlers)));
                await messageDialog.ShowAsync();
        }

        public void CommandHandlers(IUICommand commandLabel)
        {


            var Actions = commandLabel.Label;
            switch (Actions)
            {
                case "Ok":
                    break;

            }
        }

 
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

 
 
    }
}