using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Pickers.Provider;
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
    ///     Page is used to load an image from the devices local storage and save it to the app 
    /// </summary>
    public sealed partial class SentenceBuilderPage : Page
    {
        public SentenceBuilderPage()
        {
            this.InitializeComponent();
        }

        // Button will open up the file explorer
        async void UploadButton_Click(object sender, RoutedEventArgs e)
        {


            /*
             * 
             *  Adapted from https://code.msdn.microsoft.com/windowsapps/How-to-upload-an-image-to-3293e4a8/sourcecode?fileId=153875&pathId=2041250276
             * 
            */

            // Be able to open the file explorer
            var pickerOpen = new Windows.Storage.Pickers.FileOpenPicker();

            // Add the types of files that are suggested to the user
            pickerOpen.FileTypeFilter.Add(".jpg");
            pickerOpen.FileTypeFilter.Add(".jpeg");
            pickerOpen.FileTypeFilter.Add(".png");

            /*
             * 
             *  Copyright http://lunarfrog.com/blog/how-to-save-writeablebitmap-as-png-file
             * 
            */

            // ****************  Comment this code  ****************
            WriteableBitmap bitmap = new WriteableBitmap(50, 50);
            StorageFile file = await pickerOpen.PickSingleFileAsync();

            using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read))
            {
                await bitmap.SetSourceAsync(fileStream);
            }

            FileSavePicker picker = new FileSavePicker();
            picker.FileTypeChoices.Add("PNG File", new List<string> { ".png"});
            picker.FileTypeChoices.Add("JPG File", new List<string> { ".jpg" });
            picker.FileTypeChoices.Add("JPEG File", new List<string> { ".jpeg" });
            StorageFile destFile = await picker.PickSaveFileAsync();

            // ----------------  AN EXCEPTION HERE WILL NEED TO BE HANDLED IN CASE USER EXITS BEFORE SAVING PICTURE  ----------------
            using (IRandomAccessStream stream = await destFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
                Stream pixelStream = bitmap.PixelBuffer.AsStream();
                byte[] pixels = new byte[pixelStream.Length];
                await pixelStream.ReadAsync(pixels, 0, pixels.Length);

                encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore,
                            (uint)bitmap.PixelWidth, (uint)bitmap.PixelHeight, 96.0, 96.0, pixels);
                await encoder.FlushAsync();
            }

        }// End method UploadButton_Click


        // ----------------  NEED NEW BUTTON TO ASK IF USER WANTS TO SAVE THE SELECTED IMAGE  ----------------

       
    }// End class

}// End namespace
