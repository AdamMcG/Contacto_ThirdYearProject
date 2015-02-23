using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacto.ViewModel
{
    //This handles the main page business logic. It should 
    class MainPageViewModel:INotifyPropertyChanged
    {
        public MainPageViewModel() {
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
