using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class addfeildsandtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TransportationMethod",
                table: "TruckAssignments",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "FinalDestination",
                table: "Operations",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Localization",
                table: "Operations",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PINumber",
                table: "Operations",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<float>(
                name: "CBM",
                table: "Goods",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "Goods",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<float>(
                name: "UnitPrice",
                table: "Goods",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fright",
                table: "Documentations",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsPartialShipmentAllowed",
                table: "Documentations",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentTerm",
                table: "Documentations",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PurchaseOrderNumber",
                table: "Documentations",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "ContactPeople",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "ContactPeople",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TinNumber",
                table: "ContactPeople",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BankInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccountHolderName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BankName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SwiftCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BankAddress = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CompanyId1 = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastModified = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankInformation_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankInformation_Companies_CompanyId1",
                        column: x => x.CompanyId1,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BankInformation_CompanyId",
                table: "BankInformation",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BankInformation_CompanyId1",
                table: "BankInformation",
                column: "CompanyId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankInformation");

            migrationBuilder.DropColumn(
                name: "TransportationMethod",
                table: "TruckAssignments");

            migrationBuilder.DropColumn(
                name: "FinalDestination",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "Localization",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "PINumber",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "CBM",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "Fright",
                table: "Documentations");

            migrationBuilder.DropColumn(
                name: "IsPartialShipmentAllowed",
                table: "Documentations");

            migrationBuilder.DropColumn(
                name: "PaymentTerm",
                table: "Documentations");

            migrationBuilder.DropColumn(
                name: "PurchaseOrderNumber",
                table: "Documentations");

            migrationBuilder.DropColumn(
                name: "City",
                table: "ContactPeople");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "ContactPeople");

            migrationBuilder.DropColumn(
                name: "TinNumber",
                table: "ContactPeople");
        }
    }
}
