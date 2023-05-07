using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageShareLikesEF.Data.Migrations
{
    public partial class filenamecolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Src",
                table: "Images",
                newName: "FileName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Images",
                newName: "Src");
        }
    }
}
