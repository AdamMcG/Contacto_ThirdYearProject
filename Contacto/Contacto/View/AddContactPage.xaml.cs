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
        ContactViewModel c = new ContactViewModel();
        private static int fieldCounter = 0;
        private static int indexLocation = -1;
        ListView addList = new ListView();

        public AddContactPage()
        {


            this.InitializeComponent();


            addList.HorizontalAlignment = HorizontalAlignment.Stretch;
            addList.Height = 500;
            addList.VerticalAlignment = VerticalAlignment.Top;
            toAddGrid.Children.Add(addList);


                 //<ListView x:Name="List"
                 // HorizontalAlignment="Stretch"
                 // Height="503"
                 // Margin="10,10.333,10,0"
                 // Grid.Row="1"
                 // VerticalAlignment="Top"
                 // />


            int n = 3;
            StackPanel[] stackPan = new StackPanel[n];
            TextBlock[] textBlocks = new TextBlock[n];
            TextBox[] textBoxes = new TextBox[n];

            for (int i = 0; i < n; i++)
            {
                stackPan[i] = new StackPanel();
                stackPan[i].Height = 80;
                stackPan[i].Width = 370;
                stackPan[i].Orientation = Orientation.Horizontal;

                textBoxes[i] = new TextBox();
                textBoxes[i].Width = 180;
                textBoxes[i].VerticalAlignment = VerticalAlignment.Center;
                textBoxes[i].HorizontalAlignment = HorizontalAlignment.Right;


                textBlocks[i] = new TextBlock();
                textBlocks[i].Width = 180;
                textBlocks[i].FontSize = 20;
                textBlocks[i].VerticalAlignment = VerticalAlignment.Center;
                textBlocks[i].HorizontalAlignment = HorizontalAlignment.Right;

            }


            textBlocks[0].Text = "First Name:\t";

            textBlocks[1].Text = "Last Name:\t";

            textBlocks[2].Text = "Phone Number:\t";

            for (int i = 0; i < n; i++)
            {
                stackPan[i].Children.Add(textBlocks[i]);
                stackPan[i].Children.Add(textBoxes[i]);
                
            }

            for (int i = 0; i < n; i++)
            {
                addList.Items.Add(stackPan[i]);
                fieldCounter++;
                indexLocation++;

            }

        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        //    var nav = (NavigationContext)e.Parameter;
            var item = c;
            this.DataContext = item;
        }



        private void AddFieldBtn_Click(object sender, RoutedEventArgs e)
        {


            TextBox fieldEntry = new TextBox();
            fieldEntry.PlaceholderText = "Field Name";
            fieldEntry.FontSize = 20;
            fieldEntry.Width = 160;
            fieldEntry.VerticalAlignment = VerticalAlignment.Center;
            fieldEntry.HorizontalAlignment = HorizontalAlignment.Left;



            TextBox detailsEntry = new TextBox();
            detailsEntry.Width = 180;
            detailsEntry.PlaceholderText = "Details";
            detailsEntry.VerticalAlignment = VerticalAlignment.Center;
            detailsEntry.HorizontalAlignment = HorizontalAlignment.Right;


            TextBlock spacer = new TextBlock();
            spacer.Width = 20;
            spacer.Opacity = 0;

            StackPanel stackPan = new StackPanel();
            stackPan.Height = 80;
            stackPan.Width = 370;
            stackPan.Orientation = Orientation.Horizontal;
            fieldCounter++;
            indexLocation++;




            stackPan.Children.Add(fieldEntry);
            stackPan.Children.Add(spacer); 
            stackPan.Children.Add(detailsEntry);
              

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



            Contact newContact = new Contact();
          //  newContact.add();
            c.addContactToGroups(newContact);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
 
    }
}