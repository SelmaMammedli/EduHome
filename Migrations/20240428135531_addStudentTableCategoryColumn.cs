using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduHome.Migrations
{
    /// <inheritdoc />
    public partial class addStudentTableCategoryColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Courses_CoursesId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "CoursesId",
                table: "Students",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_CoursesId",
                table: "Students",
                newName: "IX_Students_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Categories_CategoryId",
                table: "Students",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Categories_CategoryId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Students",
                newName: "CoursesId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_CategoryId",
                table: "Students",
                newName: "IX_Students_CoursesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Courses_CoursesId",
                table: "Students",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
