using Microsoft.EntityFrameworkCore.Migrations;

namespace kisko.Migrations
{
    public partial class _11th : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SecondStudent",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SecondStudentId",
                table: "Project",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecondStudent",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "SecondStudentId",
                table: "Project");
        }
    }
}
