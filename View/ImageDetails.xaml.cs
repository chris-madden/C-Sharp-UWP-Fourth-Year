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
        private string imageName;
        // string textValidation = "Label must be 20 characters or less";
        //private string noLabelValidation = "You must give the picture a label";
        private StorageFile foundFile;
      
        public ImageDetails()
        {

            this.InitializeComponent();

        }// End Constructor

        // Recieve the image that was selected 
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Passed over the image name and extension
            imageName = e.Parameter as string;

            // Returns the file that was just copied to local folder
            foundFile = await SearchForFile(imageName);

            if (foundFile != null)
            {

                // Store the path of the image in the local folder
                imagePath = foundFile.Path;

                BitmapImage bp;

                try
                {
                    // Convert file to a bitmap image
                    bp = await ImageUtils.StorageFileToBitmapImage(foundFile);

                    // NEED TO SHOW IMAGE TO XAML
                    SelectedImage.Source = bp;
                }
                catch (Exception) {

                    // If file is empty ( 0KB ) then the exception for component not found will be caught
                    // If exception is caught then bring user back to main page
                    this.Frame.Navigate(typeof(MainPage));

                }// End try catch
               
            }// End if

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

            // Get access to resources for localisation
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();

            // Retrieve reqired strings
            var textValidation = loader.GetString("ImageDetailsTextValidation"); 
            var noLabelValidation = loader.GetString("ImageDetailsNoLabelValidation");

            // Get text from label textbox
            string label = Label.Text;

            // Check that string is 20 characters or less
            if (label.Length <= 20)
            {

                // ----------  Adapted from https://msdn.microsoft.com/en-us/library/system.string.isnullorempty(v=vs.110).aspx  ----------
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

                    // Clear the textbox
                    label = "";

                    // Navigate back to main page
                    this.Frame.Navigate(typeof(MainPage));

                }// End if
                else { 

                // Display message to user saying the picture must have a label
                TextValidation.Text = noLabelValidation;

                }// End if (String is empty or not)

            }
            else {

                // Display message to user saying it must be 20 characters or less
                TextValidation.Text = textValidation;

            }// End if (string is 20 characters or not)

            // Clear the textbox
            label = "";

        }// End SaveToDB_Click

        // Will delete the file and return to the main page if Discard button is clicked
        private async void DiscardPicture_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

            // Get file 
            foundFile = await SearchForFile(imageName);

            try {

                // Point to the local storage folder
                StorageFile deleteFile = await ApplicationData.Current.LocalFolder.GetFileAsync(foundFile.Name);

                // If file exists then delete it
                if (deleteFile != null)
                {
                    await deleteFile.DeleteAsync();
                }

            }
            catch (NullReferenceException) { }
            
            // Bring user back to main page
            this.Frame.Navigate(typeof(MainPage));

        }// End DiscardPicture_Click

    }// End class ImageDetails

}// End namespace AutismCommunicationApp
