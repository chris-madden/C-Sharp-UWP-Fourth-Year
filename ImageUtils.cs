using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace AutismCommunicationApp
{
    class ImageUtils
    {

        /*
        * 
        *  Copyright http://windowsapptutorials.com/tips/storagefile/convert-storagefile-to-a-bitmapimage-in-universal-windows-apps/
        * 
       */
        public static async Task<BitmapImage> StorageFileToBitmapImage(StorageFile savedStorageFile)
        {
            using (IRandomAccessStream fileStream = await savedStorageFile.OpenAsync(FileAccessMode.Read))
            {

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.DecodePixelHeight = 200;
                bitmapImage.DecodePixelWidth = 200;
                await bitmapImage.SetSourceAsync(fileStream);
                return bitmapImage;

            }// End using

        }// End StorageFileToBitmapImage

    }// End ImageUtils

}// End AutismCommunicationApp
