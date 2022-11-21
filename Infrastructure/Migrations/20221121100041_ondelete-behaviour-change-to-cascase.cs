using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class ondeletebehaviourchangetocascase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserRoles_UserGroups_UserGroupId",
                table: "AppUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Addresses_AddressId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_ContactPeople_ContactPersonId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Containers_Operations_OperationId",
                table: "Containers");

            migrationBuilder.DropForeignKey(
                name: "FK_Documentations_Operations_OperationId",
                table: "Documentations");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationStatuses_Operations_OperationId",
                table: "OperationStatuses");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserRoles_UserGroups_UserGroupId",
                table: "AppUserRoles",
                column: "UserGroupId",
                principalTable: "UserGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Addresses_AddressId",
                table: "Companies",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_ContactPeople_ContactPersonId",
                table: "Companies",
                column: "ContactPersonId",
                principalTable: "ContactPeople",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Containers_Operations_OperationId",
                table: "Containers",
                column: "OperationId",
                principalTable: "Operations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Documentations_Operations_OperationId",
                table: "Documentations",
                column: "OperationId",
                principalTable: "Operations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationStatuses_Operations_OperationId",
                table: "OperationStatuses",
                column: "OperationId",
                principalTable: "Operations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserRoles_UserGroups_UserGroupId",
                table: "AppUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Addresses_AddressId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_ContactPeople_ContactPersonId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Containers_Operations_OperationId",
                table: "Containers");

            migrationBuilder.DropForeignKey(
                name: "FK_Documentations_Operations_OperationId",
                table: "Documentations");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationStatuses_Operations_OperationId",
                table: "OperationStatuses");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserRoles_UserGroups_UserGroupId",
                table: "AppUserRoles",
                column: "UserGroupId",
                principalTable: "UserGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Addresses_AddressId",
                table: "Companies",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_ContactPeople_ContactPersonId",
                table: "Companies",
                column: "ContactPersonId",
                principalTable: "ContactPeople",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Containers_Operations_OperationId",
                table: "Containers",
                column: "OperationId",
                principalTable: "Operations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documentations_Operations_OperationId",
                table: "Documentations",
                column: "OperationId",
                principalTable: "Operations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationStatuses_Operations_OperationId",
                table: "OperationStatuses",
                column: "OperationId",
                principalTable: "Operations",
                principalColumn: "Id");
        }
    }
}
