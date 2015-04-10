using Contacto.Model;
using Contacto.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Contacto.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddGroupPage : Page
    {
        GroupViewModel defaultview = new GroupViewModel();
        public AddGroupPage()
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
            this.DataContext = defaultview;
            defaultview.fillcontactList();
            defaultview.initaliseGroup();
        }


        public void addGroupToList(int[] index)
        { }

        private void exit(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
        
        private void AddGroup(object sender, RoutedEventArgs e)
        {

            defaultview.groupName = myTextBox.Text;
            var selected = ToAddToGroups.SelectedItems;


            for (int i = 0; i < selected.Count; i++)
            {

                var temp = selected.ElementAt(i);
                defaultview.localContacts.Add((Contact)temp);
            }
            defaultview.addGroup();
            defaultview.serailizeGroups();

                // defaultview.groupName = myTextBox.Text;
                // List<int> testing = new List<int>();
                //int a = ToAddToGroups.SelectedIndex;
                //testing.Add(a);
                // for (int i = 0; i < testing.Count; i++)
                // {
                //     defaultview.localContacts.Add(defaultview.globalContacts.ElementAt(testing.ElementAt(i)));
                // }
                //defaultview.addGroup();
           Frame.Navigate(typeof(MainPage));
        }
    }
}
