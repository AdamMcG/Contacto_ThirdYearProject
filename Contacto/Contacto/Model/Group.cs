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

namespace Contacto.Model
{
    //This class is used to create a group made up of select contacts from the contact list. 
    //contacts should be able to be a member of many groups. 
    public class Group
    {
        public Group(){
            muGroup = 1;
            myGroupName = "Sheniqua";
            Contact test = new Contact();
            contlist.Add(test);
        }

        private int uniqueGroupID;
        public int muGroup{ 
        get {return uniqueGroupID;}
        set { uniqueGroupID = value; }
        }
        private string groupName;
        public string myGroupName{
        get { return groupName; }
        set { groupName = value; }
        }

        private ObservableCollection<Contact> myContactList = new ObservableCollection<Contact>();
        public ObservableCollection<Contact> contlist
        { get { return myContactList; }}
    }
}
