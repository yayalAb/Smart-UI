using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class componentModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_feildsModel_Components_ComponentId",
                table: "feildsModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_feildsModel",
                table: "feildsModel");

            migrationBuilder.RenameTable(
                name: "feildsModel",
                newName: "feilds");

            migrationBuilder.RenameIndex(
                name: "IX_feildsModel_ComponentId",
                table: "feilds",
                newName: "IX_feilds_ComponentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_feilds",
                table: "feilds",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_feilds_Components_ComponentId",
                table: "feilds",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_feilds_Components_ComponentId",
                table: "feilds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_feilds",
                table: "feilds");

            migrationBuilder.RenameTable(
                name: "feilds",
                newName: "feildsModel");

            migrationBuilder.RenameIndex(
                name: "IX_feilds_ComponentId",
                table: "feildsModel",
                newName: "IX_feildsModel_ComponentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_feildsModel",
                table: "feildsModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_feildsModel_Components_ComponentId",
                table: "feildsModel",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
