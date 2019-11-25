using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VehiclesDashboard.VehicleConnection.API.Migrations
{
    public partial class InitialVehivleHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerVehicleHistory",
                columns: table => new
                {
                    HistoryId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(nullable: false),
                    VehicleId = table.Column<string>(nullable: false),
                    RegNo = table.Column<string>(nullable: false),
                    ConnectionStatus = table.Column<bool>(nullable: false),
                    StatusModificationTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerVehicleHistory", x => x.HistoryId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerVehicleHistory");
        }
    }
}
