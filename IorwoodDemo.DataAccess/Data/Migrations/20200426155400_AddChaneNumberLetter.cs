using Microsoft.EntityFrameworkCore.Migrations;

namespace IorwoodDemo.Data.Migrations
{
    public partial class AddChaneNumberLetter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "number",
                table: "Unit",
                newName: "Number");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Unit",
                newName: "number");
        }
    }
}
