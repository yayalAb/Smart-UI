using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class setimagerelationshiptonotrequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Images_ImageId",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "fk_shipping agent_image1",
                table: "ShippingAgents");

            migrationBuilder.DropForeignKey(
                name: "image",
                table: "Trucks");

            migrationBuilder.DropIndex(
                name: "IX_Trucks_ImageId",
                table: "Trucks");

            migrationBuilder.DropIndex(
                name: "IX_ShippingAgents_ImageId",
                table: "ShippingAgents");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_ImageId",
                table: "Drivers");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "ShippingAgents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Containers",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                name: "IX_Drivers_ImageId",
                table: "Drivers",
                column: "ImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Containers_ImageId",
                table: "Containers",
                column: "ImageId",
                unique: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Containers_Images_ImageId",
                table: "Containers");

            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Images_ImageId",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingAgents_Images_ImageId",
                table: "ShippingAgents");

            migrationBuilder.DropForeignKey(
                name: "FK_Trucks_Images_ImageId",
                table: "Trucks");

            migrationBuilder.DropIndex(
                name: "IX_Trucks_ImageId",
                table: "Trucks");

            migrationBuilder.DropIndex(
                name: "IX_ShippingAgents_ImageId",
                table: "ShippingAgents");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_ImageId",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Containers_ImageId",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Containers");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "ShippingAgents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_ImageId",
                table: "Trucks",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAgents_ImageId",
                table: "ShippingAgents",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_ImageId",
                table: "Drivers",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Images_ImageId",
                table: "Drivers",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_shipping agent_image1",
                table: "ShippingAgents",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "image",
                table: "Trucks",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }
    }
}
