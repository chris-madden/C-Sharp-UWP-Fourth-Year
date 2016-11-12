using AutismCommunicationApp.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    class PictureManager
    {

        public static List<Picture> loadData()
        {

            // Load all data from database
            using (var context = new PictureContext())
            {

                // Load all data into a list
                var pictures = context.Pictures.ToList();

                return pictures;

            }// End using

        }// End loadData

    }// End PictureManager

}// End namespace ViewModel
