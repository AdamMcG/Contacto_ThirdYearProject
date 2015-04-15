using Contacto.Model;
using Contacto.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Appointments;
using Windows.ApplicationModel.Chat;
using Windows.ApplicationModel.Email;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Contacto.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GroupDetail : Page
    {
        public Group myGroup { get; set; }

        GroupViewModel groupVM = new GroupViewModel();


        public GroupDetail()
        {

            this.InitializeComponent();

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            myGroup = (Group)e.Parameter;


            groupVM.fillcontactList();

            this.DataContext = myGroup;
            listofContacts.ItemsSource = myGroup.contactList;

            

        }

                public void exit(object sender, RoutedEventArgs e)
        {

            Frame.Navigate(typeof(MainPage));
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UpdateGroup), myGroup);
        }

        private async void createAppointment()
        {
            var appointment = new Appointment();
            appointment.Subject = myGroup.myGroupName;
            string appointmentId = await AppointmentManager.ShowAddAppointmentAsync(appointment, new Rect(), Windows.UI.Popups.Placement.Default);
        }

        private async void createSms()
        {
            var chatmesg = new ChatMessage();

            chatmesg.Body = smsBody.Text;

            await Windows.ApplicationModel.Chat.ChatMessageManager.ShowComposeSmsMessageAsync(chatmesg);

        }

        
        private void ListPickerFlyout_ItemsPicked(ListPickerFlyout sender, ItemsPickedEventArgs args)
        {
            string selection = (string)sender.SelectedItem;

            fieldSelector.Content = selection;

        }

        private async void createEmail()
        {
            var email = new EmailMessage();
            
            await Windows.ApplicationModel.Email.EmailManager.ShowComposeNewEmailAsync(email);
        }

        private void SMSClick(object sender, RoutedEventArgs e)
        {
            createSms();
        }

        private void EmailClick(object sender, RoutedEventArgs e)
        {
            createEmail();
        }

        private void Calendar_Click(object sender, RoutedEventArgs e)
        {
            createAppointment();
        }


        private void HeaderImg1_Tapped(object sender, TappedRoutedEventArgs e)
        {

            ContentArea.SelectedIndex = 0;
        }

        private void HeaderImg2_Tapped(object sender, TappedRoutedEventArgs e)
        {

            ContentArea.SelectedIndex = 1;
        }

        private void HeaderImg3_Tapped(object sender, TappedEventHandler e)
        {
            ContentArea.SelectedIndex = 2;

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
            }
            else if (ContentArea.SelectedIndex == 2)
            {
                HeaderIcon1.Style = HeaderStyleUnselected;
                HeaderIcon2.Style = HeaderStyleUnselected;
                HeaderIcon3.Style = HeaderStyleSelected;
            }
            else
            {
                HeaderIcon1.Style = HeaderStyleSelected;
                HeaderIcon2.Style = HeaderStyleUnselected;
                HeaderIcon3.Style = HeaderStyleUnselected;

            }

        }

        private void StackPanel_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {

            FlyoutBase.GetAttachedFlyout(sender as FrameworkElement).ShowAt(sender as FrameworkElement);
        
        }



        private async void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {

           Contact temp = (Contact)listofContacts.SelectedItem;

            string dialog = "Are you sure you want to delete " + temp.mufirstName + " " + temp.mulastName + " From this list?";

            MessageDialog messageDialog = new MessageDialog(dialog, "Delete?");

            messageDialog.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(CommandHandlers)));
            messageDialog.Commands.Add(new UICommand("No", new UICommandInvokedHandler(CommandHandlers)));


            await messageDialog.ShowAsync();




        }


        public async void CommandHandlers(IUICommand commandLabel)
        {

            var Actions = commandLabel.Label;
            switch (Actions)
            {
                case "Yes":

                    groupVM.removeGroup(myGroup);
                    Contact temp = (Contact)listofContacts.SelectedItem;
                    myGroup.contactList.Remove(temp);
                    groupVM.addGroup(myGroup);
                    groupVM.serailizeGroups();
                    break;
                case "No":
                    break;

            }
        }

    }   
}
