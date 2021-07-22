using Microsoft.EntityFrameworkCore.Migrations;

namespace kisko.Migrations
{
    public partial class _10th : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lol",
                table: "Project");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "lol",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
