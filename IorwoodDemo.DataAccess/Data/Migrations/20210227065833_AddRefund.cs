using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IorwoodDemo.Data.Migrations
{
    public partial class AddRefund : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefundHeader",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderHeaderId = table.Column<int>(nullable: false),
                    TotalAmount = table.Column<double>(nullable: false),
                    RefundDate = table.Column<DateTime>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefundHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefundHeader_OrderHeader_OrderHeaderId",
                        column: x => x.OrderHeaderId,
                        principalTable: "OrderHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefundDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RefundHeaderId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefundDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefundDetail_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RefundDetail_RefundHeader_RefundHeaderId",
                        column: x => x.RefundHeaderId,
                        principalTable: "RefundHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefundDetail_ProductId",
                table: "RefundDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RefundDetail_RefundHeaderId",
                table: "RefundDetail",
                column: "RefundHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_RefundHeader_OrderHeaderId",
                table: "RefundHeader",
                column: "OrderHeaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefundDetail");

            migrationBuilder.DropTable(
                name: "RefundHeader");
        }
    }
}
