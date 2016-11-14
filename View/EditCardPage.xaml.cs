using AutismCommunicationApp.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AutismCommunicationApp
{
    // This page will allow the user to delete images from database 
    // and also update labels
    public sealed partial class EditCardPage : Page
    {

        private int pictureID;
        private string pictureName;
        Picture card;
   

        public EditCardPage()
        {
            this.InitializeComponent();
        }

        // Recieve the image that was selected 
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Passed over the image name and extension
            card = e.Parameter as Picture;

            // Get ID of image
            pictureID = card.pictureId;

            pictureName = Path.GetFileName(card.picturePath);



        }// End OnNavigatedTo

        private void DeletePicture_Click(object sender, RoutedEventArgs e)
        {
            DatabaseOperations dataOp = new DatabaseOperations();

            dataOp.deleteImage(pictureID, pictureName);

            this.Frame.Navigate(typeof(MainPage));
        }
    }// End class EditCardPage

}
