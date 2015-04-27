﻿using System;
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
using Contacto.View;
using Contacto.ViewModel;
using Contacto.Data;
using Contacto.Model;
using Contacto.Common;
using Windows.ApplicationModel.Resources;
using System.Threading.Tasks;
using Windows.UI.Popups;
using System.Net.NetworkInformation;
using Windows.Networking.Connectivity;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace Contacto
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        MainPageViewModel myMain = new MainPageViewModel();
        Dictionary<string, string> searchResults = new Dictionary<string, string>();

        bool singletap = true;
        bool singletapGroups = true;
        bool singletapbackup = true;
        public MainPage()
        {


            this.InitializeComponent();


            ContactListView.ItemsSource = myMain.listOfContacts;


            HeaderIcon1.Style = HeaderStyleSelected;
            HeaderIcon2.Style = HeaderStyleUnselected;
            HeaderIcon3.Style = HeaderStyleUnselected;
            
            
           
            this.NavigationCacheMode = NavigationCacheMode.Required;


        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.InitializeComponent();
            ContentArea.SelectedIndex = 1;
            myMain.initalizeList();
            myMain.InitalizeGroups();
            this.DataContext = myMain;
            BackupListView.ItemsSource = myMain.myBackup;
            ContactListView.ItemsSource = myMain.listOfContacts;
        }


        private void HeaderImg1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ContentArea.SelectedIndex = 0;
        }

        private void HeaderImg2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ContentArea.SelectedIndex = 1;
        }


        private void HeaderImg3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ContentArea.SelectedIndex = 2;
        }


        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {

            Frame.Navigate(typeof(AddContactPage));
   

        }

  

       
        private void addGroup_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddGroupPage));
        }

        private void ContentArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ContentArea.SelectedIndex == 0)
            {

                HeaderIcon1.Style = HeaderStyleSelected;
                HeaderIcon2.Style = HeaderStyleUnselected;
                HeaderIcon3.Style = HeaderStyleUnselected;


            }
            else if (ContentArea.SelectedIndex == 1)
            {

                HeaderIcon1.Style = HeaderStyleUnselected;
                HeaderIcon2.Style = HeaderStyleSelected;
                HeaderIcon3.Style = HeaderStyleUnselected;
                myMain.initalizeList();


                this.DataContext = myMain;

                ContactListView.ItemsSource = myMain.listOfContacts;


            }
            else if (ContentArea.SelectedIndex == 2)
            {
                HeaderIcon1.Style = HeaderStyleUnselected;
                HeaderIcon2.Style = HeaderStyleUnselected;
                HeaderIcon3.Style = HeaderStyleSelected;
                myMain.InitalizeGroups();
            }
            else {

                HeaderIcon1.Style = HeaderStyleSelected;
                HeaderIcon2.Style = HeaderStyleUnselected;
                HeaderIcon3.Style = HeaderStyleUnselected;
            
            }
        }

        
        
        private async void ContactListView_Tapped(object sender, TappedRoutedEventArgs e)
        {

            this.singletap = true;
            await Task.Delay(200);
            if (this.singletap)
        {
            try
            {

                int index = ContactListView.SelectedIndex;

                Frame.Navigate(typeof(ContactDetail), myMain.listOfContacts.ElementAt<Contact>(index));

            }
            catch (Exception ex)
            {

                ex.ToString();
            }
            }

        }



        private async void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {

            int index = ContactListView.SelectedIndex;
            string name =  myMain.listOfContacts.ElementAt<Contact>(index).mufirstName + " " + myMain.listOfContacts.ElementAt<Contact>(index).mulastName;
            string dialog = "Are you sure you want to delete " + name + "?";

            MessageDialog messageDialog = new MessageDialog(dialog, "Delete?");

            messageDialog.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(CommandHandlers)));
            messageDialog.Commands.Add(new UICommand("No", new UICommandInvokedHandler(CommandHandlers)));


            await messageDialog.ShowAsync();




        }


        private void findContact_Click(object sender, RoutedEventArgs e)
        {
            SearchBox.Visibility = Visibility.Visible;
            

        }



        private void searchForContact(string value, string searchfield)
        {

            List<Contact> resultList = new List<Contact>();

            if (searchfield == "firstName"){
                for (int i = 0; i < myMain.listOfContacts.Count; i++)
                {
                    if (value == myMain.listOfContacts.ElementAt<Contact>(i).mufirstName)
                    {
                        resultList.Add(myMain.listOfContacts.ElementAt<Contact>(i));

                    }

                }
            }
            else if (searchfield == "lastName")
            {
                for (int i = 0; i < myMain.listOfContacts.Count; i++)
                {
                    if (value == myMain.listOfContacts.ElementAt<Contact>(i).mulastName)
                    {
                        resultList.Add(myMain.listOfContacts.ElementAt<Contact>(i));

                    }

                }

            }

            for (int i = 0; i < resultList.Count; i++)
            {
                string id = resultList.ElementAt<Contact>(i).uniqueContactID;
                string FirstName = resultList.ElementAt<Contact>(i).mufirstName;
                string LastName = resultList.ElementAt<Contact>(i).mulastName;
                string Name = FirstName + " " + LastName;

                searchResults.Add(id, Name);

            }

            List<String> listOfResults = searchResults.Values.ToList<String>();
            resultPicker.ItemsSource = listOfResults;

        }

       

        public void CommandHandlers(IUICommand commandLabel)
        {

            int index = ContactListView.SelectedIndex;

            var Actions = commandLabel.Label;
            switch (Actions)
            {
                case "Yes":
                    myMain.deleteUser(index);
                    break;
                case "No":
                    break;
                
            }
        }

        private void StackPanel_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {

            this.singletap = false;
            FlyoutBase.GetAttachedFlyout(sender as FrameworkElement).ShowAt(sender as FrameworkElement);


        }
        


        private void YesButton_Click(object sender, RoutedEventArgs e)
        {

            // YesButton Clicked! Let's hide our InputBox and handle the input text.
            SearchBox.Visibility = Visibility.Collapsed;

            // Do something with the Input
            String input = InputTextBox.Text;
            if (searchOptions.SelectedIndex == 0)
            {
                searchForContact(InputTextBox.Text, "firstName");

            }
            else
            {
                searchForContact(InputTextBox.Text, "lastName");

            }

            // Clear InputBox.
            searchOptions.SelectedIndex = -1;
            InputTextBox.Text = String.Empty;
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            // NoButton Clicked! Let's hide our InputBox.
            SearchBox.Visibility = Visibility.Collapsed;

            // Clear InputBox.
            InputTextBox.Text = String.Empty;
            searchResults.Clear();
        }

        private void resultPicker_ItemsPicked(ListPickerFlyout sender, ItemsPickedEventArgs args)
        {

            int index = resultPicker.SelectedIndex;
            string id = searchResults.Keys.ElementAt<string>(index);
            searchResults.Clear();
            Contact toUse = new Contact();

            for (int i = 0; i < myMain.listOfContacts.Count; i++)
            {
                if (id == myMain.listOfContacts.ElementAt<Contact>(i).uniqueContactID)
                {
                    toUse = myMain.listOfContacts.ElementAt<Contact>(i);

                }

            }


            Frame.Navigate(typeof(ContactDetail), toUse);
        }

        private async void Group_Tapped(object sender, TappedRoutedEventArgs e)
        {

            this.singletapGroups = true;
            await Task.Delay(200);
            if (this.singletapGroups)
            {
                try
                {

                    Frame.Navigate(typeof(GroupDetail), myMain.listOfGroups.ElementAt(GridForGroups.SelectedIndex));

                }
                catch (Exception ex)
                {

                    ex.ToString();
                }
            }

        }


        private void sort(string order)
        {




            List<Contact> toSort = myMain.listOfContacts.ToList<Contact>(); ;

            switch (order)
            {
                case "f.asc":

                    var newList_f_ASC = toSort.OrderBy(x => x.mufirstName).ToList();
                    myMain.listOfContacts.Clear();


                    for (int i = 0; i < newList_f_ASC.Count(); i++)
                    {

                        myMain.listOfContacts.Add(newList_f_ASC.ElementAt<Contact>(i));
                    }

                    break;

                case "f.desc":

                    var newList_f_DESC = toSort.OrderByDescending(x => x.mufirstName).ToList();

                    
                    myMain.listOfContacts.Clear();


                    for (int i = 0; i < newList_f_DESC.Count(); i++)
                    {

                        myMain.listOfContacts.Add(newList_f_DESC.ElementAt<Contact>(i));
                    }
                    
                    break;

                case "l.asc":

                    var newList_L_ASC = toSort.OrderBy(x => x.mulastName).ToList();
                    
                    myMain.listOfContacts.Clear();


                    for (int i = 0; i < newList_L_ASC.Count(); i++)
                    {

                        myMain.listOfContacts.Add(newList_L_ASC.ElementAt<Contact>(i));
                    }
                    break;

                case "l.desc":

                    var newList_L_DESC = toSort.OrderByDescending(x => x.mulastName).ToList();

                    
                    myMain.listOfContacts.Clear();


                    for (int i = 0; i < newList_L_DESC.Count(); i++)
                    {

                        myMain.listOfContacts.Add(newList_L_DESC.ElementAt<Contact>(i));
                    }
                    
                    break;
            }
                
        
        }


        private void MenuFlyoutItem_Click_1(object sender, RoutedEventArgs e)
        {
            sort("f.asc");
        }

        private void MenuFlyoutItem_Click_2(object sender, RoutedEventArgs e)
        {
            sort("f.desc");
        }

        private void MenuFlyoutItem_Click_3(object sender, RoutedEventArgs e)
        {
            sort("l.asc");
        }

        private void MenuFlyoutItem_Click_4(object sender, RoutedEventArgs e)
        {
            sort("l.desc");

        }

        private void GridForGroups_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            this.singletapGroups = false; 
            FlyoutBase.GetAttachedFlyout(sender as FrameworkElement).ShowAt(sender as FrameworkElement);
        }



        private async void MenuFlyoutItem_Click_Group(object sender, RoutedEventArgs e)
        {

            int index = GridForGroups.SelectedIndex;
            string name = myMain.listOfGroups.ElementAt<Group>(index).myGroupName;
            string dialog = "Are you sure you want to delete " + name + "?";

            MessageDialog messageDialog = new MessageDialog(dialog, "Delete?");

            messageDialog.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(GroupCommandHandler)));
            messageDialog.Commands.Add(new UICommand("No", new UICommandInvokedHandler(GroupCommandHandler)));


            await messageDialog.ShowAsync();




        }



        public void GroupCommandHandler(IUICommand commandLabel)
        {

            var item = GridForGroups.SelectedItem;

            var Actions = commandLabel.Label;
            switch (Actions)
            {
                case "Yes":
                    myMain.removeGroup((Group)item);
                    myMain.SerialiseGroups();
                    break;
                case "No":
                    break;

            }
        }

        public void backupCommandHandler(IUICommand commandLabel)
        {
            var Actions = commandLabel.Label;
            switch (Actions)
            {
                case "Reset":
                    int index = BackupListView.SelectedIndex;
                    myMain.getBackUpitems(index);
                    break;
                    case "yes":
                    int index2 = BackupListView.SelectedIndex;
                    deleteBackup(index2);
                    break;
                case "no":
                    break;
                case "Download":
                    Download_Click();
                    break;
                case "Upload":
                    upload_Click();
                    break;
                case "Close":
                    break;
            }
        }

        public void deleteBackup(int index)
        {
            int a = BackupListView.SelectedIndex;
           
            myMain.deletingABackup(index);
        }

        private async void backupOpen()
        {
            try
            {
                string dialog = " upload your contacts or download an old set?";
                MessageDialog messageDialog = new MessageDialog(dialog, "Backup Your Contacts");

                messageDialog.Commands.Add(new UICommand("Download", new UICommandInvokedHandler(backupCommandHandler)));
                messageDialog.Commands.Add(new UICommand("Upload", new UICommandInvokedHandler(backupCommandHandler)));
               
                await messageDialog.ShowAsync();
                await Task.Delay(200);
            }
            catch (Exception)
            { ToString(); }
        }

        private  void backup_Click(object sender, RoutedEventArgs e)
        {
            backupOpen();
        }

        private void upload_Click()
        {
           uploadToCloud();
           
        }

        private async void uploadToCloud()
        {
            try
            {

                string dialog1 = "Backup Uploading...";
                MessageDialog messageDialog1 = new MessageDialog(dialog1);
                await messageDialog1.ShowAsync();
                Task t = myMain.insertItem();
                await t;
                string dialog = "Backup Successful";
                MessageDialog messageDialog = new MessageDialog(dialog);
                await messageDialog.ShowAsync();

                
            }
            catch (Exception e)
            { e.ToString(); }
        }

        private async void Download_Click()
        {
            try
            {
                if (NetworkInterface.GetIsNetworkAvailable())
                {

                    string dialog1 = "Backup Downloading...";
                    MessageDialog messageDialog1 = new MessageDialog(dialog1);
                    await messageDialog1.ShowAsync();
                    Task t = myMain.Fillbackup();
                    await t;
                    string dialog = "Backup Successful!";
                    MessageDialog messageDialog = new MessageDialog(dialog);

                    messageDialog.Commands.Add(new UICommand("Okay", new UICommandInvokedHandler(CommandHandlers)));
                    await messageDialog.ShowAsync();
                    Frame.Navigate(typeof(MainPage));
                    await Task.Delay(200);
                }
                else
                {
                    string dialog = "Network Connection is unavailable";
                    MessageDialog messageDialog = new MessageDialog(dialog, "");

                    messageDialog.Commands.Add(new UICommand("Okay", new UICommandInvokedHandler(CommandHandlers)));
                    await messageDialog.ShowAsync();
                }
            }
            catch (Exception)
            {
                string dialog = "Something has gone wrong";
                MessageDialog messageDialog = new MessageDialog(dialog, "");

                messageDialog.Commands.Add(new UICommand("Ok", new UICommandInvokedHandler(CommandHandlers)));

               messageDialog.ShowAsync();
            }
        }

        private async void backup_Tapped(object sender, TappedRoutedEventArgs e)
        { 
            this.singletapbackup = true;
            await Task.Delay(200);
            if (this.singletapbackup)
            {
                try
                {
                    string dialog = "Are you sure you want to reset your backup?";
                    MessageDialog messageDialog = new MessageDialog(dialog, "Reset Backup");

                    messageDialog.Commands.Add(new UICommand("Reset", new UICommandInvokedHandler(backupCommandHandler)));
                    messageDialog.Commands.Add(new UICommand("No", new UICommandInvokedHandler(backupCommandHandler)));
                   await messageDialog.ShowAsync();
                   
                }
                catch (Exception)
                { }
            }            
        }



        public object NetworkInformation_NetworkStatusChanged { get; set; }

        private async void MenuFlyoutItem_Click_backup(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageDialog messageDialog = new MessageDialog("do you wish to delete this backup?");

                messageDialog.Commands.Add(new UICommand("yes", new UICommandInvokedHandler(backupCommandHandler)));
                messageDialog.Commands.Add(new UICommand("no", new UICommandInvokedHandler(backupCommandHandler)));
                await messageDialog.ShowAsync();
            }
            catch (Exception)
            {
                string dialog = "Last Backup; Cannot delete";
                MessageDialog messageDialog = new MessageDialog(dialog, "");

                messageDialog.Commands.Add(new UICommand("Close", new UICommandInvokedHandler(backupCommandHandler)));
                messageDialog.ShowAsync();
            }
        }

        private void backup_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            this.singletapbackup = false;

            FlyoutBase.GetAttachedFlyout(sender as FrameworkElement).ShowAt(sender as FrameworkElement);
        }
    }
}
