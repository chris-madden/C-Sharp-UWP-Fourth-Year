using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AutismCommunicationApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DisplayPictures : Page
    {
        public DisplayPictures()
        {
            this.InitializeComponent();
        }

        // Load an image
        async void loadImagesButton_Click(object sender, RoutedEventArgs e)
        {

            // Point to Local folder
            StorageFolder folder = ApplicationData.Current.LocalFolder;

            // Point to pictures folder in the local folder
            StorageFolder picturesSubFolder = await folder.GetFolderAsync("Pictures");

            // Get the file called Test.jpg in the pictures subfolder
            StorageFile file = await picturesSubFolder.GetFileAsync("Test.jpg");

            // ********** adapted from  http://windowsapptutorials.com/tips/storagefile/convert-storagefile-to-a-bitmapimage-in-universal-windows-apps/  **********

            // Convert storage file to bitmap image
            BitmapImage bitmapImage = await ImageUtils.StorageFileToBitmapImage(file);

            firstImage.Source = bitmapImage;

        }

    }// End class DisplayIamges


    /*
     * 
     *  Copyright http://windowsapptutorials.com/tips/storagefile/convert-storagefile-to-a-bitmapimage-in-universal-windows-apps/
     * 
    */
    public class ImageUtils
    {
        public static async Task<BitmapImage> StorageFileToBitmapImage(StorageFile savedStorageFile)
        {
            using (IRandomAccessStream fileStream = await savedStorageFile.OpenAsync(FileAccessMode.Read))
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.DecodePixelHeight = 100;
                bitmapImage.DecodePixelWidth = 100;
                await bitmapImage.SetSourceAsync(fileStream);
                return bitmapImage;
            }
        }
    }

}// End namespace AutismCommunicationApp
