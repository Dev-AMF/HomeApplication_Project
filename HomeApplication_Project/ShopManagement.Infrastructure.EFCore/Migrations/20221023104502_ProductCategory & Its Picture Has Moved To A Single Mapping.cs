using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopManagement.Infrastructure.EFCore.Migrations
{
    public partial class ProductCategoryItsPictureHasMovedToASingleMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_ProductCategoriesPictures_PictureId",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_PictureId",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "PictureId",
                table: "ProductCategories");

            migrationBuilder.AddColumn<int>(
                name: "ProductCategoryId",
                table: "ProductCategoriesPictures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategoriesPictures_ProductCategoryId",
                table: "ProductCategoriesPictures",
                column: "ProductCategoryId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategoriesPictures_ProductCategories_ProductCategoryId",
                table: "ProductCategoriesPictures",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategoriesPictures_ProductCategories_ProductCategoryId",
                table: "ProductCategoriesPictures");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategoriesPictures_ProductCategoryId",
                table: "ProductCategoriesPictures");

            migrationBuilder.DropColumn(
                name: "ProductCategoryId",
                table: "ProductCategoriesPictures");

            migrationBuilder.AddColumn<int>(
                name: "PictureId",
                table: "ProductCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_PictureId",
                table: "ProductCategories",
                column: "PictureId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_ProductCategoriesPictures_PictureId",
                table: "ProductCategories",
                column: "PictureId",
                principalTable: "ProductCategoriesPictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
