using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopManagement.Infrastructure.EFCore.Migrations
{
    public partial class ProductsPictureHasBeenMovedToaSingleMappingFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductsPictures_PictureId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_PictureId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PictureId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductsPictures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductsPictures_ProductId",
                table: "ProductsPictures",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsPictures_Products_ProductId",
                table: "ProductsPictures",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsPictures_Products_ProductId",
                table: "ProductsPictures");

            migrationBuilder.DropIndex(
                name: "IX_ProductsPictures_ProductId",
                table: "ProductsPictures");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductsPictures");

            migrationBuilder.AddColumn<int>(
                name: "PictureId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_PictureId",
                table: "Products",
                column: "PictureId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductsPictures_PictureId",
                table: "Products",
                column: "PictureId",
                principalTable: "ProductsPictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
