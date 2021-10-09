using Microsoft.EntityFrameworkCore.Migrations;

namespace Courses_Distribution_System.Migrations
{
    public partial class DropDistributions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Distributions");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Sections",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfessorId",
                table: "Sections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sections_ProfessorId",
                table: "Sections",
                column: "ProfessorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Professors_ProfessorId",
                table: "Sections",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Professors_ProfessorId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Sections_ProfessorId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "ProfessorId",
                table: "Sections");

            migrationBuilder.CreateTable(
                name: "Distributions",
                columns: table => new
                {
                    SectionId = table.Column<int>(type: "int", nullable: false),
                    ProfessorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distributions", x => new { x.SectionId, x.ProfessorId });
                    table.ForeignKey(
                        name: "FK_Distributions_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Distributions_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Distributions_ProfessorId",
                table: "Distributions",
                column: "ProfessorId");
        }
    }
}
