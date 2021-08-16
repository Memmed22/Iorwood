using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IorwoodDemo.Data.Migrations
{
    public partial class AccountingBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "CurrentMovement");

            migrationBuilder.AddColumn<int>(
                name: "AccountingBookId",
                table: "CurrentMovement",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CurrentDate",
                table: "CurrentMovement",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "AccountingBook",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountingDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    InFlowSum = table.Column<double>(nullable: false),
                    OutFlowSum = table.Column<double>(nullable: false),
                    CashLeft = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountingBook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountingBook_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrentMovement_AccountingBookId",
                table: "CurrentMovement",
                column: "AccountingBookId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingBook_UserId",
                table: "AccountingBook",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentMovement_AccountingBook_AccountingBookId",
                table: "CurrentMovement",
                column: "AccountingBookId",
                principalTable: "AccountingBook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrentMovement_AccountingBook_AccountingBookId",
                table: "CurrentMovement");

            migrationBuilder.DropTable(
                name: "AccountingBook");

            migrationBuilder.DropIndex(
                name: "IX_CurrentMovement_AccountingBookId",
                table: "CurrentMovement");

            migrationBuilder.DropColumn(
                name: "AccountingBookId",
                table: "CurrentMovement");

            migrationBuilder.DropColumn(
                name: "CurrentDate",
                table: "CurrentMovement");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "CurrentMovement",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
