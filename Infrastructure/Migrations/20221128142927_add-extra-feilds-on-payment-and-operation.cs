using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class addextrafeildsonpaymentandoperation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DONumber",
                table: "Payments",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivalDate",
                table: "Operations",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConnaissementNumber",
                table: "Operations",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CountryOfOrigin",
                table: "Operations",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<float>(
                name: "REGTax",
                table: "Operations",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecepientName",
                table: "Operations",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "SDate",
                table: "Operations",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SNumber",
                table: "Operations",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "VesselName",
                table: "Operations",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DONumber",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ArrivalDate",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "ConnaissementNumber",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "CountryOfOrigin",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "REGTax",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "RecepientName",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "SDate",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "SNumber",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "VesselName",
                table: "Operations");
        }
    }
}
