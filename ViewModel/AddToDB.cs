using AutismCommunicationApp.DataModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutismCommunicationApp.ViewModel
{
    class AddToDB
    {

        /*
        public void Add()
        {
            using (var db = new PictureContext())
            {
                var picture = new Picture { picturePath = "Assets/Here", pictureLabel = "Test" };

                // Add picture info into table
                db.Pictures.Add(picture);

                db.SaveChanges();

                //Debug.WriteLine("Picture id = " + picture.pictureId);

                Debug.WriteLine("Picture path is " + picture.pictureId + "Picture label is " + picture.pictureLabel);

            }
        }
        */

        public void deletePicture()
        {

            using (var db = new PictureContext())
            {

                var picture = db.Pictures.First();

                db.Pictures.Remove(picture);

                db.SaveChanges();

                Debug.WriteLine("Picture id is " + picture.pictureId + "Picture label is " + picture.pictureLabel);

            }

        }// End deletePicture
    }
}
