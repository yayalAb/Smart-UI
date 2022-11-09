using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class changebillofloadingandoperationtablerelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_Bill of Loading_container1",
                table: "BillOfLoadings");

            migrationBuilder.DropForeignKey(
                name: "fk_Bill of Loading_port1",
                table: "BillOfLoadings");

            migrationBuilder.DropForeignKey(
                name: "FK_TerminalPortFees_Operations_operation_Id",
                table: "TerminalPortFees");

            migrationBuilder.DropTable(
                name: "ECDDocuments");

            migrationBuilder.DropIndex(
                name: "IX_TerminalPortFees_operation_Id",
                table: "TerminalPortFees");

            migrationBuilder.DropIndex(
                name: "IX_ShippingAgentFees_OperationId",
                table: "ShippingAgentFees");

            migrationBuilder.DropIndex(
                name: "IX_Documentations_OperationId",
                table: "Documentations");

            migrationBuilder.DropColumn(
                name: "PortOfLoading",
                table: "BillOfLoadings");

            migrationBuilder.DropColumn(
                name: "ShippingAgent",
                table: "BillOfLoadings");

            migrationBuilder.DropColumn(
                name: "TruckNumber",
                table: "BillOfLoadings");

            migrationBuilder.RenameColumn(
                name: "PortId",
                table: "BillOfLoadings",
                newName: "ShippingAgentId");

            migrationBuilder.RenameIndex(
                name: "IX_BillOfLoadings_PortId",
                table: "BillOfLoadings",
                newName: "IX_BillOfLoadings_ShippingAgentId");

            migrationBuilder.AddColumn<int>(
                name: "ECDDocumentId",
                table: "Operations",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VoyageNumber",
                table: "BillOfLoadings",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TypeOfMerchandise",
                table: "BillOfLoadings",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShippingLine",
                table: "BillOfLoadings",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45,
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Quantity",
                table: "BillOfLoadings",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NotifyParty",
                table: "BillOfLoadings",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GoodsDescription",
                table: "BillOfLoadings",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DestinationType",
                table: "BillOfLoadings",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerName",
                table: "BillOfLoadings",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BillNumber",
                table: "BillOfLoadings",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BillOfLoadingDocumentId",
                table: "BillOfLoadings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PortOfLoadingId",
                table: "BillOfLoadings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TerminalPortFees_operation_Id",
                table: "TerminalPortFees",
                column: "operation_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAgentFees_OperationId",
                table: "ShippingAgentFees",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_ECDDocumentId",
                table: "Operations",
                column: "ECDDocumentId",
                unique: true,
                filter: "[ECDDocumentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Documentations_OperationId",
                table: "Documentations",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_BillOfLoadings_BillOfLoadingDocumentId",
                table: "BillOfLoadings",
                column: "BillOfLoadingDocumentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillOfLoadings_PortOfLoadingId",
                table: "BillOfLoadings",
                column: "PortOfLoadingId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillOfLoadings_Containers_ContainerId",
                table: "BillOfLoadings",
                column: "ContainerId",
                principalTable: "Containers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BillOfLoadings_Documents_BillOfLoadingDocumentId",
                table: "BillOfLoadings",
                column: "BillOfLoadingDocumentId",
                principalTable: "Documents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BillOfLoadings_Ports_PortOfLoadingId",
                table: "BillOfLoadings",
                column: "PortOfLoadingId",
                principalTable: "Ports",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BillOfLoadings_ShippingAgents_ShippingAgentId",
                table: "BillOfLoadings",
                column: "ShippingAgentId",
                principalTable: "ShippingAgents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_ECD Document_operation1",
                table: "Operations",
                column: "ECDDocumentId",
                principalTable: "Documents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TerminalPortFees_Operations_operation_Id",
                table: "TerminalPortFees",
                column: "operation_Id",
                principalTable: "Operations",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillOfLoadings_Containers_ContainerId",
                table: "BillOfLoadings");

            migrationBuilder.DropForeignKey(
                name: "FK_BillOfLoadings_Documents_BillOfLoadingDocumentId",
                table: "BillOfLoadings");

            migrationBuilder.DropForeignKey(
                name: "FK_BillOfLoadings_Ports_PortOfLoadingId",
                table: "BillOfLoadings");

            migrationBuilder.DropForeignKey(
                name: "FK_BillOfLoadings_ShippingAgents_ShippingAgentId",
                table: "BillOfLoadings");

            migrationBuilder.DropForeignKey(
                name: "fk_ECD Document_operation1",
                table: "Operations");

            migrationBuilder.DropForeignKey(
                name: "FK_TerminalPortFees_Operations_operation_Id",
                table: "TerminalPortFees");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_TerminalPortFees_operation_Id",
                table: "TerminalPortFees");

            migrationBuilder.DropIndex(
                name: "IX_ShippingAgentFees_OperationId",
                table: "ShippingAgentFees");

            migrationBuilder.DropIndex(
                name: "IX_Operations_ECDDocumentId",
                table: "Operations");

            migrationBuilder.DropIndex(
                name: "IX_Documentations_OperationId",
                table: "Documentations");

            migrationBuilder.DropIndex(
                name: "IX_BillOfLoadings_BillOfLoadingDocumentId",
                table: "BillOfLoadings");

            migrationBuilder.DropIndex(
                name: "IX_BillOfLoadings_PortOfLoadingId",
                table: "BillOfLoadings");

            migrationBuilder.DropColumn(
                name: "ECDDocumentId",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "BillOfLoadingDocumentId",
                table: "BillOfLoadings");

            migrationBuilder.DropColumn(
                name: "PortOfLoadingId",
                table: "BillOfLoadings");

            migrationBuilder.RenameColumn(
                name: "ShippingAgentId",
                table: "BillOfLoadings",
                newName: "PortId");

            migrationBuilder.RenameIndex(
                name: "IX_BillOfLoadings_ShippingAgentId",
                table: "BillOfLoadings",
                newName: "IX_BillOfLoadings_PortId");

            migrationBuilder.AlterColumn<string>(
                name: "VoyageNumber",
                table: "BillOfLoadings",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45);

            migrationBuilder.AlterColumn<string>(
                name: "TypeOfMerchandise",
                table: "BillOfLoadings",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45);

            migrationBuilder.AlterColumn<string>(
                name: "ShippingLine",
                table: "BillOfLoadings",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45);

            migrationBuilder.AlterColumn<float>(
                name: "Quantity",
                table: "BillOfLoadings",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<string>(
                name: "NotifyParty",
                table: "BillOfLoadings",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45);

            migrationBuilder.AlterColumn<string>(
                name: "GoodsDescription",
                table: "BillOfLoadings",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "DestinationType",
                table: "BillOfLoadings",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerName",
                table: "BillOfLoadings",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45);

            migrationBuilder.AlterColumn<string>(
                name: "BillNumber",
                table: "BillOfLoadings",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45);

            migrationBuilder.AddColumn<int>(
                name: "PortOfLoading",
                table: "BillOfLoadings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAgent",
                table: "BillOfLoadings",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TruckNumber",
                table: "BillOfLoadings",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ECDDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Document = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ECDDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "fk_ECD Document_operation1",
                        column: x => x.OperationId,
                        principalTable: "Operations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TerminalPortFees_operation_Id",
                table: "TerminalPortFees",
                column: "operation_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAgentFees_OperationId",
                table: "ShippingAgentFees",
                column: "OperationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documentations_OperationId",
                table: "Documentations",
                column: "OperationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ECDDocuments_OperationId",
                table: "ECDDocuments",
                column: "OperationId");

            migrationBuilder.AddForeignKey(
                name: "fk_Bill of Loading_container1",
                table: "BillOfLoadings",
                column: "ContainerId",
                principalTable: "Containers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "fk_Bill of Loading_port1",
                table: "BillOfLoadings",
                column: "PortId",
                principalTable: "Ports",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TerminalPortFees_Operations_operation_Id",
                table: "TerminalPortFees",
                column: "operation_Id",
                principalTable: "Operations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
