using Contacto.Model;
using Contacto.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.DataContext = defaultview;
            Task t =  defaultview.fillcontactList();
            await t;
            t = defaultview.initaliseGroup();
            await t;
        }


        public void addGroupToList(int[] index)
        { }

        private void exit(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
        
        private async void AddGroup(object sender, RoutedEventArgs e)
        {

            defaultview.groupName = myTextBox.Text;
            var selected = ToAddToGroups.SelectedItems;


            for (int i = 0; i < selected.Count; i++)
            {

                var temp = selected.ElementAt(i);
                defaultview.localContacts.Add((Contact)temp);
            }
            defaultview.addGroup();
            Task t = defaultview.serailizeGroups();
            await t;
               
            
            Frame.Navigate(typeof(MainPage));
        }
    }
}
