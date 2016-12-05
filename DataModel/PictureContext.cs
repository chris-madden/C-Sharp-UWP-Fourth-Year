using Microsoft.EntityFrameworkCore;

namespace AutismCommunicationApp.DataModel
{
    // Class name here is name of database
    class PictureContext : DbContext
    {

        // Name of Table
        public DbSet<Picture> Pictures { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Pictures.db");
        }

    }
}
