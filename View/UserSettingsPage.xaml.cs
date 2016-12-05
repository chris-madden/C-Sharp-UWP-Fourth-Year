using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AutismCommunicationApp.View
{
  
    public sealed partial class UserSettingsPage : Page
    {

        private int maxPinCodeLength = 4;
        private int maxSentenceLength = 1;

        public UserSettingsPage()
        {
            this.InitializeComponent();
        }

        // Button to update the number of images that can be put into the communucation bar
        private void UpdateSize_Click(object sender, RoutedEventArgs e)
        {

            // Get access to resources for localisation
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();

            // Retrieve reqired strings
            var numberSuccessMessage = loader.GetString("SettingsNumberSuccess");
            var sizeWarning = loader.GetString("SettingsSizeWarning");
            var numberWarning = loader.GetString("SettingsNumberWarning");

            // If the user has entered a value
            if (!String.IsNullOrEmpty(SentenceSizeTextBox.Text) && SentenceSizeTextBox.Text.Length == maxSentenceLength)
            {

                // Load pincode from local settings
                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

                // ----------  Copyright of http://stackoverflow.com/questions/18251875/in-c-how-to-check-whether-a-string-contains-an-integer  ----------
                // Check if pin code entered consists of only numbers
                bool containsInt = SentenceSizeTextBox.Text.All(char.IsDigit);

                // If text entry is all digits
                if (containsInt)
                {

                    // Parse text entry into integer
                    int numberRangeChecker = Int32.Parse(SentenceSizeTextBox.Text);

                    // Number must be between 2 and 4 or will not be updated
                    if (numberRangeChecker >= 2 && numberRangeChecker <= 4)
                    {
                        // Change the local setting for the pin code
                        localSettings.Values["sentenceSize"] = SentenceSizeTextBox.Text;

                        // Leave success message
                        sizeWarningTextblock.Text = numberSuccessMessage + " " + numberRangeChecker;
                    }
                    else {
                        sizeWarningTextblock.Text = sizeWarning;
                    }
                   
                }
                else
                {
                    // Prompt user to only enter numbers
                    sizeWarningTextblock.Text = numberWarning;
                }

            }
            else
            {
                // Prompt user that pin code can only have four numbers
                sizeWarningTextblock.Text = sizeWarning;
            }

        }// End UpdateSize_Click

        private void UpdatePin_Click(object sender, RoutedEventArgs e)
        {

            // Get access to resources for localisation
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();

            // Retrieve reqired strings
            var pinCodeSuccess = loader.GetString("SettingsPinChangeSuccess");
            var promptUserLetters = loader.GetString("SettingsNoLettersAllowed");
            var promptUserPin = loader.GetString("SettingsPinHasFourNumbers");

            // If the user has entered a value
            if (!String.IsNullOrEmpty(UpdatePinTextBox.Text) && UpdatePinTextBox.Text.Length == maxPinCodeLength)
            {

                // Load pincode from local settings
                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

                // ----------  Copyright of http://stackoverflow.com/questions/18251875/in-c-how-to-check-whether-a-string-contains-an-integer  ----------
                // Check if pin code entered consists of only numbers
                bool containsInt = UpdatePinTextBox.Text.All(char.IsDigit);

                // If pin code is all digits
                if (containsInt)
                {
                    // Change the local setting for the pin code
                    localSettings.Values["pinCode"] = UpdatePinTextBox.Text;

                    // Leave success message
                    promptPinChange.Text = pinCodeSuccess + " " + UpdatePinTextBox.Text;
                }
                else {

                    // Prompt user to only enter numbers
                    promptPinChange.Text = promptUserLetters;
                }
               
            }
            else {

                // Prompt user that pin code can only have four numbers
                promptPinChange.Text = promptUserPin;
            }

        }// End UpdatePin_Click

        private void ReturnToMainPage_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

    }// End Class UserSettingsPage

}// End namespace AutismCommunicationApp.View
