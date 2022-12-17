using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class table_change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConnaissementNumber",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "TypeOfMerchandise",
                table: "Operations");

            migrationBuilder.AddColumn<float>(
                name: "AgreedTariff",
                table: "TruckAssignments",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "TruckAssignments",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "GatePassType",
                table: "TruckAssignments",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SENumber",
                table: "TruckAssignments",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "Containers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Article",
                table: "Containers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Containers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "GeneratedDocumentId",
                table: "Containers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GoodsDescription",
                table: "Containers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<float>(
                name: "GrossWeight",
                table: "Containers",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Containers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "TotalPrice",
                table: "Containers",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "WeightMeasurement",
                table: "Containers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CurrencyType",
                table: "BankInformation",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GeneratedDocument",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LoadType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DocumentType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OperationId = table.Column<int>(type: "int", nullable: false),
                    ExitPortId = table.Column<int>(type: "int", nullable: false),
                    DestinationPortId = table.Column<int>(type: "int", nullable: false),
                    ContactPersonId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastModified = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneratedDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneratedDocument_ContactPeople_ContactPersonId",
                        column: x => x.ContactPersonId,
                        principalTable: "ContactPeople",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneratedDocument_Operations_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneratedDocument_Ports_DestinationPortId",
                        column: x => x.DestinationPortId,
                        principalTable: "Ports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneratedDocument_Ports_ExitPortId",
                        column: x => x.ExitPortId,
                        principalTable: "Ports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GeneratedDocumentGood",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    GoodId = table.Column<int>(type: "int", nullable: false),
                    GeneratedDocumentId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastModified = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneratedDocumentGood", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneratedDocumentGood_GeneratedDocument_GeneratedDocumentId",
                        column: x => x.GeneratedDocumentId,
                        principalTable: "GeneratedDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneratedDocumentGood_Goods_GoodId",
                        column: x => x.GoodId,
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Containers_GeneratedDocumentId",
                table: "Containers",
                column: "GeneratedDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneratedDocument_ContactPersonId",
                table: "GeneratedDocument",
                column: "ContactPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneratedDocument_DestinationPortId",
                table: "GeneratedDocument",
                column: "DestinationPortId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneratedDocument_ExitPortId",
                table: "GeneratedDocument",
                column: "ExitPortId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneratedDocument_OperationId",
                table: "GeneratedDocument",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneratedDocumentGood_GeneratedDocumentId",
                table: "GeneratedDocumentGood",
                column: "GeneratedDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneratedDocumentGood_GoodId",
                table: "GeneratedDocumentGood",
                column: "GoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Containers_GeneratedDocument_GeneratedDocumentId",
                table: "Containers",
                column: "GeneratedDocumentId",
                principalTable: "GeneratedDocument",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Containers_GeneratedDocument_GeneratedDocumentId",
                table: "Containers");

            migrationBuilder.DropTable(
                name: "GeneratedDocumentGood");

            migrationBuilder.DropTable(
                name: "GeneratedDocument");

            migrationBuilder.DropIndex(
                name: "IX_Containers_GeneratedDocumentId",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "AgreedTariff",
                table: "TruckAssignments");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "TruckAssignments");

            migrationBuilder.DropColumn(
                name: "GatePassType",
                table: "TruckAssignments");

            migrationBuilder.DropColumn(
                name: "SENumber",
                table: "TruckAssignments");

            migrationBuilder.DropColumn(
                name: "Article",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "GeneratedDocumentId",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "GoodsDescription",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "GrossWeight",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "WeightMeasurement",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "CurrencyType",
                table: "BankInformation");

            migrationBuilder.AddColumn<string>(
                name: "ConnaissementNumber",
                table: "Operations",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TypeOfMerchandise",
                table: "Operations",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<float>(
                name: "Size",
                table: "Containers",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
