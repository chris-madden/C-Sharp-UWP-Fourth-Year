using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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

        public ImageDetails()
        {

            this.InitializeComponent();

        }// End Constructor

        /*public ImageDetails(WriteableBitmap image, string imagePath)
        {

            this.image = image;
            this.imagePath = imagePath;

            Debug.WriteLine("Items passed are " + imagePath);

        }// End constructor
        */
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var details = (ImageDetails)e.Parameter;

            imagePath = details.imagePath;
            SelectedImage.Source = details.image;

            Debug.WriteLine("Test = " + imagePath);

        }// End OnNavigatedTo

       
    }
}
