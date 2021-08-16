using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IorwoodDemo.Data.Migrations
{
    public partial class addDatesToStockTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CareateDateOn",
                table: "Stock",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateOn",
                table: "Stock",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CareateDateOn",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "UpdateOn",
                table: "Stock");
        }
    }
}
