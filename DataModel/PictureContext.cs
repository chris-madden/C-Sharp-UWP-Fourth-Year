using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AutismCommunicationApp.DataModel
{
    class PictureContext : DbContext
    {

        public DbSet<Picture> Pictures { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Pictures.db");
        }

    }
}
