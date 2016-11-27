using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System;
using AutismCommunicationApp.DataModel;
using System.Collections.Generic;
using ViewModel;
using System.Linq;
using Windows.ApplicationModel.DataTransfer;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media.Imaging;
using System.Diagnostics;

namespace AutismCommunicationApp
{
    public sealed partial class MainPage : Page
    {

        // Private list used to bind to MainPage view
        private ObservableCollection<Picture> Pictures;
        private ObservableCollection<Picture> communicationBar;
        private bool editEnabled;

        public object SupportedOrientations { get; private set; }

        public MainPage()
        {

            this.InitializeComponent();

            // Save data to this list
            this.Pictures = PictureManager.loadData();

            this.communicationBar = new ObservableCollection<Picture>();

            editEnabled = false;

        }// End Constructor

        // Code for humburger button click
        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {

            // Set pane to open if it closed
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;

        }// End HamburgerButton_Click


        // ====================  FILE EXPLORER BUTTON  ====================

        // This button opens file explorer, copies the selected file and navigates to the ImageDetails page
        private async void MenuButton1_Click(object sender, RoutedEventArgs e)
        {

            // adapted from http://stackoverflow.com/questions/39111925/uwp-copy-file-from-fileopenpicker-to-localstorage

            // Open file explorer
            var pickerOpen = new FileOpenPicker();

            // Have a filter for png files
            pickerOpen.FileTypeFilter.Add(".png");
            pickerOpen.FileTypeFilter.Add(".jpg");
            pickerOpen.FileTypeFilter.Add(".jpeg");

            // pick file and store it in a storage file
            StorageFile storageFile = await pickerOpen.PickSingleFileAsync();

            // Check if file is selected
            if (storageFile != null)
            {
                // Copy the file into the devices local storage
                // If file alredy exists replace the copy
                await storageFile.CopyAsync(ApplicationData.Current.LocalFolder, storageFile.Name, NameCollisionOption.ReplaceExisting);
            }

            // If a file has been selected
            if (storageFile != null)
            {

                // Navigate to page where image is displayed and label can be set
                // Pass the file name along
                this.Frame.Navigate(typeof(ImageDetails), storageFile.Name);

            }// End if
           
        }// End MenuButton1_Click

        //  ====================  DRAG AND DROP FROM DISPLAY TO COMMUNICATION BAR  ====================

        /*
         *  Adapted from http://www.shenchauhan.com/blog/2015/8/23/drag-and-drop-in-uwp
        */

        // Code for gridview displaying pictures
        private void DisplayPictures_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
           
            // Get ID of card and set as a string
            var card = string.Concat(e.Items.Cast<Picture>().Select(i => i.pictureId));
            e.Data.SetText(card);
            e.Data.RequestedOperation = DataPackageOperation.Move;

        }

        // Code for communication bar
        private void CommunicationBar_DragOver(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.Text))
            {
                e.AcceptedOperation = DataPackageOperation.Move;
            }
        }

        // Moves the item being dragged into an observable collection and moves it into a different 
        // gridview on the screen
        private async void CommunicationBar_Drop(object sender, DragEventArgs e)
        {
            // If a string is being passed over 
            if (e.DataView.Contains(StandardDataFormats.Text))
            {

                // Retrieve the pictures id and store in string
                var id = await e.DataView.GetTextAsync();

                var itemIdsToMove = id.Split(',');

                var destinationListView = sender as GridView;
                var listViewItemsSource = destinationListView.ItemsSource as ObservableCollection<Picture>;

                if (listViewItemsSource != null)
                {

                    // Loop through list containing picture
                    foreach (var itemId in itemIdsToMove)
                    {

                        // Catch exception that won't let you drop an image in the same list it is already in
                        try
                        {

                            // Find picture in list 
                            var itemToMove = this.Pictures.First(i => i.pictureId.ToString() == itemId);

                            // If communication bar has no more than 2 pictures in it
                            if (listViewItemsSource.Count() < 2)
                            {

                                // Move picture to communication bar
                                listViewItemsSource.Add(itemToMove);

                                // Remove picture from display 
                                this.Pictures.Remove(itemToMove);

                            }// End if
                        }
                        catch (InvalidOperationException){ }
                        
                    }// End foreach
                    
                }// End if

            }// End if

        }// End CommunicationBar_Drop

        //  ====================  DRAG AND DROP FROM COMMUNICATION BAR TO DISPLAY  ====================

        private void CommunicationBar_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {

            // Get ID of card and set as a string
            var card = string.Concat(e.Items.Cast<Picture>().Select(i => i.pictureId));
            e.Data.SetText(card);
            e.Data.RequestedOperation = DataPackageOperation.Move;

        }

        private void DisplayPictures_DragOver(object sender, DragEventArgs e)
        {

            if (e.DataView.Contains(StandardDataFormats.Text))
            {
                e.AcceptedOperation = DataPackageOperation.Move;
            }

        }

        private async void DisplayPictures_Drop(object sender, DragEventArgs e) 
        {
            // If a string is being passed over 
            if (e.DataView.Contains(StandardDataFormats.Text))
            {

                // Retrieve the pictures id and store in string
                var id = await e.DataView.GetTextAsync();

                // Created an array with picture ID
                var itemIdsToMove = id.Split(',');

                var destinationListView = sender as GridView;
                var listViewItemsSource = destinationListView.ItemsSource as ObservableCollection<Picture>;

                if (listViewItemsSource != null)
                {

                    // Loop through list containing picture
                    foreach (var itemId in itemIdsToMove) 
                    {

                        // Catch exception that won't let you drop an image in the same list it is already in
                        try
                        {

                            // Find picture in communication bar list 
                            var itemToMove = this.communicationBar.First(i => i.pictureId.ToString() == itemId);

                            // Move picture to communication bar
                            listViewItemsSource.Add(itemToMove);

                            // Remove picture from communication bar 
                            this.communicationBar.Remove(itemToMove);

                        }catch (InvalidOperationException) { }

                    }// End foreach

                }// End nested if

            }// End if

        }// End DisplayPictures_Drop

        // ====================  EDIT MODE FUNCTIONALITY  ====================

        // Button will enable edit mode
        private void Edit_Click(object sender, RoutedEventArgs e)
        {

            // Switch the value of editEnabled to the opposite of what is currently is 
            editEnabled = (editEnabled == true) ? false : true;

            // Change the content of the edit textblock to show if it's enabled or disabled
            EditTextBlock.Text = (editEnabled == true) ? "Edit Enabled" : "Edit Disabled";

        }// End Edit_Click

        private void DisplayPictures_DoubleTapped(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {

            // Adapted from http://stackoverflow.com/questions/13696210/how-to-get-tapped-item-in-gridview

            // Get the details of the selected Card
            var card = (Picture)(sender as GridView).SelectedItem;

            // If edit mode is enabled navigate to the edit page
            // Pass over the card with the details
            if (editEnabled == true)
                this.Frame.Navigate(typeof(EditCardPage), card);

        }// DisplayPictures_DoubleTapped

    }// End class MainPage

}// End namespace AutismCommunicationApp


