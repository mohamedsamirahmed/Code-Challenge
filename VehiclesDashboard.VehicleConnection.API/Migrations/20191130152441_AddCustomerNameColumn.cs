using Microsoft.EntityFrameworkCore.Migrations;

namespace VehiclesDashboard.VehicleConnection.API.Migrations
{
    public partial class AddCustomerNameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "CustomerVehicleHistory",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "CustomerVehicleHistory");
        }
    }
}
