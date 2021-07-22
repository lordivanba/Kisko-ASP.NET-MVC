using Microsoft.EntityFrameworkCore.Migrations;

namespace kisko.Migrations
{
    public partial class _5thMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Student",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Project",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Student",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Project");
        }
    }
}
