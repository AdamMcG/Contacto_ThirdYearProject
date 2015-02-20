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


        public AddContactPage()
        {


            this.InitializeComponent();

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
                List.Items.Add(stackPan[i]);
                fieldCounter++;
                indexLocation++;

            }

        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var nav = (NavigationContext)e.Parameter;
            var item = c;
            this.DataContext = item;
        }



        private void AddFieldBtn_Click(object sender, RoutedEventArgs e)
        {


            TextBox textBoxes = new TextBox();
            textBoxes.Width = 180;
            textBoxes.VerticalAlignment = VerticalAlignment.Center;
            textBoxes.HorizontalAlignment = HorizontalAlignment.Right;


            TextBox textBlocks = new TextBox();
            textBoxes.PlaceholderText = "Details";
            textBlocks.PlaceholderText = "Field Name";
            textBlocks.FontSize = 20;
            textBlocks.Width = 160;
            textBlocks.VerticalAlignment = VerticalAlignment.Center;
            textBlocks.HorizontalAlignment = HorizontalAlignment.Left;

            TextBlock spacer = new TextBlock();
            spacer.Width = 20;
            spacer.Opacity = 0;

            StackPanel stackPan = new StackPanel();
            stackPan.Height = 80;
            stackPan.Width = 370;
            stackPan.Orientation = Orientation.Horizontal;
            fieldCounter++;
            indexLocation++;


            

                // Here you can modify the value of the textbox which is at textBoxes[i]

                stackPan.Children.Add(textBlocks);
           stackPan.Children.Add(spacer);     
                stackPan.Children.Add(textBoxes);
              

              // This adds the controls to the form (you will need to specify thier co-ordinates etc. first)

              List.Items.Add(stackPan);;  
            }

        private void RemoveFieldBtn_Click(object sender, RoutedEventArgs e)
        {
            if (fieldCounter > 3)
            {
                List.Items.RemoveAt(indexLocation);
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
            int numElements = List.Items.Count();
            StackPanel[] elements = new StackPanel[numElements];
            for (int i = 0; i < numElements; i++){
                List.Items.CopyTo(elements, i);
            }
            Contact newContact = new Contact();
            //newContact.add(some dictionary value to its dictionary)
            c.addContactToGroups(newContact);
        }
 
    }
}