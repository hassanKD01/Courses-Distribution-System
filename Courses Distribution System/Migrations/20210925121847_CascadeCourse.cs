using Microsoft.EntityFrameworkCore.Migrations;

namespace Courses_Distribution_System.Migrations
{
    public partial class CascadeCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Departments_DepartmentName",
                table: "Courses");

            migrationBuilder.AlterColumn<string>(
                name: "DepartmentName",
                table: "Courses",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Departments_DepartmentName",
                table: "Courses",
                column: "DepartmentName",
                principalTable: "Departments",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Departments_DepartmentName",
                table: "Courses");

            migrationBuilder.AlterColumn<string>(
                name: "DepartmentName",
                table: "Courses",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Departments_DepartmentName",
                table: "Courses",
                column: "DepartmentName",
                principalTable: "Departments",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
