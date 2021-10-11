using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleTracker.Persistence.Migrations
{
    public partial class addedusertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "VehicleState",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    OwnerIdentity_UserId = table.Column<string>(type: "varchar(50)", nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleState_UserId",
                table: "VehicleState",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleState_User_UserId",
                table: "VehicleState",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleState_User_UserId",
                table: "VehicleState");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_VehicleState_UserId",
                table: "VehicleState");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "VehicleState");
        }
    }
}
