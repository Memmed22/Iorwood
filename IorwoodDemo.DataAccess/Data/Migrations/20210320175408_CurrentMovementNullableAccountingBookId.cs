using Microsoft.EntityFrameworkCore.Migrations;

namespace IorwoodDemo.Data.Migrations
{
    public partial class CurrentMovementNullableAccountingBookId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrentMovement_AccountingBook_AccountingBookId",
                table: "CurrentMovement");

            migrationBuilder.AlterColumn<int>(
                name: "AccountingBookId",
                table: "CurrentMovement",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentMovement_AccountingBook_AccountingBookId",
                table: "CurrentMovement",
                column: "AccountingBookId",
                principalTable: "AccountingBook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrentMovement_AccountingBook_AccountingBookId",
                table: "CurrentMovement");

            migrationBuilder.AlterColumn<int>(
                name: "AccountingBookId",
                table: "CurrentMovement",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentMovement_AccountingBook_AccountingBookId",
                table: "CurrentMovement",
                column: "AccountingBookId",
                principalTable: "AccountingBook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
