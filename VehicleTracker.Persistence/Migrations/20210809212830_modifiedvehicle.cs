using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleTracker.Persistence.Migrations
{
    public partial class modifiedvehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_VehicleOwner_OwnerId",
                table: "Vehicle");

            migrationBuilder.DropTable(
                name: "VehicleOwner");

            migrationBuilder.DropIndex(
                name: "IX_Vehicle_OwnerId",
                table: "Vehicle");

            migrationBuilder.RenameColumn(
                name: "RegistrationNumber",
                table: "Vehicle",
                newName: "DeviceNumber");

            migrationBuilder.AddColumn<string>(
                name: "OwnerIdentity_UserId",
                table: "Vehicle",
                type: "varchar(50)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerIdentity_UserId",
                table: "Vehicle");

            migrationBuilder.RenameColumn(
                name: "DeviceNumber",
                table: "Vehicle",
                newName: "RegistrationNumber");

            migrationBuilder.CreateTable(
                name: "VehicleOwner",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "varchar(150)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    OwnerIdentity_UserId = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleOwner", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_OwnerId",
                table: "Vehicle",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_VehicleOwner_OwnerId",
                table: "Vehicle",
                column: "OwnerId",
                principalTable: "VehicleOwner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
