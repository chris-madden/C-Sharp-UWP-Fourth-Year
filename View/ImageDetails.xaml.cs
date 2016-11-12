using AutismCommunicationApp.DataModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace AutismCommunicationApp
{

    // This page will display the selected image and allow user to give the image a label
    public sealed partial class ImageDetails : Page
    {

        private string imagePath;
       
        public ImageDetails()
        {

            this.InitializeComponent();

        }// End Constructor

        // Recieve the image that was selected 
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Passed over the image name and extension
            string imageName = e.Parameter as string;

            // Returns the file that was just copied to local folder
            StorageFile foundFile = await SearchForFile(imageName);

            if (foundFile != null)
            {

                // Store the path of the image in the local folder
                imagePath = foundFile.Path;

                // Convert file to a bitmap image
                BitmapImage bp = await ImageUtils.StorageFileToBitmapImage(foundFile);
                // NEED TO SHOW IMAGE TO XAML
                SelectedImage.Source = bp;
            }

        }// End OnNavigatedTo

        // This method searches the devices local folder for the required image and if image is found it is 
        // returned as a storage file
        private async Task<StorageFile> SearchForFile(string imageName)
        {

            // Point to the devices local folder 
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;

            // Put files into a list
            IReadOnlyList<StorageFile> fileList =await localFolder.GetFilesAsync();

            StorageFile foundFile;

            // Loop through the list
            foreach (StorageFile file in fileList)
            {

                // If the file matches the recently copied file
                if (file.Name == imageName)
                {

                    // Store the file in a new Storage File
                    foundFile = file;
                    return foundFile;

                }// End if
               
            }// End loop

            // If file was not mathed return null
            return null;

        }// End method SearchForFile

        // This method saves the image path and its label to the database
        private void SaveToDB_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

            // Get text from label textbox
            string label = Label.Text;

            // Adapted from https://msdn.microsoft.com/en-us/library/system.string.isnullorempty(v=vs.110).aspx

            // If the label textbox has text in it 
            if (!String.IsNullOrEmpty(label))
            {

                // ==========  SAVE TO DATABASE  ==========

                // Get access to the database
                using (var db = new PictureContext())
                {

                    // Store image path and label into model
                    var picture = new Picture { picturePath = imagePath, pictureLabel = label };

                    // Add picture to table
                    db.Pictures.Add(picture);

                    // Save the database with the new picture
                    db.SaveChangesAsync();

                }// End using

            }// End if

        }// End SaveToDB_Click

    }// End class ImageDetails

}// End namespace AutismCommunicationApp
