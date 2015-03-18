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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Contacto
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddContactPage : Page
    {

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
            AddViewModel.pullFromJson();
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

            

            for (int i = 0; i <= indexLocation; i++)
            {


                string s = AddViewModel.getFieldData(i);
                string t = AddViewModel.getDetailsData(i);

                TextBlock test = new TextBlock();
                test.Text = s;
                test.FontSize = 18;
                test.Width = 100;
                test.Height = 100;

                TextBlock test2 = new TextBlock();
                test2.Text = t;
                test2.FontSize = 18;
                test2.Width = 100;
                test2.Height = 100;

                addList.Items.Add(test);
                addList.Items.Add(test2);
            }

            Contact newContact = new Contact();
          //  newContact.add();
            AddViewModel.listOfContacts.Add(newContact);
            AddViewModel.createNewContactList();
            Frame.Navigate(typeof(MainPage));
        }
 
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
 
    }
}