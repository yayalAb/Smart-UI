using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class updateuserGrouptablewithroles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserRoles_AspNetUsers_ApplicationUserId",
                table: "AppUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AppUserRoles_ApplicationUserId",
                table: "AppUserRoles");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "AppUserRoles");

            migrationBuilder.AddColumn<string>(
                name: "Responsiblity",
                table: "UserGroups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserGroupId",
                table: "AppUserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AppUserRoles_UserGroupId",
                table: "AppUserRoles",
                column: "UserGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserRoles_UserGroups_UserGroupId",
                table: "AppUserRoles",
                column: "UserGroupId",
                principalTable: "UserGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserRoles_UserGroups_UserGroupId",
                table: "AppUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AppUserRoles_UserGroupId",
                table: "AppUserRoles");

            migrationBuilder.DropColumn(
                name: "Responsiblity",
                table: "UserGroups");

            migrationBuilder.DropColumn(
                name: "UserGroupId",
                table: "AppUserRoles");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "AppUserRoles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserRoles_ApplicationUserId",
                table: "AppUserRoles",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserRoles_AspNetUsers_ApplicationUserId",
                table: "AppUserRoles",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
