using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VehiclesDashboard.VehicleServices.API.Migrations
{
    public partial class AddCustomerVehiclesData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CustomerVehicles",
                columns: new[] { "CustomerId", "VehicleId", "IsConnectedStatus", "LastStatusModificationTime", "RegNo" },
                values: new object[] { 1, "YS2R4X20005399401", true, new DateTime(2019, 11, 22, 16, 22, 21, 852, DateTimeKind.Local), "ABC123" });

            migrationBuilder.InsertData(
                table: "CustomerVehicles",
                columns: new[] { "CustomerId", "VehicleId", "IsConnectedStatus", "LastStatusModificationTime", "RegNo" },
                values: new object[] { 1, "VLUR4X20009093588", true, new DateTime(2019, 11, 22, 16, 22, 21, 853, DateTimeKind.Local), "DEF456" });

            migrationBuilder.InsertData(
                table: "CustomerVehicles",
                columns: new[] { "CustomerId", "VehicleId", "IsConnectedStatus", "LastStatusModificationTime", "RegNo" },
                values: new object[] { 1, "VLUR4X20009048066", true, new DateTime(2019, 11, 22, 16, 22, 21, 853, DateTimeKind.Local), "GHI789" });

            migrationBuilder.InsertData(
                table: "CustomerVehicles",
                columns: new[] { "CustomerId", "VehicleId", "IsConnectedStatus", "LastStatusModificationTime", "RegNo" },
                values: new object[] { 2, "YS2R4X20005388011", true, new DateTime(2019, 11, 22, 16, 22, 21, 853, DateTimeKind.Local), "JKL012" });

            migrationBuilder.InsertData(
                table: "CustomerVehicles",
                columns: new[] { "CustomerId", "VehicleId", "IsConnectedStatus", "LastStatusModificationTime", "RegNo" },
                values: new object[] { 2, "YS2R4X20005387949", true, new DateTime(2019, 11, 22, 16, 22, 21, 853, DateTimeKind.Local), "MNO345" });

            migrationBuilder.InsertData(
                table: "CustomerVehicles",
                columns: new[] { "CustomerId", "VehicleId", "IsConnectedStatus", "LastStatusModificationTime", "RegNo" },
                values: new object[] { 3, "VLUR4X20009048066", true, new DateTime(2019, 11, 22, 16, 22, 21, 853, DateTimeKind.Local), "PQR678" });

            migrationBuilder.InsertData(
                table: "CustomerVehicles",
                columns: new[] { "CustomerId", "VehicleId", "IsConnectedStatus", "LastStatusModificationTime", "RegNo" },
                values: new object[] { 3, "YS2R4X20005387055", true, new DateTime(2019, 11, 22, 16, 22, 21, 853, DateTimeKind.Local), "STU901" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CustomerVehicles",
                keyColumns: new[] { "CustomerId", "VehicleId" },
                keyValues: new object[] { 1, "VLUR4X20009048066" });

            migrationBuilder.DeleteData(
                table: "CustomerVehicles",
                keyColumns: new[] { "CustomerId", "VehicleId" },
                keyValues: new object[] { 1, "VLUR4X20009093588" });

            migrationBuilder.DeleteData(
                table: "CustomerVehicles",
                keyColumns: new[] { "CustomerId", "VehicleId" },
                keyValues: new object[] { 1, "YS2R4X20005399401" });

            migrationBuilder.DeleteData(
                table: "CustomerVehicles",
                keyColumns: new[] { "CustomerId", "VehicleId" },
                keyValues: new object[] { 2, "YS2R4X20005387949" });

            migrationBuilder.DeleteData(
                table: "CustomerVehicles",
                keyColumns: new[] { "CustomerId", "VehicleId" },
                keyValues: new object[] { 2, "YS2R4X20005388011" });

            migrationBuilder.DeleteData(
                table: "CustomerVehicles",
                keyColumns: new[] { "CustomerId", "VehicleId" },
                keyValues: new object[] { 3, "VLUR4X20009048066" });

            migrationBuilder.DeleteData(
                table: "CustomerVehicles",
                keyColumns: new[] { "CustomerId", "VehicleId" },
                keyValues: new object[] { 3, "YS2R4X20005387055" });
        }
    }
}
