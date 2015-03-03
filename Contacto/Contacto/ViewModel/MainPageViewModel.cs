using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contacto.Data;
using System.Collections.ObjectModel;
using Contacto.Model;
using System.Collections;
using Windows.Storage;
using Windows.Data.Json;
namespace Contacto.ViewModel
{
    //This handles the main page business logic.
    class MainPageViewModel:INotifyPropertyChanged
    {
        private string data = "testing";
        public string dating
        {
            get { return data; }
            set
            {
                {
                    if (value != data)
                    {
                        data = value;
                        NotifyPropertyChanged("data");
                    }
                }
            }
        }
       private static MainPageViewModel contData = new MainPageViewModel();
        
        private ObservableCollection<Contact> contactlist = new ObservableCollection<Contact>();
        public ObservableCollection<Contact> listOfContacts
        {
            get { return contactlist; }
        }

        public void addtolist(Contact MyContact)
        {
            Contact testContact = new Contact("1", "Testing 123 testing","Ad","1232");
            contactlist.Add(testContact);
            contactlist.Add(MyContact);
        }
            public MainPageViewModel() {
                Contact newCont = new Contact("2", "Testing 456 testing","Ad","5655");
                addtolist(newCont);
                
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handle = PropertyChanged;
            if (null != handle)
            { handle(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}
