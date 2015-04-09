using System;
using Contacto;
using System.Collections.Generic;
using Windows.Storage;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.IO;
using Contacto.Data;
using System.ComponentModel;

namespace Contacto.Model
{
    //This class is used to create a group made up of select contacts from the contact list. 
    //contacts should be able to be a member of many groups. 
    public class Group: INotifyPropertyChanged
    {
        public Group(){
            muGroup = 2;
            myGroupName = "Sheniqua";
        }

        private int uniqueGroupID;
        public int muGroup{ 
        get {return uniqueGroupID;}
        set { uniqueGroupID = value; }
        }
        private string groupName;
        public string myGroupName{
        get { return groupName; }
            set
            {
                if (value != groupName)
                {
                    groupName= value;
                    NotifyPropertyChanged("groupName");
                }
            }
        }

        private ObservableCollection<Contact> myContactList = new ObservableCollection<Contact>();
        public ObservableCollection<Contact> contactList{
            get { return myContactList; }
            set  { myContactList = value;  }
        }
    

    public event PropertyChangedEventHandler PropertyChanged;
            private void NotifyPropertyChanged(String propertyName)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (null != handler)
                    handler(this, new PropertyChangedEventArgs(propertyName));
            }

        }
}
