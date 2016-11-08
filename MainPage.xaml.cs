using AutismCommunicationApp.DataModel;
using System.Collections.Generic;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using System;
using AutismCommunicationApp.ViewModel;


namespace AutismCommunicationApp
{
    public sealed partial class MainPage : Page
    {
        private List<Picture> picturesList;
        StorageFile storageFile;
        WriteableBitmap bitmap;

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

            atd.deletePicture();

        }// End HamburgerButton_Click

        private async void MenuButton1_Click(object sender, RoutedEventArgs e)
        {
            /*
            * 
            *  Adapted from https://code.msdn.microsoft.com/windowsapps/How-to-upload-an-image-to-3293e4a8/sourcecode?fileId=153875&pathId=2041250276
            * 
           */

            // Be able to open the file explorer
            var pickerOpen = new FileOpenPicker();

            // Add the types of files that are suggested to the user
            pickerOpen.FileTypeFilter.Add(".jpg");
            pickerOpen.FileTypeFilter.Add(".jpeg");
            pickerOpen.FileTypeFilter.Add(".png");

            storageFile = await pickerOpen.PickSingleFileAsync();

            bitmap = new WriteableBitmap(50, 50);

            if (storageFile != null)
            {
                // Save path to text box
                //imagePath.Text = storageFile.Path;

            }// End if 
        }
    }// End class MainPage

}// End namespace AutismCommunicationApp
