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

namespace AutismCommunicationApp
{
    public sealed partial class MainPage : Page
    {

        // Private list used to bind to MainPage view
        private List<Picture> Pictures;
        private ObservableCollection<Picture> communicationBar;
        
        public MainPage()
        {

            this.InitializeComponent();

            // Save data to this list
            this.Pictures = PictureManager.loadData();

            this.communicationBar = new ObservableCollection<Picture>();

        }// End Constructor

        // Code for humburger button click
        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {

            // Set pane to open if it closed
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;

        }// End HamburgerButton_Click

        // This button opens file explorer, copies the selected file and navigates to the ImageDetails page
        private async void MenuButton1_Click(object sender, RoutedEventArgs e)
        {

            // adapted from http://stackoverflow.com/questions/39111925/uwp-copy-file-from-fileopenpicker-to-localstorage

            // Open file explorer
            var pickerOpen = new FileOpenPicker();

            // Have a filter for png files
            pickerOpen.FileTypeFilter.Add(".png");

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

        /*
         *  Adapted from http://www.shenchauhan.com/blog/2015/8/23/drag-and-drop-in-uwp
        */

        // Code for gridview displaying pictures
        private void DisplayPictures_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            var items = string.Join(",", e.Items.Cast<Picture>().Select(i => i.pictureId));
            e.Data.SetText(items);
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
            if (e.DataView.Contains(StandardDataFormats.Text))
            {
                var id = await e.DataView.GetTextAsync();
                var itemIdsToMove = id.Split(',');

                var destinationListView = sender as GridView;
                var listViewItemsSource = destinationListView?.ItemsSource as ObservableCollection<Picture>;

                if (listViewItemsSource != null)
                {
                    foreach (var itemId in itemIdsToMove)
                    {
                        var itemToMove = this.Pictures.First(i => i.pictureId.ToString() == itemId);

                        listViewItemsSource.Add(itemToMove);
                        this.Pictures.Remove(itemToMove);
                    }
                }
            }
        }
    }// End class MainPage

}// End namespace AutismCommunicationApp


