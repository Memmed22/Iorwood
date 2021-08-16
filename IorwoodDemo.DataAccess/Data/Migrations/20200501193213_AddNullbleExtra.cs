using Microsoft.EntityFrameworkCore.Migrations;

namespace IorwoodDemo.Data.Migrations
{
    public partial class AddNullbleExtra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Extra_ExtraId",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "ExtraId",
                table: "Product",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Extra_ExtraId",
                table: "Product",
                column: "ExtraId",
                principalTable: "Extra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Extra_ExtraId",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "ExtraId",
                table: "Product",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Extra_ExtraId",
                table: "Product",
                column: "ExtraId",
                principalTable: "Extra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
