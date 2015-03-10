using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Contacto.Model;
using System.Threading.Tasks;
using Contacto.Data;

namespace Contacto.ViewModel
{
    //This class is used to hold the business logic of the "contact" feature. 
    //The add, update and delete methods should be in this class. 
    class ContactViewModel: INotifyPropertyChanged
    {
            ContactDataSource a = new ContactDataSource();
        private string test = "this is databinding working";
        public string testing
        {
            get { return test; }
            set { 
        if(value != test)
        {
            test = value;
            NotifyPropertyChanged("testMe");
        }
        }}
       
        public ContactViewModel()
        {}

        public void addContactToGroups(Contact myContact)
        {
            myContacts.Add(myContact);
            
            
        }

        public ObservableCollection<Contact> myContacts = new ObservableCollection<Contact>();

        public ContactViewModel(Contact aContact){
            myContacts.Add(aContact);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handle = PropertyChanged;
            if (null != handle)
            {
                handle(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
