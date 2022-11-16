using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class updateoperationtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_ShippingAgents_ShippingAgentId",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "SourceDocumentType",
                table: "Operations");

            migrationBuilder.AlterColumn<string>(
                name: "VoyageNumber",
                table: "Operations",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "TypeOfMerchandise",
                table: "Operations",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ShippingLine",
                table: "Operations",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "ShippingAgentId",
                table: "Operations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NotifyParty",
                table: "Operations",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "GoodsDescription",
                table: "Operations",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "DestinationType",
                table: "Operations",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "BillNumber",
                table: "Operations",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_ShippingAgents_ShippingAgentId",
                table: "Operations",
                column: "ShippingAgentId",
                principalTable: "ShippingAgents",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_ShippingAgents_ShippingAgentId",
                table: "Operations");

            migrationBuilder.UpdateData(
                table: "Operations",
                keyColumn: "VoyageNumber",
                keyValue: null,
                column: "VoyageNumber",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "VoyageNumber",
                table: "Operations",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Operations",
                keyColumn: "TypeOfMerchandise",
                keyValue: null,
                column: "TypeOfMerchandise",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "TypeOfMerchandise",
                table: "Operations",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Operations",
                keyColumn: "ShippingLine",
                keyValue: null,
                column: "ShippingLine",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ShippingLine",
                table: "Operations",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "ShippingAgentId",
                table: "Operations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Operations",
                keyColumn: "NotifyParty",
                keyValue: null,
                column: "NotifyParty",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "NotifyParty",
                table: "Operations",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Operations",
                keyColumn: "GoodsDescription",
                keyValue: null,
                column: "GoodsDescription",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "GoodsDescription",
                table: "Operations",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Operations",
                keyColumn: "DestinationType",
                keyValue: null,
                column: "DestinationType",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "DestinationType",
                table: "Operations",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Operations",
                keyColumn: "BillNumber",
                keyValue: null,
                column: "BillNumber",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "BillNumber",
                table: "Operations",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Operations",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SourceDocumentType",
                table: "Operations",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_ShippingAgents_ShippingAgentId",
                table: "Operations",
                column: "ShippingAgentId",
                principalTable: "ShippingAgents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
