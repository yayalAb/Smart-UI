using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class makeforiegnkeyesnullableinoperation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Ports_PortOfLoadingId",
                table: "Operations");

            migrationBuilder.AlterColumn<int>(
                name: "PortOfLoadingId",
                table: "Operations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Ports_PortOfLoadingId",
                table: "Operations",
                column: "PortOfLoadingId",
                principalTable: "Ports",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Ports_PortOfLoadingId",
                table: "Operations");

            migrationBuilder.AlterColumn<int>(
                name: "PortOfLoadingId",
                table: "Operations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Ports_PortOfLoadingId",
                table: "Operations",
                column: "PortOfLoadingId",
                principalTable: "Ports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
