using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class goodrelationchangetocascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goods_Containers_ContainerId",
                table: "Goods");

            migrationBuilder.DropForeignKey(
                name: "FK_Goods_Operations_OperationId",
                table: "Goods");

            migrationBuilder.AddForeignKey(
                name: "FK_Goods_Containers_ContainerId",
                table: "Goods",
                column: "ContainerId",
                principalTable: "Containers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Goods_Operations_OperationId",
                table: "Goods",
                column: "OperationId",
                principalTable: "Operations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goods_Containers_ContainerId",
                table: "Goods");

            migrationBuilder.DropForeignKey(
                name: "FK_Goods_Operations_OperationId",
                table: "Goods");

            migrationBuilder.AddForeignKey(
                name: "FK_Goods_Containers_ContainerId",
                table: "Goods",
                column: "ContainerId",
                principalTable: "Containers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Goods_Operations_OperationId",
                table: "Goods",
                column: "OperationId",
                principalTable: "Operations",
                principalColumn: "Id");
        }
    }
}
