using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoFixStationDatabaseImplement.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpareParts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FactoryNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    UMeasurement = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpareParts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoreKeepers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreKeepers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeOfWorks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hours = table.Column<int>(type: "int", nullable: false),
                    Mins = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeOfWorks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    DateBegin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRecords_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TOs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Sum = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateImplement = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOver = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TOs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TOs_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TOs_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeOfWorkId = table.Column<int>(type: "int", nullable: false),
                    WorkName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkTypes_TimeOfWorks_TimeOfWorkId",
                        column: x => x.TimeOfWorkId,
                        principalTable: "TimeOfWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Works",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreKeeperId = table.Column<int>(type: "int", nullable: false),
                    WorkTypeId = table.Column<int>(type: "int", nullable: false),
                    WorkName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WorkStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Works", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Works_StoreKeepers_StoreKeeperId",
                        column: x => x.StoreKeeperId,
                        principalTable: "StoreKeepers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Works_WorkTypes_WorkTypeId",
                        column: x => x.WorkTypeId,
                        principalTable: "WorkTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkType_SpareParts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SparePartId = table.Column<int>(type: "int", nullable: false),
                    WorkTypeId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkType_SpareParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkType_SpareParts_SpareParts_SparePartId",
                        column: x => x.SparePartId,
                        principalTable: "SpareParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkType_SpareParts_WorkTypes_WorkTypeId",
                        column: x => x.WorkTypeId,
                        principalTable: "WorkTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_ServiceRecords_CarId",
                table: "ServiceRecords",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_TO_Works_TOId",
                table: "TO_Works",
                column: "TOId");

            migrationBuilder.CreateIndex(
                name: "IX_TO_Works_WorkId",
                table: "TO_Works",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_TOs_CarId",
                table: "TOs",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_TOs_EmployeeId",
                table: "TOs",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Works_StoreKeeperId",
                table: "Works",
                column: "StoreKeeperId");

            migrationBuilder.CreateIndex(
                name: "IX_Works_WorkTypeId",
                table: "Works",
                column: "WorkTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkType_SpareParts_SparePartId",
                table: "WorkType_SpareParts",
                column: "SparePartId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkType_SpareParts_WorkTypeId",
                table: "WorkType_SpareParts",
                column: "WorkTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkTypes_TimeOfWorkId",
                table: "WorkTypes",
                column: "TimeOfWorkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceRecords");

            migrationBuilder.DropTable(
                name: "TO_Works");

            migrationBuilder.DropTable(
                name: "WorkType_SpareParts");

            migrationBuilder.DropTable(
                name: "TOs");

            migrationBuilder.DropTable(
                name: "Works");

            migrationBuilder.DropTable(
                name: "SpareParts");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "StoreKeepers");

            migrationBuilder.DropTable(
                name: "WorkTypes");

            migrationBuilder.DropTable(
                name: "TimeOfWorks");
        }
    }
}
