<div align="center"><img src ="https://github.com/chris2020/C-Sharp-UWP-Fourth-Year/blob/master/Readme_Images/communicating.png"/></div>

# Communication App for Children with Autism

**Student Name:** Christy Madden <br />
**Student ID:** G00214065 <br />
**Module:** Mobile Applications Development 3 <br />
**Lecturer:** Dr Martin Kenirons <br />

# Introduction

### What is the application?

This is a communication app for non verbal children. Its purpose is to be used as a tool to allow children to express what they want by building sentences from pictures. It takes after the well known Picture Exchange Communication System (PECS) which was designed for children with Austism Specturm Disorder. For further reading on PECS click on this link [click on this link](http://www.pecsusa.com/pecs.php).

### What does the appication run on?

This application is built as a Universal Windows Platfrom (UWP) app. It is designed to be usable on Windows phones, tablets and desktops. This application is aimed at the Windows 10 operating system and will not run on Windows 8 or 8.1 devices. For more information about why [click on this link](http://stackoverflow.com/questions/30317848/run-windows-10-universal-apps-on-windows-8-1).

### What is the application built with?

The application was built with _C#_ and _XAML_. _XAML_ is used for designing the Graphical User Interface (GUI), while _C#_ is used for the back-end. This application is using Microsofts new open source _.NET Core Framework_ and was built using _Visual Studio 15_.

### How can the application be deployed?

If you want to run the application locally on your own device you will first need to to have Windows 10 with the anniversary update and you will need Visual Studio 15. There will be other packages needed to run the app but Visual Studio will prompt you and tell you which ones you need to install. 

Once you meet that criteria, you can clone my repository or download the zip file from this [link](https://github.com/chris2020/C-Sharp-UWP-Fourth-Year). After that you can open Visual Studio and select File-> Open-> Project/Solution. Then you can navigate to the folder you cloned or downloaded and select the file named AutismCommunicationApp, this is a _.csproj_ file and will open the app in Visual Studio. From there you can choose to run the open using _Local Machine_ or deploy it to a Windows 10 phone or tablet.

**Note**<br />
Just a note about some files you don't need. There are **2 folders** that contain images that are used for the README file and Wiki. You can delete this folders if you wish as there are not part of the project itself. The folders are called *Readme_Images* and *ResearchImages*.

### Where can I find more details?

I strongly encourage you to check out the **_Wiki_** for this application (and not just because so much time went in to it!). In it you will find a lot more information about the application, from how to use it (there are some extra features that you might not figure out straight away without reading about them) to more details about the development of the application. In it you can find out how the application was researched, developed and tested as well as some nice pictures. To find the Wiki click on this [link](https://github.com/chris2020/C-Sharp-UWP-Fourth-Year/wiki)

# Features 

### Adding Pictures To App
This is an important point to highlight as it's the drving force of the whole application. When the application is run on a new device for the first time it has no pictures but without pictures the app serves no purpose. For this reason the app provides **_3_** different ways to save pictures.

1. Add them from the device. This method opens the file explorer and lets you bring in any pictures that are already currently stored on the device.
2. Open the web browser. Users can get the web browser to open which will allow the them to search and download pictures they want. 
3. Use the devices camera. The last method is accessing the devices camera and taking a photo. Once the photo has been accepted, the user is brought back to the application where they can decide to save it or not.

### MVVM Architecture

This app was designed using Microsofts _MVVM architecture_ which allows for _separation of concerns_. The _View_ is separated from the _Model_ and the two are connected by the _ViewModel_, this can be seen with my folder structure as I have a folder for each these containing the appropriate classes. 

#### _Folders and Classes_

|View|Description|
|---|---|
|MainPage.xaml| This is the main page of the appication and where the user will spend most of their time. Here pictures are displayed and sentences can be built for communication. This page also provides three separate ways a user can add a picture to the app while also giving user access to the other pages in the app |
|ImageDetails.xaml|This is the page that will allow the user to view and add a new picture to the application. The user gives the picture a label to describe it. The picture can also be discarded if the user does not want to incorporate it into the app |
|EditCardPage.xaml| This page enables users to update or delete pictures that are currently saved in the application |
|UserSettingsPage.xaml| This page lets users change some of the applications settings such as the pin code and the number of pictures allowed in a sentence |

**_NOTE_**<br />
All these xaml pages have code behind them stored in C# classes. These classes contain functionality such as navigating pages, reacting to button clicks and validation.

|ViewModel|Description|
|---|---|
|PictureManager.cs| This loads the data from the database and passes an observable collection to the view |
|DatabaseOperations.cs|This file handles database operations passed from the view and updates the database accordingly|

|DataModel|Description|
|---|---|
|Picture.cs| This file contains provides the model for the pictures. It has the attributes of a picture such as it's ID, storage path and label |
|PictureContext.cs|This file has configuration settings for the database|

### SQLite Database

The application stores picture data in the lightweight SQLite database. It is stored locally in the app within the _localstorage_ folder. It is a _code first_ database meaning it was generated from classes within the app. It uses the Picture.cs file to build a table and proves a benefit of using the .NET core framework.  

**_NOTE_**<br />
The Entity Framework Core is used for this and _Linq_ is also used for database operations in the app. An example of one of the uses of Linq can be seen in the code snippet below.

```C#

 bool containsInt = UpdatePinTextBox.Text.All(char.IsDigit);
 
```

### Local Storage
 As mentioned above the SQLite database is stored in the apps local storage but it used more for just the database. The images in the app are stored to local storage also. This can have benefits as the images can be backed up by the system is the device supports _App Data Backup_. An example of using local storage is below
 
```C#
 
// pick file and store it in a storage file
StorageFile storageFile = await pickerOpen.PickSingleFileAsync();

// Check if file is selected
if (storageFile != null)
{
    // Copy the file into the devices local storage
    // If file alredy exists replace the copy
    await storageFile.CopyAsync(ApplicationData.Current.LocalFolder, storageFile.Name, NameCollisionOption.ReplaceExisting);
}

```

**_NOTE_**<br />
For more information about backing up images click this [link](https://blogs.windows.com/buildingapps/2016/05/10/getting-started-storing-app-data-locally/#Oem4oVmKcGaopeS3.97). 
 
### Local Settings
 
The application also makes use of _Local Settings_. Local settings is used for one off bits of data that needs to be stored locally to the device. Examples of this include the pin code and size of the sentence allowed which are found in the settings page. The link above from the local storage section also provides details about local settings. Below is a code snippet showing an example of its use in the app.

```C#

// Access local settings
var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

// Read local settings for pin code
Object loadPin = localSettings.Values["pinCode"];

// If there is no pincode set one to a default value
if (loadPin == null)
{
    localSettings.Values["pinCode"] = "9999";
}

```

### Observable Collection
Observable collections are used in this app and are very important for the main page. The main page has 2 separate observable collections, one for displaying the content, which are the pictures that can be used for communicating and the other is used for to show what pictures are being used to build a sentence. When a picture is moved into the sentence builder section it is visibly removed from the content section and vice versa. This dynamic view could not be achieved without the use of observable collections.

### Drag and Drop UI
Combined with the observable collections is the ability to drag and drop the pictures from one collection to the other. The app makes use of the _DragItemsStarting_, _DragOver_ and _Drop_ events. This means the user can hold down on a picture and move it around the screen, if the picture is placed over one of the grid views which has an observable collection the picture will be placed into that collection and removed from the one it was in. The following code snippet demonstrates the observable collections being updated when dropped into the communication bar (used to display the sentences).

```C#

var listViewItemsSource = destinationListView.ItemsSource as ObservableCollection<Picture>;

if (listViewItemsSource != null)
{

    // Loop through list containing picture
    foreach (var itemId in itemIdsToMove)
    {

        // Catch exception that won't let you drop an image in the same list it is already in
        try
        {

            // Find picture in list 
            var itemToMove = this.Pictures.First(i => i.pictureId.ToString() == itemId);

            // If communication bar has no more than 2 pictures in it
            if (listViewItemsSource.Count() < maxPictures)
            {

                // Move picture to communication bar
                listViewItemsSource.Add(itemToMove);

                // Remove picture from display 
                this.Pictures.Remove(itemToMove);

            }// End if
        }
        catch (InvalidOperationException){ }
        
    }// End foreach
    
}// End if

```

### Databinding 
The application uses _x:Bind_ to display the pictures and labels from the observable collection into the view. It is used to two separated places in the app, the first is the main page to bind a collection to a grid view and the second is in a card template which binds the picture and the label specifically. Below is a code snippet to show the grid view binding.

```XAML

 <!-- ==========  Picture Display  ========== -->
<GridView ItemsSource="{x:Bind Pictures}" 
          Name="DisplayPictures"
          ScrollViewer.VerticalScrollMode="Disabled"
          ScrollViewer.VerticalScrollBarVisibility="Hidden"
          ScrollViewer.HorizontalScrollMode="Auto"
          ScrollViewer.HorizontalScrollBarVisibility="Visible"
          AllowDrop="True"
          CanDragItems="True"
          DragItemsStarting="DisplayPictures_DragItemsStarting"
          DragOver="DisplayPictures_DragOver"
          Drop="DisplayPictures_Drop"
          IsDoubleTapEnabled="True"
          DoubleTapped="DisplayPictures_DoubleTapped"
          Grid.Row="0">

```

### Templates
The app uses _templates_ to combine the label with the picture. Using this method means images are always given their correct label. These templates are then put into an observable collection as a single item meaning when the collection is displayed it shows the picture with its label as a single item in the grid view. The code below shows the databinding in the template.

```XAML

<Image Name="CardPicture" Width="100" Height="100" Stretch="Fill" Source="{x:Bind Picture.picturePath}" />
<TextBlock Name="CardText" FontSize="16" Text="{x:Bind Picture.pictureLabel}" HorizontalAlignment="Center" />

```

 **_NOTE_**<br />
 This code is from the CardTemplate.xaml file

### Visual State Manager
The visual state manager is used on all the pages. It has three states that it reacts to, a phone state, a tablet state and a desktop state. It's main fuction is to change the size of the images and the label font size as if the pictures are too big, the screen can only display a few at a time making the app inefficient. By making the images and font smaller on smaller screens it means more pictures can be displayed and this leads to a better user experience with less scrolling or swiping needed. Code below shows one of the states.

```XAML

<!--  ==========  Phone state  ==========  -->
<VisualState x:Name="VisualStatePhone">
    
    <VisualState.StateTriggers>
        <AdaptiveTrigger MinWindowWidth="0"/>
    </VisualState.StateTriggers>
    
    <VisualState.Setters>
        <Setter Target="CardPicture.Height" Value="100" />
        <Setter Target="CardPicture.Width" Value="100" />
        <Setter Target="CardText.FontSize" Value="10"/>
    </VisualState.Setters>
    
</VisualState>

```

