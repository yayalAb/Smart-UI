using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class namingfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "title",
                table: "AppUserRoles",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "page",
                table: "AppUserRoles",
                newName: "Page");

            migrationBuilder.RenameColumn(
                name: "canViewDetail",
                table: "AppUserRoles",
                newName: "CanViewDetail");

            migrationBuilder.RenameColumn(
                name: "canView",
                table: "AppUserRoles",
                newName: "CanView");

            migrationBuilder.RenameColumn(
                name: "canUpdate",
                table: "AppUserRoles",
                newName: "CanUpdate");

            migrationBuilder.RenameColumn(
                name: "canDelete",
                table: "AppUserRoles",
                newName: "CanDelete");

            migrationBuilder.RenameColumn(
                name: "canAdd",
                table: "AppUserRoles",
                newName: "CanAdd");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "AppUserRoles",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Page",
                table: "AppUserRoles",
                newName: "page");

            migrationBuilder.RenameColumn(
                name: "CanViewDetail",
                table: "AppUserRoles",
                newName: "canViewDetail");

            migrationBuilder.RenameColumn(
                name: "CanView",
                table: "AppUserRoles",
                newName: "canView");

            migrationBuilder.RenameColumn(
                name: "CanUpdate",
                table: "AppUserRoles",
                newName: "canUpdate");

            migrationBuilder.RenameColumn(
                name: "CanDelete",
                table: "AppUserRoles",
                newName: "canDelete");

            migrationBuilder.RenameColumn(
                name: "CanAdd",
                table: "AppUserRoles",
                newName: "canAdd");
        }
    }
}
