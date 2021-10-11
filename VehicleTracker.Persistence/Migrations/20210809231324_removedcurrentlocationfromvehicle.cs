using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleTracker.Persistence.Migrations
{
    public partial class removedcurrentlocationfromvehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentLocation_Latitude",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "CurrentLocation_Longitude",
                table: "Vehicle");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrentLocation_Latitude",
                table: "Vehicle",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentLocation_Longitude",
                table: "Vehicle",
                type: "varchar(50)",
                nullable: true);
        }
    }
}
