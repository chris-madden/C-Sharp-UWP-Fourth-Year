using AutismCommunicationApp.DataModel;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AutismCommunicationApp
{
    public sealed partial class MainPage : Page
    {
        private List<Picture> picturesList;

        public MainPage()
        {

            this.InitializeComponent();

        }// End Constructor

        // Code for humburger button click
        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {

            // Set pane to open if it closed
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;

        }// End HamburgerButton_Click

    }// End class MainPage

}// End namespace AutismCommunicationApp
