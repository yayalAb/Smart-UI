using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class GeneratedDocumentIdForiegnKeynullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfPackages",
                table: "Goods",
                newName: "RemainingQuantity");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "TruckAssignments",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Goods",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(float),
                oldType: "float",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "TruckAssignments");

            migrationBuilder.RenameColumn(
                name: "RemainingQuantity",
                table: "Goods",
                newName: "NumberOfPackages");

            migrationBuilder.AlterColumn<float>(
                name: "Quantity",
                table: "Goods",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
