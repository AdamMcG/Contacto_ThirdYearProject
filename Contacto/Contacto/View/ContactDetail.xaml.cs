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
using Windows.ApplicationModel.Appointments;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Chat;
using Windows.ApplicationModel.Email;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Contacto.View
{
    public sealed partial class ContactDetail : Page
    {
        MainPageViewModel myMain = new MainPageViewModel();
        ContactDetailViewModel defaultViewModel = new ContactDetailViewModel();
        public List<string> fields { get; set; }


        Contact myContact = new Contact();
        public ContactDetail()
        {

            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            defaultViewModel.pullFromJson();
            myContact = (Contact)e.Parameter;
            myContact.deleteDuplicates();

            fields = new List<string>();

            foreach (Contacto.Model.Contact.DynamicFields d in myContact.Dynamic)
            {

                fields.Add(d.muKey.ToString());

                {
                    if (d.muKey == "Mobile Phone" || d.muKey == "Work Phone")
                    { fieldSelector.Content = d.muKey; }

                    if (d.muKey == "Email Address")
                    {

                        fieldSelector2.Content = d.muKey;
                    }

                }
            }


            
            FieldList.ItemsSource = fields;
            FieldList2.ItemsSource = fields;

            DefaultFields.ItemsSource = fields;
            DefaultFields2.ItemsSource = fields;


            ContentArea.DataContext = myContact;


            HeaderIcon1.Style = HeaderStyleSelected;
            HeaderIcon2.Style = HeaderStyleUnselected;
            HeaderIcon3.Style = HeaderStyleUnselected;



        }

        public void exit(object sender, RoutedEventArgs e)
        {

            Frame.Navigate(typeof(MainPage));
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(updateContact), myContact);
        }

        private async void createAppointment()
        {
            var appointment = new Appointment();
            string subjectName = myContact.mufirstName + " " + myContact.mulastName;
            appointment.Subject = subjectName;
            var Invitee = new AppointmentInvitee();
            Invitee.DisplayName = subjectName;
            Invitee.Address = myContact.muprimary_email_address;
            Invitee.Role = AppointmentParticipantRole.OptionalAttendee;
            Invitee.Response = AppointmentParticipantResponse.Accepted;
            appointment.Invitees.Add(Invitee);


            string appointmentId = await AppointmentManager.ShowAddAppointmentAsync(appointment, new Rect(), Windows.UI.Popups.Placement.Default);
        }

        private async void createSms()
        {
            var chatmesg = new ChatMessage();

            chatmesg.Body = smsBody.Text;
            

            foreach (Contacto.Model.Contact.DynamicFields d in myContact.Dynamic)
            {
                if (d.muKey == fieldSelector.Content.ToString() )
                { chatmesg.Recipients.Add(d.muValue); }
            }

            await Windows.ApplicationModel.Chat.ChatMessageManager.ShowComposeSmsMessageAsync(chatmesg);

        }


        public void makeACall(object sender, RoutedEventArgs e)
        {
            Windows.ApplicationModel.Calls.PhoneCallManager.ShowPhoneCallUI(myContact.muprimary_contact_num, myContact.mufirstName);
        }

        private void ListPickerFlyout_ItemsPicked(ListPickerFlyout sender, ItemsPickedEventArgs args)
        {
            string selection = (string)sender.SelectedItem;

            fieldSelector.Content = selection;

        }

        private void ListPickerFlyout_ItemsPicked2(ListPickerFlyout sender, ItemsPickedEventArgs args)
        {

            string selection = (string)sender.SelectedItem;
            fieldSelector2.Content = selection;

        }

        private void ListPickerFlyout_ItemsPicked3(ListPickerFlyout sender, ItemsPickedEventArgs args)
        {

            string selection = (string)sender.SelectedItem;
            PhoneButton.Content = selection;

        }
        private void ListPickerFlyout_ItemsPicked4(ListPickerFlyout sender, ItemsPickedEventArgs args)
        {

            string selection = (string)sender.SelectedItem;
            EmailButton.Content = selection;

        }


        private async void createEmail()
        {
            var email = new EmailMessage();
            string name = myContact.mufirstName + " " + myContact.mulastName;
            email.Subject = emailSubject.Text.ToString();
            email.Body = emailBody.Text.ToString();
            

            foreach (Contacto.Model.Contact.DynamicFields d in myContact.Dynamic)
            {
                if (d.muKey == fieldSelector2.Content.ToString())
                {
                    email.To.Add(new EmailRecipient(d.muValue, name));
                }
            }
            await Windows.ApplicationModel.Email.EmailManager.ShowComposeNewEmailAsync(email);
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsBox.Visibility = Visibility.Visible;
        }



        private void YesButton_Click(object sender, RoutedEventArgs e)
        {


            for (int i = 0; i < myContact.muCustomFields.Count; i++)
            {
                if (EmailButton.Content.ToString() == myContact.muCustomFields.Keys.ElementAt<string>(i))
                {
                    myContact.muprimary_email_address = myContact.muCustomFields.Values.ElementAt<string>(i);

                }
                
                if (PhoneButton.ToString() == myContact.muCustomFields.Keys.ElementAt<string>(i))
                {
                    myContact.muprimary_contact_num = myContact.muCustomFields.Values.ElementAt<string>(i);

                }
            }

                SettingsBox.Visibility = Visibility.Collapsed;

        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            

            SettingsBox.Visibility = Visibility.Collapsed;

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


        private void HeaderImg3_Tapped(object sender, TappedRoutedEventArgs e)
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
