using Microsoft.EntityFrameworkCore.Migrations;

namespace Courses_Distribution_System.Migrations
{
    public partial class AddArchiveCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Archived",
                table: "Professors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Archived",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Archived",
                table: "Professors");

            migrationBuilder.DropColumn(
                name: "Archived",
                table: "Courses");
        }
    }
}
