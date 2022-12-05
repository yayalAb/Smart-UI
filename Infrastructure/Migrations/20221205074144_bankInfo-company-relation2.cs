using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class bankInfocompanyrelation2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankInformation_Companies_CompanyId1",
                table: "BankInformation");

            migrationBuilder.DropIndex(
                name: "IX_BankInformation_CompanyId1",
                table: "BankInformation");

            migrationBuilder.DropColumn(
                name: "CompanyId1",
                table: "BankInformation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId1",
                table: "BankInformation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BankInformation_CompanyId1",
                table: "BankInformation",
                column: "CompanyId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BankInformation_Companies_CompanyId1",
                table: "BankInformation",
                column: "CompanyId1",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
