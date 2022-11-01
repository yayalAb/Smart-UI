using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class addedcanviewpropertytouserRoletable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_AspNetUsers_ApplicationUserId",
                table: "UserRoles");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "UserRoles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "canView",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_AspNetUsers_ApplicationUserId",
                table: "UserRoles",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_AspNetUsers_ApplicationUserId",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "canView",
                table: "UserRoles");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "UserRoles",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_AspNetUsers_ApplicationUserId",
                table: "UserRoles",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
