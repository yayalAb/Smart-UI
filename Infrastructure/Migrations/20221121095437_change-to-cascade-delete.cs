using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class changetocascadedelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Operations_OperationId",
                table: "Payments");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Operations_OperationId",
                table: "Payments",
                column: "OperationId",
                principalTable: "Operations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Operations_OperationId",
                table: "Payments");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Operations_OperationId",
                table: "Payments",
                column: "OperationId",
                principalTable: "Operations",
                principalColumn: "Id");
        }
    }
}
