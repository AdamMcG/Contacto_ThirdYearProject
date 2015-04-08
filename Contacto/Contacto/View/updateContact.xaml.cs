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


                //defaultViewModel.removeFromList(myContact);
                myContact.muCustomFields.Add(selection, "");
                myContact.fillDynamicFields();

                //defaultViewModel.addtocontactlist(myContact);
                //defaultViewModel.createNewContactList();

 //               Frame.Navigate(typeof(updateContact), myContact);

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



        }



        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            // YesButton Clicked! Let's hide our InputBox and handle the input text.
            InputBox.Visibility = Visibility.Collapsed;

            // Do something with the Input
            String input = InputTextBox.Text;



          //  defaultViewModel.removeFromList(myContact);
            myContact.muCustomFields.Add(input, "");
            myContact.fillDynamicFields();

//            defaultViewModel.addtocontactlist(myContact);
//            defaultViewModel.createNewContactList();

//            Frame.Navigate(typeof(updateContact), myContact);



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

    }
}