using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Factory.Migrations
{
    public partial class AddMachineProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastInspection",
                table: "Machines",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Version",
                table: "Machines",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LicenseRenewBy",
                table: "Engineers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastInspection",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "LicenseRenewBy",
                table: "Engineers");
        }
    }
}
