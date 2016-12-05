using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Microsoft.EntityFrameworkCore;
using AutismCommunicationApp.DataModel;

namespace AutismCommunicationApp
{
   
    sealed partial class App : Application
    {

        // These variables are used for local settings
        public static int pinCodeLocally;
        public static int comBarSize;

        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            using (var db = new PictureContext())
            {
                db.Database.Migrate();
            }
        }

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

            // Read local settings for pin code
            Object loadPin = localSettings.Values["pinCode"];

            // If there is no pincode set one to a default value
            if (loadPin == null)
            {
                localSettings.Values["pinCode"] = "9999";
            }

            // =====  Sentence Size  =====

            // Read local settings for sentence size
            Object size = localSettings.Values["sentenceSize"];

            // If there is no size set one to a default value
            if (size == null)
            {
                localSettings.Values["sentenceSize"] = "2";
            }

        }// End OnLaunched

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
