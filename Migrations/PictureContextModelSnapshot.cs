using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using AutismCommunicationApp.DataModel;

namespace AutismCommunicationApp.Migrations
{
    [DbContext(typeof(PictureContext))]
    partial class PictureContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
