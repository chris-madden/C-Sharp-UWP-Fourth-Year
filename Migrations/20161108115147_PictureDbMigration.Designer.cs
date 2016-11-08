using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using AutismCommunicationApp.DataModel;

namespace AutismCommunicationApp.Migrations
{
    [DbContext(typeof(PictureContext))]
    [Migration("20161108115147_PictureDbMigration")]
    partial class PictureDbMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1");

            modelBuilder.Entity("AutismCommunicationApp.DataModel.Picture", b =>
                {
                    b.Property<int>("pictureId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("pictureLabel");

                    b.Property<string>("picturePath");

                    b.HasKey("pictureId");

                    b.ToTable("Pictures");
                });
        }
    }
}
