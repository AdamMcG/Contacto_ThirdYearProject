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
using Contacto.View;
using Contacto.ViewModel;
using Contacto.Data;
using Contacto.Model;
using Contacto.Common;
using Windows.ApplicationModel.Resources;
using System.Threading.Tasks;
using Windows.UI.Popups;
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
            ContentArea.SelectedIndex = 0;
            myMain.initalizeList();
            myMain.InitalizeGroups();
            this.DataContext = myMain;

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

        //private void HeaderImg4_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    ContentArea.SelectedIndex = 3;


        //}

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


            resultPicker.ItemsSource = searchResults.Values;


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

            searchResults.Clear();
            // YesButton Clicked! Let's hide our InputBox and handle the input text.
            SearchBox.Visibility = Visibility.Collapsed;

            // Do something with the Input
            String input = InputTextBox.Text;
            searchForContact(InputTextBox.Text, "firstName");


            // Clear InputBox.
            InputTextBox.Text = String.Empty;
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            // NoButton Clicked! Let's hide our InputBox.
            SearchBox.Visibility = Visibility.Collapsed;

            // Clear InputBox.
            InputTextBox.Text = String.Empty;
        }

        private void Group_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(GroupDetail),myMain.listOfGroups.ElementAt(GridForGroups.SelectedIndex));
        }




 
    }
}
