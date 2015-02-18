using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacto.ViewModel
{
    //This class is used to hold the business logic of the "add a contact" feature. 
    //The add, update and delete methods should be in this class. 
    class ContactViewModel: INotifyPropertyChanged
    {
        public ContactViewModel(){ 
        
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
