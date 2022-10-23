using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopManagement.Infrastructure.EFCore.Migrations
{
    public partial class ProductsPageMetasHasBeenMovedToaSingleMappingFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductsPageMetas_MetasId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_MetasId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MetasId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductsPageMetas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductsPageMetas_ProductId",
                table: "ProductsPageMetas",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsPageMetas_Products_ProductId",
                table: "ProductsPageMetas",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsPageMetas_Products_ProductId",
                table: "ProductsPageMetas");

            migrationBuilder.DropIndex(
                name: "IX_ProductsPageMetas_ProductId",
                table: "ProductsPageMetas");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductsPageMetas");

            migrationBuilder.AddColumn<int>(
                name: "MetasId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_MetasId",
                table: "Products",
                column: "MetasId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductsPageMetas_MetasId",
                table: "Products",
                column: "MetasId",
                principalTable: "ProductsPageMetas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
