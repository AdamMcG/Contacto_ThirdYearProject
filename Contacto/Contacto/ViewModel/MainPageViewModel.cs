﻿using System;
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
       private static MainPageViewModel contData = new MainPageViewModel();
        
        private ObservableCollection<Contact> contactlist = new ObservableCollection<Contact>();
        public ObservableCollection<Contact> listOfContacts{
            get { return contactlist; }
        }

        public void addtolist(Contact MyContact)
        {
            Contact testContact = new Contact("1", "Carol","Ad","1232");
            contactlist.Add(testContact);
            contactlist.Add(MyContact);
        }
            public MainPageViewModel() {
                Contact newCont = new Contact("2", "Joesph","Ad","5655");
                addtolist(newCont);
                pullFromJSON();
                                
        }

            public void pullFromJSON()
            {
                JsonValue j = JsonValue.Parse("{\"uniqueID\": \"800\", \"firstName\": \"Adam\", \"lastName\": \"View from 15th Floor\", \"phoneNumber\": \"12322\" }");
                string a = j.GetObject().GetNamedString("uniqueID");
                string b = j.GetObject().GetNamedString("firstName");
                string c = j.GetObject().GetNamedString("lastName");
                string d = j.GetObject().GetNamedString("phoneNumber");
                Contact newCont = new Contact(a, b, c, d);
                contactlist.Add(newCont);
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
