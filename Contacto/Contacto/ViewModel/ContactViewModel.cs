using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Contacto.Model;
using System.Threading.Tasks;

namespace Contacto.ViewModel
{
    //This class is used to hold the business logic of the "contact" feature. 
    //The add, update and delete methods should be in this class. 
   public class ContactViewModel: INotifyPropertyChanged
    {
        private ObservableCollection<Contact> myContacts = new ObservableCollection<Contact>();
        private string testme= "This is working!";
        public string testing
        {
            get { return testme; }
            set {
                if (value != testme)
                {
                    testme = value;
                    NotifyPropertyChanged("testme");
                }
            }
        }
        public ContactViewModel(Contact aContact){
           
            myContacts.Add(aContact);
        }

        public ContactViewModel()
        {

        }

        public void addContact(){
        
        }

        public void updateContact(){ 
        
        }
        public void deleteContact()
        { 
        
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
