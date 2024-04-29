using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduHome.Migrations
{
    /// <inheritdoc />
    public partial class addLinkandImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Welcomes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VideoLink",
                table: "Welcomes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Welcomes");

            migrationBuilder.DropColumn(
                name: "VideoLink",
                table: "Welcomes");
        }
    }
}
