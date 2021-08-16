using Microsoft.EntityFrameworkCore.Migrations;

namespace IorwoodDemo.Data.Migrations
{
    public partial class addCorrectOrderDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_OrderHeader_OrderHeaderId",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "OrderHeadId",
                table: "OrderDetail");

            migrationBuilder.AlterColumn<int>(
                name: "OrderHeaderId",
                table: "OrderDetail",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_OrderHeader_OrderHeaderId",
                table: "OrderDetail",
                column: "OrderHeaderId",
                principalTable: "OrderHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_OrderHeader_OrderHeaderId",
                table: "OrderDetail");

            migrationBuilder.AlterColumn<int>(
                name: "OrderHeaderId",
                table: "OrderDetail",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "OrderHeadId",
                table: "OrderDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_OrderHeader_OrderHeaderId",
                table: "OrderDetail",
                column: "OrderHeaderId",
                principalTable: "OrderHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
