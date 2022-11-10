using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class changetablerelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillOfLoadings_Documents_BillOfLoadingDocumentId",
                table: "BillOfLoadings");

            migrationBuilder.DropForeignKey(
                name: "FK_BillOfLoadings_ShippingAgents_ShippingAgentId",
                table: "BillOfLoadings");

            migrationBuilder.DropForeignKey(
                name: "FK_Containers_Images_ImageId",
                table: "Containers");

            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Images_ImageId",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "fk_good_container1",
                table: "Goods");

            migrationBuilder.DropForeignKey(
                name: "fk_ECD Document_operation1",
                table: "Operations");

            migrationBuilder.DropForeignKey(
                name: "fk_operation_Bill of Loading1",
                table: "Operations");

            migrationBuilder.DropForeignKey(
                name: "fk_operation_company1",
                table: "Operations");

            migrationBuilder.DropForeignKey(
                name: "fk_operation_driver1",
                table: "Operations");

            migrationBuilder.DropForeignKey(
                name: "fk_operation_truck1",
                table: "Operations");

            migrationBuilder.DropForeignKey(
                name: "fk_shipping agent_address1",
                table: "ShippingAgents");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingAgents_Images_ImageId",
                table: "ShippingAgents");

            migrationBuilder.DropForeignKey(
                name: "FK_Trucks_Images_ImageId",
                table: "Trucks");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Trucks_ImageId",
                table: "Trucks");

            migrationBuilder.DropIndex(
                name: "IX_ShippingAgents_ImageId",
                table: "ShippingAgents");

            migrationBuilder.DropIndex(
                name: "IX_Operations_ECDDocumentId",
                table: "Operations");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_ImageId",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Containers_ImageId",
                table: "Containers");

            migrationBuilder.DropIndex(
                name: "IX_BillOfLoadings_BillOfLoadingDocumentId",
                table: "BillOfLoadings");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "ShippingAgents");

            migrationBuilder.DropColumn(
                name: "ECDDocumentId",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "BillOfLoadingDocumentId",
                table: "BillOfLoadings");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Ports",
                newName: "PortNumber");

            migrationBuilder.RenameColumn(
                name: "Loacation",
                table: "Containers",
                newName: "Location");

            migrationBuilder.AlterColumn<float>(
                name: "Capacity",
                table: "Trucks",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Trucks",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "ShippingAgents",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ECDDocument",
                table: "Operations",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OperationId",
                table: "Goods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Drivers",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Containers",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "BillOfLoadingDocument",
                table: "BillOfLoadings",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OperationStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GeneratedDocumentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneratedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OperationId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationStatus_Operations_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Goods_OperationId",
                table: "Goods",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationStatus_OperationId",
                table: "OperationStatus",
                column: "OperationId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillOfLoadings_ShippingAgents_ShippingAgentId",
                table: "BillOfLoadings",
                column: "ShippingAgentId",
                principalTable: "ShippingAgents",
                principalColumn: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_BillOfLoadings_BillOfLoadingId",
                table: "Operations",
                column: "BillOfLoadingId",
                principalTable: "BillOfLoadings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Companies_CompanyId",
                table: "Operations",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Drivers_DriverId",
                table: "Operations",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Trucks_truck_id",
                table: "Operations",
                column: "truck_id",
                principalTable: "Trucks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingAgents_Addresses_AddressId",
                table: "ShippingAgents",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillOfLoadings_ShippingAgents_ShippingAgentId",
                table: "BillOfLoadings");

            migrationBuilder.DropForeignKey(
                name: "FK_Goods_Containers_ContainerId",
                table: "Goods");

            migrationBuilder.DropForeignKey(
                name: "FK_Goods_Operations_OperationId",
                table: "Goods");

            migrationBuilder.DropForeignKey(
                name: "FK_Operations_BillOfLoadings_BillOfLoadingId",
                table: "Operations");

            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Companies_CompanyId",
                table: "Operations");

            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Drivers_DriverId",
                table: "Operations");

            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Trucks_truck_id",
                table: "Operations");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingAgents_Addresses_AddressId",
                table: "ShippingAgents");

            migrationBuilder.DropTable(
                name: "OperationStatus");

            migrationBuilder.DropIndex(
                name: "IX_Goods_OperationId",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "ShippingAgents");

            migrationBuilder.DropColumn(
                name: "ECDDocument",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "OperationId",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "BillOfLoadingDocument",
                table: "BillOfLoadings");

            migrationBuilder.RenameColumn(
                name: "PortNumber",
                table: "Ports",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Containers",
                newName: "Loacation");

            migrationBuilder.AlterColumn<float>(
                name: "Capacity",
                table: "Trucks",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Trucks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "ShippingAgents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ECDDocumentId",
                table: "Operations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Drivers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Containers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BillOfLoadingDocumentId",
                table: "BillOfLoadings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_ImageId",
                table: "Trucks",
                column: "ImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAgents_ImageId",
                table: "ShippingAgents",
                column: "ImageId",
                unique: true,
                filter: "[ImageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_ECDDocumentId",
                table: "Operations",
                column: "ECDDocumentId",
                unique: true,
                filter: "[ECDDocumentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_ImageId",
                table: "Drivers",
                column: "ImageId",
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_BillOfLoadings_Documents_BillOfLoadingDocumentId",
                table: "BillOfLoadings",
                column: "BillOfLoadingDocumentId",
                principalTable: "Documents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BillOfLoadings_ShippingAgents_ShippingAgentId",
                table: "BillOfLoadings",
                column: "ShippingAgentId",
                principalTable: "ShippingAgents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Containers_Images_ImageId",
                table: "Containers",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Images_ImageId",
                table: "Drivers",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "fk_good_container1",
                table: "Goods",
                column: "ContainerId",
                principalTable: "Containers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "fk_ECD Document_operation1",
                table: "Operations",
                column: "ECDDocumentId",
                principalTable: "Documents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "fk_operation_Bill of Loading1",
                table: "Operations",
                column: "BillOfLoadingId",
                principalTable: "BillOfLoadings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "fk_operation_company1",
                table: "Operations",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "fk_operation_driver1",
                table: "Operations",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "fk_operation_truck1",
                table: "Operations",
                column: "truck_id",
                principalTable: "Trucks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "fk_shipping agent_address1",
                table: "ShippingAgents",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingAgents_Images_ImageId",
                table: "ShippingAgents",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trucks_Images_ImageId",
                table: "Trucks",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }
    }
}
