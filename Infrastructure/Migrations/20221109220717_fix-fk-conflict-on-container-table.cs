using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class fixfkconflictoncontainertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Containers_ContainerId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_ContainerId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "ContainerId",
                table: "Addresses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContainerId",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ContainerId",
                table: "Addresses",
                column: "ContainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Containers_ContainerId",
                table: "Addresses",
                column: "ContainerId",
                principalTable: "Containers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
