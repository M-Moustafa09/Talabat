using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stor.Reposiotry.Data.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pictureurl",
                table: "produts",
                newName: "PictureUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PictureUrl",
                table: "produts",
                newName: "Pictureurl");
        }
    }
}
