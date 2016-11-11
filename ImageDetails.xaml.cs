using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
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
    public sealed partial class ImageDetails : Page
    {
        public string imagePath { get; set; }
        public BitmapImage image { get; set; }
        public string fileName { get; set; }

        public ImageDetails()
        {

            this.InitializeComponent();

        }// End Constructor

        // Recieve the image that was selected 
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var details = (ImageDetails)e.Parameter;

            imagePath = details.imagePath;
            SelectedImage.Source = details.image;

            Debug.WriteLine("Test = " + imagePath);

        }// End OnNavigatedTo

        // Save the selected image to local storage
        private async void SaveImage_Click(object sender, RoutedEventArgs e)
        {
            /*
            // Take image from the view
            BitmapImage sImage = SelectedImage.Source as BitmapImage;

            // Create a wrtiable bitmap
            WriteableBitmap bitmap = new WriteableBitmap(50, 50);

            // Get image from the view
            TestImage.Source = sImage;

            string pathOfImage = imagePath;

            // Store image in a StorageFile
            StorageFile sf = await StorageFile.
            */

            // Be able to open the file explorer
            var pickerOpen = new FileOpenPicker();

            // Add the types of files that are suggested to the user
            pickerOpen.FileTypeFilter.Add(".jpg");
            pickerOpen.FileTypeFilter.Add(".jpeg");
            pickerOpen.FileTypeFilter.Add(".png");

            // storageFile here is holding the image but only as a File
            StorageFile storageFile = await pickerOpen.PickSingleFileAsync();

            WriteableBitmap bitmap = new WriteableBitmap(50, 50);

            // Read inage into an access stream
            using (IRandomAccessStream fileStream = await storageFile.OpenAsync(FileAccessMode.Read))
            {
                await bitmap.SetSourceAsync(fileStream);
            }

            // If there is an image
            if (storageFile != null)
            {

                /*
                * 
                *  adapted from  http://stackoverflow.com/questions/34362838/storing-a-bitmapimage-in-localfolder-uwp
                * 
               */

                // Create a file in the local folder
                StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName +".jpg", CreationCollisionOption.FailIfExists);

                // Writes the image to the devices local storage 
                using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
                    Stream pixelStream = bitmap.PixelBuffer.AsStream();
                    byte[] pixels = new byte[pixelStream.Length];
                    await pixelStream.ReadAsync(pixels, 0, pixels.Length);

                    encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore,
                   (uint)bitmap.PixelWidth, (uint)bitmap.PixelHeight, 96.0, 96.0, pixels);
                    await encoder.FlushAsync();

                }// End using


            }// End if

        }// End SaveImage_Click

    }// End class ImageDetails

}// End namespace AutismCommunicationApp
