using Microsoft.EntityFrameworkCore.Migrations;

namespace IorwoodDemo.Data.Migrations
{
    public partial class updateStockTypetToDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "StockQuantity",
                table: "Product",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "StockMinQuantity",
                table: "Product",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StockQuantity",
                table: "Product",
                type: "int",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "StockMinQuantity",
                table: "Product",
                type: "int",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
