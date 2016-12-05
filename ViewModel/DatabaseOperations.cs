using AutismCommunicationApp.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace AutismCommunicationApp
{
    class DatabaseOperations
    {

        // Delete image and all it's details
        public async void deleteImage(int id, string pictureName)
        {

            // Get access to database
            using (var db = new PictureContext())
            {

                // Find id in database 
                var image = db.Pictures.Single(i => i.pictureId == id);

                // Remove data from database
                db.Remove(image);

                // Save chanages to database
                await db.SaveChangesAsync();

                // ===== DELETE FILE FROM LOCAL STORAGE =====

                try
                {

                    // Point to the local storage folder
                    StorageFile deleteFile = await ApplicationData.Current.LocalFolder.GetFileAsync(pictureName);

                    // If file exists then delete it
                    if (deleteFile != null)
                    {
                        await deleteFile.DeleteAsync();
                    }

                }
                catch (System.IO.FileNotFoundException) { }

            }// End using

        }// End deleteImage

        // Update the label in the database
        public async void updateLabel(int id, string newLabel)
        {

            // Get access to database
            using (var db = new PictureContext())
            {

                // Find id in database 
                var label = db.Pictures.Single(i => i.pictureId == id);

                // Update the label
                label.pictureLabel = newLabel;

                // Save changes 
                await db.SaveChangesAsync();

            }//End using

        }// End updateLabel

    }// End DatabaseOperations

}// End AutismCommunicationApp
