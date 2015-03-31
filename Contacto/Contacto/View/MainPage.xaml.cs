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
            Frame.Navigate(typeof(View.AddGroupPage));

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

        
            


       

 
    }
}
