using AutismCommunicationApp.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    class PictureManager
    {

        public static ObservableCollection<Picture> loadData()
        {

            // Load all data from database
            using (var context = new PictureContext())
            {

                // Load all data into a list
                var pictures = context.Pictures.ToList();

                // Convert list to observable collection
                ObservableCollection <Picture> oPictures = new ObservableCollection<Picture>(pictures);

                return oPictures;

            }// End using

        }// End loadData

    }// End PictureManager

}// End namespace ViewModel
