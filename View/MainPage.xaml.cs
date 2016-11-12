using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System;
using AutismCommunicationApp.DataModel;
using System.Collections.Generic;
using ViewModel;

namespace AutismCommunicationApp
{
    public sealed partial class MainPage : Page
    {

        private List<Picture> Pictures;
        
        public MainPage()
        {

            this.InitializeComponent();

            this.Pictures = PictureManager.loadData();

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

    }// End class MainPage

}// End namespace AutismCommunicationApp


