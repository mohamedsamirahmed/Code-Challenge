using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VehiclesDashboard.VehicleServices.API.Migrations
{
    public partial class InitialVechileServiceContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    City = table.Column<string>(maxLength: 25, nullable: true),
                    PostalCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    VIN = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.VIN);
                });

            migrationBuilder.CreateTable(
                name: "CustomerVehicles",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false),
                    VehicleId = table.Column<string>(nullable: false),
                    RegNo = table.Column<string>(nullable: false),
                    IsConnectedStatus = table.Column<bool>(nullable: false),
                    LastStatusModificationTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerVehicles", x => new { x.CustomerId, x.VehicleId });
                    table.ForeignKey(
                        name: "FK_CustomerVehicles_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerVehicles_Vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicle",
                        principalColumn: "VIN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerId", "Address", "City", "Name", "PostalCode" },
                values: new object[] { 1, "Cementvägen 8", "Södertälje", "Kalles Grustransporter AB", "111 11" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerId", "Address", "City", "Name", "PostalCode" },
                values: new object[] { 2, "Balkvägen 12", "Stockholm", "Johans Bulk AB", "222 22" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerId", "Address", "City", "Name", "PostalCode" },
                values: new object[] { 3, "Budgetvägen 1", "Uppsala", "Haralds Värdetransporter AB", "333 33" });

            migrationBuilder.InsertData(
                table: "Vehicle",
                columns: new[] { "VIN", "IsDeleted" },
                values: new object[] { "YS2R4X20005399401", null });

            migrationBuilder.InsertData(
                table: "Vehicle",
                columns: new[] { "VIN", "IsDeleted" },
                values: new object[] { "VLUR4X20009093588", null });

            migrationBuilder.InsertData(
                table: "Vehicle",
                columns: new[] { "VIN", "IsDeleted" },
                values: new object[] { "VLUR4X20009048066", null });

            migrationBuilder.InsertData(
                table: "Vehicle",
                columns: new[] { "VIN", "IsDeleted" },
                values: new object[] { "YS2R4X20005388011", null });

            migrationBuilder.InsertData(
                table: "Vehicle",
                columns: new[] { "VIN", "IsDeleted" },
                values: new object[] { "YS2R4X20005387949", null });

            migrationBuilder.InsertData(
                table: "Vehicle",
                columns: new[] { "VIN", "IsDeleted" },
                values: new object[] { "YS2R4X20005387055", null });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVehicles_VehicleId",
                table: "CustomerVehicles",
                column: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerVehicles");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Vehicle");
        }
    }
}
