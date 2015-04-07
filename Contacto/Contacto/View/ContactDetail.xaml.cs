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

        Contact myContact = new Contact();
        public ContactDetail()
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
            defaultViewModel.pullFromJson();
            myContact = (Contact)e.Parameter;
             myContact.deleteDuplicates();
            this.DataContext = myContact;


        }

        public void exit(object sender, RoutedEventArgs e)
        {

            //defaultViewModel.removeFromList(myContact);
            //defaultViewModel.addtocontactlist(myContact);
            //defaultViewModel.createNewContactList();



            Frame.Navigate(typeof(MainPage));
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(updateContact), myContact);
        }

        private async void createAppointment()
        {
            var appointment = new Appointment();
            string subjectName = myContact.mufirstName +" "+ myContact.mulastName;
            appointment.Subject = subjectName;

            string appointmentId = await AppointmentManager.ShowAddAppointmentAsync(appointment, new Rect(), Windows.UI.Popups.Placement.Default);
        }

        private async void createSms()
        {
            var chatmesg = new ChatMessage();

            foreach (Contacto.Model.Contact.DynamicFields d in myContact.Dynamic)
            {
                if (d.muKey == "Mobile Phone" || d.muKey == "Work Phone")
                { chatmesg.Recipients.Add(d.muValue); }
            }
            await Windows.ApplicationModel.Chat.ChatMessageManager.ShowComposeSmsMessageAsync(chatmesg);
        }

        private async void createEmail()
        {
            var email = new EmailMessage();
            string name = myContact.mufirstName + " "+  myContact.mulastName;
               foreach (Contacto.Model.Contact.DynamicFields d in myContact.Dynamic){
                   if(d.muKey == "Email Address"){
                       email.To.Add(new EmailRecipient(d.muValue, name));
                   }
               }
          await  Windows.ApplicationModel.Email.EmailManager.ShowComposeNewEmailAsync(email);
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
    }
}
