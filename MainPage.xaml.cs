using AutismCommunicationApp.DataModel;
using System.Collections.Generic;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using System;
using AutismCommunicationApp.ViewModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Graphics.Imaging;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;

namespace AutismCommunicationApp
{
    public sealed partial class MainPage : Page
    {
        private List<Picture> picturesList;
        StorageFile storageFile;
        string fileName;

        public MainPage()
        {

            this.InitializeComponent();

        }// End Constructor

        // Code for humburger button click
        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {

            // Set pane to open if it closed
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;

            AddToDB atd = new AddToDB();

            //atd.deletePicture();

        }// End HamburgerButton_Click

        // This button needs to direct you to new page
        // New page needs to display the selected image and give options to 
        // 1. Change the image name
        // 2. Give the image a label
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
                await storageFile.CopyAsync(ApplicationData.Current.LocalFolder);
            }

        }// End MenuButton1_Click

    }// End class MainPage

}// End namespace AutismCommunicationApp


