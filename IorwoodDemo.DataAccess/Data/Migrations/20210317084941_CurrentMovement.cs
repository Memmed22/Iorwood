using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IorwoodDemo.Data.Migrations
{
    public partial class CurrentMovement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrentMovement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    IsInflow = table.Column<bool>(nullable: false),
                    MovementType = table.Column<string>(nullable: true),
                    Sum = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Cleared = table.Column<bool>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentMovement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrentMovement_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrentMovement_UserId",
                table: "CurrentMovement",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrentMovement");
        }
    }
}
