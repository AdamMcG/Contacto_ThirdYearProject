using System;
using System.Collections.Generic;
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Contacto.ViewModel;
using Contacto.Model;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Contacto.Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Contacto
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddContactPage : Page
    {
        ContactViewModel c = new ContactViewModel();
        private static int fieldCounter = 0;
        private static int indexLocation = -1;
        ListView addList = new ListView();
        AddContactViewModel AddViewModel = new AddContactViewModel();


        public AddContactPage()
        {


            this.InitializeComponent();


            addList.HorizontalAlignment = HorizontalAlignment.Stretch;
            addList.Height = 500;
            addList.VerticalAlignment = VerticalAlignment.Top;
            toAddGrid.Children.Add(addList);




            addList.Items.Add(AddViewModel.initalizePage("First Name"));
            addList.Items.Add(AddViewModel.initalizePage("Last Name"));
            addList.Items.Add(AddViewModel.initalizePage("Phone Number"));




            fieldCounter = fieldCounter + 3;
            indexLocation = indexLocation + 3;


        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        //    var nav = (NavigationContext)e.Parameter;
            var item = c;
            this.DataContext = item;
        }



        private void AddFieldBtn_Click(object sender, RoutedEventArgs e)
        {


            fieldCounter++;
            indexLocation++;

            StackPanel stackPan = AddViewModel.createList(); 

           addList.Items.Add(stackPan);
            }

        private void RemoveFieldBtn_Click(object sender, RoutedEventArgs e)
        {
            if (fieldCounter > 3)
            {
                addList.Items.RemoveAt(indexLocation);
                fieldCounter--;
                indexLocation--;
            }
            else
            {
                return;
            }
        }
 
        private void FinishBtn_Click(object sender, RoutedEventArgs e)
        {

            string id = Guid.NewGuid().ToString();
            string firstName = AddViewModel.getDetailsData(0);
            string lastName = AddViewModel.getDetailsData(1);
            string phoneNumber = AddViewModel.getDetailsData(2);



            Contact toAdd = new Contact(id, firstName, lastName, phoneNumber);


            //for (int i = 3; i <= indexLocation; i++)
            //{
            //    string field = AddViewModel.getFieldData(i);
            //    string details = AddViewModel.getDetailsData(i);

            //    toAdd.addNewField(field, details);


            //}

            MainPageViewModel mainPage = new MainPageViewModel();
            JSON_work jwork = new JSON_work();
            mainPage.addtolist(toAdd);
            jwork.SerialisingListWithJsonNetAsync(mainPage.listOfContacts);

            Frame.Navigate(typeof(MainPage));
        }
 
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
 
    }
}