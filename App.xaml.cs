using Microsoft.WindowsAzure.MobileServices;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Microsoft.EntityFrameworkCore;
using AutismCommunicationApp.DataModel;

namespace AutismCommunicationApp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {

        // These variables are used for local settings
        public static int pinCodeLocally;
        public static int comBarSize;

       /* Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
        */
      
        // This MobileServiceClient has been configured to communicate with the Azure Mobile Service and
        // Azure Gateway using the application key. You're all set to start working with your Mobile Service!

        // **********  CLOUD ADDRESS FOR SAVING DATA =  https://autismcommunicationapp.azurewebsites.net**********
        public static MobileServiceClient MobileService = new MobileServiceClient(
            "https://autismcommunicationapp.azurewebsites.net"
        );
        
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            using (var db = new PictureContext())
            {
                db.Database.Migrate();
            }
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
           
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();

            // ====================  LOCAL SETTINGS  ====================

            // -----  Adapted from https://msdn.microsoft.com/library/windows/apps/xaml/windows.storage.applicationdata.localsettings.aspx  -----

            // =====  Pin Code  =====

            // Access local settings
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            // Read locla settings for pin code
            Object loadPin = localSettings.Values["pinCode"];

            // If there is no pincode set one to a default value
            if (loadPin == null)
            {
                localSettings.Values["pinCode"] = "9999";
            }

            // =====  Sentence Size  =====

            Object size = localSettings.Values["sentenceSize"];

            // If there is no size set one to a default value
            if (size == null)
            {
                localSettings.Values["sentenceSize"] = "2";
            }

            Object test = localSettings.Values["sentenceSize"];

        }// End OnLaunched

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
