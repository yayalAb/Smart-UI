using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class Addbutton1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "buttonNameVal",
                table: "ButtonFeilds");

            migrationBuilder.DropColumn(
                name: "buttonTypeVal",
                table: "ButtonFeilds");

            migrationBuilder.DropColumn(
                name: "classNameVal",
                table: "ButtonFeilds");

            migrationBuilder.DropColumn(
                name: "colorVal",
                table: "ButtonFeilds");

            migrationBuilder.DropColumn(
                name: "eventNameVal",
                table: "ButtonFeilds");

            migrationBuilder.RenameColumn(
                name: "NoOfFeildsINRow",
                table: "Components",
                newName: "NoOfFeildsInRow");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Projects",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "componentNameVal",
                table: "buttons",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "componentNameVal",
                table: "buttons");

            migrationBuilder.RenameColumn(
                name: "NoOfFeildsInRow",
                table: "Components",
                newName: "NoOfFeildsINRow");

            migrationBuilder.AddColumn<string>(
                name: "buttonNameVal",
                table: "ButtonFeilds",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "buttonTypeVal",
                table: "ButtonFeilds",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "classNameVal",
                table: "ButtonFeilds",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "colorVal",
                table: "ButtonFeilds",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "eventNameVal",
                table: "ButtonFeilds",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
