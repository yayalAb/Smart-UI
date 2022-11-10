using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class containerandporttableupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Containers_Addresses_AddressId",
                table: "Containers");

            migrationBuilder.DropIndex(
                name: "IX_Containers_AddressId",
                table: "Containers");

            migrationBuilder.DropIndex(
                name: "IX_Containers_ImageId",
                table: "Containers");

            migrationBuilder.DropIndex(
                name: "IX_BillOfLoadings_BillOfLoadingDocumentId",
                table: "BillOfLoadings");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Containers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Ports",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "Containers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BillOfLoadingDocumentId",
                table: "BillOfLoadings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ContainerId",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Containers_ImageId",
                table: "Containers",
                column: "ImageId",
                unique: true,
                filter: "[ImageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BillOfLoadings_BillOfLoadingDocumentId",
                table: "BillOfLoadings",
                column: "BillOfLoadingDocumentId",
                unique: true,
                filter: "[BillOfLoadingDocumentId] IS NOT NULL");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Containers_ContainerId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Containers_ImageId",
                table: "Containers");

            migrationBuilder.DropIndex(
                name: "IX_BillOfLoadings_BillOfLoadingDocumentId",
                table: "BillOfLoadings");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_ContainerId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "ContainerId",
                table: "Addresses");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Ports",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45);

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "Containers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Containers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "BillOfLoadingDocumentId",
                table: "BillOfLoadings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Containers_AddressId",
                table: "Containers",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Containers_ImageId",
                table: "Containers",
                column: "ImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillOfLoadings_BillOfLoadingDocumentId",
                table: "BillOfLoadings",
                column: "BillOfLoadingDocumentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Containers_Addresses_AddressId",
                table: "Containers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }
    }
}
