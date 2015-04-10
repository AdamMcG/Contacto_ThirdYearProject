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
        public List<Contact> groupContacts  { get; set; }

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

            groupContacts = myGroup.contactList.ToList<Contact>();

            this.DataContext = myGroup;
            listofContacts.ItemsSource = myGroup.contactList;

            

        }

                public void exit(object sender, RoutedEventArgs e)
        {

            Frame.Navigate(typeof(MainPage));
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
           
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
    }   
}
