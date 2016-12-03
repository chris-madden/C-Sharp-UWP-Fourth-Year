using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AutismCommunicationApp.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserSettingsPage : Page
    {

        private string promptUserPin = "Pin must be 4 exactly 4 numbers";
        private string promptUserLetters = "Pin can only contain numbers";

        public UserSettingsPage()
        {
            this.InitializeComponent();
        }

        private void UpdateSize_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdatePin_Click(object sender, RoutedEventArgs e)
        {

            // If the user has entered a value
            if (!String.IsNullOrEmpty(UpdatePinTextBox.Text) && UpdatePinTextBox.Text.Length == 4)
            {

                // Load pincode from local settings
                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

                // Check if pin code entered consists of only numbers
                bool containsInt = UpdatePinTextBox.Text.All(char.IsDigit);

                // If pin code is all digits
                if (containsInt)
                {
                    // Change the local setting for the pin code
                    localSettings.Values["pinCode"] = UpdatePinTextBox.Text;
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
