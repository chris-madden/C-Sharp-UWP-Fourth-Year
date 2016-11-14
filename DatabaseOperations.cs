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

            using (var db = new PictureContext())
            {

                // Find id in database and delete it
                var image = db.Pictures.Single(i => i.pictureId == id);

                // Remove data from database
                db.Remove(image);

                // Save chanages to database
                await db.SaveChangesAsync();

                // Point to the local storage folder
                StorageFile deleteFile = await ApplicationData.Current.LocalFolder.GetFileAsync(pictureName);

                // If file exists then delete it
                if (deleteFile != null)
                {
                    await deleteFile.DeleteAsync();
                }


            }// End using

        }// End deleteImage

        // Update the label in the database

    }// End DatabaseOperations

}// End AutismCommunicationApp
