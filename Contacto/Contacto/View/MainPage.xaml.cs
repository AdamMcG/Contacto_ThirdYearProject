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
using Contacto.ViewModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace Contacto
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
         MainPageViewModel myMain = new MainPageViewModel();
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
            this.DataContext = myMain;
        }

        

        private void HeaderImg1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
            ContentArea.SelectedIndex = 0;
        }

        private void HeaderImg2_Tapped(object sender, TappedRoutedEventArgs e)
        {

            ContentArea.SelectedIndex = 1;


        }

        private void HeaderImg3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ContentArea.SelectedIndex = 2;


        }

        private void HeaderImg4_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ContentArea.SelectedIndex = 3;


        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
          //  Frame.Navigate(typeof(AddContactPage));

            NavigationContext nav = new NavigationContext()
            {

                ID = 1,
                Name = "Test",
                Description = "This is a test"

            };

            Frame.Navigate(typeof(AddContactPage), nav);
   

        }


  

       

 
    }
}
