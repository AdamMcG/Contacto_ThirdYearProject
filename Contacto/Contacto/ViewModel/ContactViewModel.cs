using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacto.ViewModel
{
    class ContactViewModel: INotifyPropertyChanged
    {
      Dictionary<string,string> Contact = new Dictionary<string, string>();
      public void testDic()
      {
          Contact.Add("First name", "Adam");

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
