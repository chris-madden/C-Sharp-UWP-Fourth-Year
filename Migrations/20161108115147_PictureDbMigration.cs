using Microsoft.EntityFrameworkCore.Migrations;

namespace AutismCommunicationApp.Migrations
{
    public partial class PictureDbMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    pictureId = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    pictureLabel = table.Column<string>(nullable: true),
                    picturePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.pictureId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pictures");
        }
    }
}
