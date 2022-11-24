using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class trackAssignmentmanytomanyrelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Containers_TruckAssignments_TruckAssignmentId",
                table: "Containers");

            migrationBuilder.DropIndex(
                name: "IX_Containers_TruckAssignmentId",
                table: "Containers");

            migrationBuilder.AddColumn<int>(
                name: "TruckAssignmentId",
                table: "Goods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ContainerTruckAssignment",
                columns: table => new
                {
                    ContainersId = table.Column<int>(type: "int", nullable: false),
                    TruckAssignmentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContainerTruckAssignment", x => new { x.ContainersId, x.TruckAssignmentsId });
                    table.ForeignKey(
                        name: "FK_ContainerTruckAssignment_Containers_ContainersId",
                        column: x => x.ContainersId,
                        principalTable: "Containers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContainerTruckAssignment_TruckAssignments_TruckAssignmentsId",
                        column: x => x.TruckAssignmentsId,
                        principalTable: "TruckAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GoodTruckAssignment",
                columns: table => new
                {
                    GoodsId = table.Column<int>(type: "int", nullable: false),
                    TruckAssignmentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodTruckAssignment", x => new { x.GoodsId, x.TruckAssignmentsId });
                    table.ForeignKey(
                        name: "FK_GoodTruckAssignment_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodTruckAssignment_TruckAssignments_TruckAssignmentsId",
                        column: x => x.TruckAssignmentsId,
                        principalTable: "TruckAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ContainerTruckAssignment_TruckAssignmentsId",
                table: "ContainerTruckAssignment",
                column: "TruckAssignmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodTruckAssignment_TruckAssignmentsId",
                table: "GoodTruckAssignment",
                column: "TruckAssignmentsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContainerTruckAssignment");

            migrationBuilder.DropTable(
                name: "GoodTruckAssignment");

            migrationBuilder.DropColumn(
                name: "TruckAssignmentId",
                table: "Goods");

            migrationBuilder.CreateIndex(
                name: "IX_Containers_TruckAssignmentId",
                table: "Containers",
                column: "TruckAssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Containers_TruckAssignments_TruckAssignmentId",
                table: "Containers",
                column: "TruckAssignmentId",
                principalTable: "TruckAssignments",
                principalColumn: "Id");
        }
    }
}
