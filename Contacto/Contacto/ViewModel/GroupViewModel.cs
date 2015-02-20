using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contacto.Model;
namespace Contacto.ViewModel
{
   //this class is used to handle the business logic of the Group function
    //Add group, update group and delete group should be implimented in this class. 
    class GroupViewModel
    {
         ObservableCollection<Group> myGroup =new ObservableCollection<Group>();
        GroupViewModel(Group aGroup){
            myGroup.Add(aGroup);
        }
        public void addGroup(){
        
        }

        public void deleteGroup(int groupID){
        
        }

        public void updateGroup(int groupID){ 
        
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
