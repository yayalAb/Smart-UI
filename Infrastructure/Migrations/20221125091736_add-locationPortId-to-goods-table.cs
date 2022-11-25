using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class addlocationPortIdtogoodstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationPortId",
                table: "Goods",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Goods_LocationPortId",
                table: "Goods",
                column: "LocationPortId");

            migrationBuilder.AddForeignKey(
                name: "FK_Goods_Ports_LocationPortId",
                table: "Goods",
                column: "LocationPortId",
                principalTable: "Ports",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goods_Ports_LocationPortId",
                table: "Goods");

            migrationBuilder.DropIndex(
                name: "IX_Goods_LocationPortId",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "LocationPortId",
                table: "Goods");
        }
    }
}
