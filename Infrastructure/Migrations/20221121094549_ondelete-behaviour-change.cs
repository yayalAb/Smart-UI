using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class ondeletebehaviourchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserRoles_UserGroups_UserGroupId",
                table: "AppUserRoles");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserRoles_UserGroups_UserGroupId",
                table: "AppUserRoles",
                column: "UserGroupId",
                principalTable: "UserGroups",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserRoles_UserGroups_UserGroupId",
                table: "AppUserRoles");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserRoles_UserGroups_UserGroupId",
                table: "AppUserRoles",
                column: "UserGroupId",
                principalTable: "UserGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
