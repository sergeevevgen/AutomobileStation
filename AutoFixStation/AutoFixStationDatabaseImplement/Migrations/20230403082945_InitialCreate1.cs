using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoFixStationDatabaseImplement.Migrations
{
    public partial class InitialCreate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TO_Works");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Works",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TOId",
                table: "Works",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "WorkBegin",
                table: "Works",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Works_TOId",
                table: "Works",
                column: "TOId");

            migrationBuilder.AddForeignKey(
                name: "FK_Works_TOs_TOId",
                table: "Works",
                column: "TOId",
                principalTable: "TOs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Works_TOs_TOId",
                table: "Works");

            migrationBuilder.DropIndex(
                name: "IX_Works_TOId",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "TOId",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "WorkBegin",
                table: "Works");

            migrationBuilder.CreateTable(
                name: "TO_Works",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TOId = table.Column<int>(type: "int", nullable: false),
                    WorkId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TO_Works", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TO_Works_TOs_TOId",
                        column: x => x.TOId,
                        principalTable: "TOs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TO_Works_Works_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TO_Works_TOId",
                table: "TO_Works",
                column: "TOId");

            migrationBuilder.CreateIndex(
                name: "IX_TO_Works_WorkId",
                table: "TO_Works",
                column: "WorkId");
        }
    }
}
