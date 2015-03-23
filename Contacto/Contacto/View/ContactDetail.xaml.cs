using Contacto.Model;
using Contacto.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Contacto.View
{
    public sealed partial class ContactDetail : Page
    {

        MainPageViewModel myMain;

        ObservableCollection<myStruct> dynamic_fields = new ObservableCollection<myStruct>();

        struct myStruct
        {
           private string key;
           public string keyy
           {
               get { return key; }
               set { key = value; }
           }
         private string valuee;
         public string valueee
         {
             get { return valuee; }
             set { valuee = value; }
         }


         public string toString()
         { return "123323"; }
            

        }


        Contact myContact; 
        public ContactDetail()
        {
                this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            myContact = (Contact)e.Parameter;


            
            for(int i = 0; i < myContact.muCustomFields.Count(); i++){

                myStruct toAdd = new myStruct();
                toAdd.keyy=(myContact.muCustomFields.Keys.ElementAt<String>(i));
                toAdd.valueee=(myContact.muCustomFields.Values.ElementAt<String>(i));
                dynamic_fields.Add(toAdd);

            }

           

        }
    }
}
