using Contacto.Model;
using Contacto.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using Windows.UI.Popups;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Contacto.View
{
    public sealed partial class updateContact : Page
    {

        UpdateContactViewModel defaultViewModel = new UpdateContactViewModel();

        Contact originalContact;
        Contact myContact;
        int fieldCounter = 0;
        int indexLocation = 0;
        public List<string> MenuOptions2 { get; set; }

        public updateContact()
        {
            this.InitializeComponent();

            MenuOptions2 = new List<string>
            {
                "Home Phone",
                "Work Phone",
                "Mobile Phone",
                "Email Address",
                "House Address",
                "Work Address",
                "Custom"
            };

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            defaultViewModel.pullFromJson();
            defaultViewModel.InitalizeGroups();
            fieldCounter = defaultViewModel.listOfContacts.Count;
            indexLocation = defaultViewModel.listOfContacts.Count - 1;

            myContact = (Contact)e.Parameter;
            originalContact = (Contact)e.Parameter;
                
            toAddGrid.DataContext = myContact;
            addGrid.DataContext = myContact;


        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {



            defaultViewModel.removeFromList(myContact);
            defaultViewModel.RefreshGroups(myContact);
            defaultViewModel.addtocontactlist(myContact);
            defaultViewModel.createNewContactList();

            Frame.Navigate(typeof(ContactDetail), myContact);
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ContactDetail), originalContact);
        }

        
        private void ListPickerFlyout_ItemsPicked(ListPickerFlyout sender, ItemsPickedEventArgs args)
        {
            string selection = (string)sender.SelectedItem;

            if (selection != "Custom")
            {


                myContact.muCustomFields.Add(selection, "");
                myContact.fillDynamicFields();
                myContact.deleteDuplicates();


                //Frame.Navigate(typeof(ContactDetail), myContact);

            }
            else
            {
                fieldCounter++;
                indexLocation++;

                InputBox.Visibility = Visibility.Visible;

            }

        }


        private void StackPanel_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {

            FlyoutBase.GetAttachedFlyout(sender as FrameworkElement).ShowAt(sender as FrameworkElement);


        }



        private async void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {




            int index = listofcustomfields.SelectedIndex;
            string keyToRemove = myContact.muCustomFields.Keys.ElementAt<string>(index);

            string dialog = "Are you sure you want to delete " + keyToRemove + "?";

 
            MessageDialog messageDialog = new MessageDialog(dialog, "Delete?");

            messageDialog.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(CommandHandlers)));
            messageDialog.Commands.Add(new UICommand("No", new UICommandInvokedHandler(CommandHandlers)));


            await messageDialog.ShowAsync();




        }



        public void CommandHandlers(IUICommand commandLabel)
        {

            var Actions = commandLabel.Label;
            switch (Actions)
            {
                case "Yes":
                    int index = listofcustomfields.SelectedIndex;
                    string keyToRemove =  myContact.muCustomFields.Keys.ElementAt<string>(index);

                    for (int i = 0; i < myContact.muCustomFields.Count; i++)
                    {
                        if (keyToRemove == myContact.muCustomFields.Keys.ElementAt<string>(i))
                        {

                            myContact.muCustomFields.Remove(keyToRemove);
                            myContact.dynamicProperty.RemoveAt(index);
                            myContact.deleteDuplicates();



                        }

                    }
     

                    
                    break;


                case "No":
                    break;

            }
        }



        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = Visibility.Collapsed;

            String input = InputTextBox.Text;



            myContact.muCustomFields.Add(input, "");
            myContact.fillDynamicFields();
            myContact.deleteDuplicates();



            InputTextBox.Text = String.Empty;
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = Visibility.Collapsed;

            InputTextBox.Text = String.Empty;
        }

    }
}