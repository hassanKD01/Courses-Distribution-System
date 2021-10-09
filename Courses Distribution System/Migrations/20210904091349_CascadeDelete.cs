using Microsoft.EntityFrameworkCore.Migrations;

namespace Courses_Distribution_System.Migrations
{
    public partial class CascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Distributions_Professors_ProfessorId",
                table: "Distributions");

            migrationBuilder.DropForeignKey(
                name: "FK_Distributions_Sections_SectionId",
                table: "Distributions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorHours_Professors_ProfessorId",
                table: "ProfessorHours");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorsDepartments_Departments_DepartmentName",
                table: "ProfessorsDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorsDepartments_Professors_ProfessorId",
                table: "ProfessorsDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Courses_CourseId",
                table: "Sections");

            migrationBuilder.AddForeignKey(
                name: "FK_Distributions_Professors_ProfessorId",
                table: "Distributions",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Distributions_Sections_SectionId",
                table: "Distributions",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorHours_Professors_ProfessorId",
                table: "ProfessorHours",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorsDepartments_Departments_DepartmentName",
                table: "ProfessorsDepartments",
                column: "DepartmentName",
                principalTable: "Departments",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorsDepartments_Professors_ProfessorId",
                table: "ProfessorsDepartments",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Courses_CourseId",
                table: "Sections",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Distributions_Professors_ProfessorId",
                table: "Distributions");

            migrationBuilder.DropForeignKey(
                name: "FK_Distributions_Sections_SectionId",
                table: "Distributions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorHours_Professors_ProfessorId",
                table: "ProfessorHours");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorsDepartments_Departments_DepartmentName",
                table: "ProfessorsDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorsDepartments_Professors_ProfessorId",
                table: "ProfessorsDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Courses_CourseId",
                table: "Sections");

            migrationBuilder.AddForeignKey(
                name: "FK_Distributions_Professors_ProfessorId",
                table: "Distributions",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Distributions_Sections_SectionId",
                table: "Distributions",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorHours_Professors_ProfessorId",
                table: "ProfessorHours",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorsDepartments_Departments_DepartmentName",
                table: "ProfessorsDepartments",
                column: "DepartmentName",
                principalTable: "Departments",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorsDepartments_Professors_ProfessorId",
                table: "ProfessorsDepartments",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Courses_CourseId",
                table: "Sections",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
