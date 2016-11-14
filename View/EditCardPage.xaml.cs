using AutismCommunicationApp.DataModel;
using System;
using System.IO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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

            // Extract name from path
            pictureName = Path.GetFileName(card.picturePath);

        }// End OnNavigatedTo

        private void DeletePicture_Click(object sender, RoutedEventArgs e)
        {
            DatabaseOperations dataOp = new DatabaseOperations();

            dataOp.deleteImage(pictureID, pictureName);

            this.Frame.Navigate(typeof(MainPage));
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {

            DatabaseOperations dataOp = new DatabaseOperations();

            // If user has entered text for new label 
            if (!String.IsNullOrEmpty(Label.Text))
            {

                dataOp.updateLabel(pictureID, Label.Text);

                this.Frame.Navigate(typeof(MainPage));

            }// End if
            
        }// End Update_Click

    }// End class EditCardPage

}// AutismCommunicationApp
