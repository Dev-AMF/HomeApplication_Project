using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopManagement.Infrastructure.EFCore.Migrations
{
    public partial class ProductCategoryItsPageMetasHasMovedToASingleMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_ProductCategoriesPageMetas_MetasId",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_MetasId",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "MetasId",
                table: "ProductCategories");

            migrationBuilder.AddColumn<int>(
                name: "ProductCategoryID",
                table: "ProductCategoriesPageMetas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategoriesPageMetas_ProductCategoryID",
                table: "ProductCategoriesPageMetas",
                column: "ProductCategoryID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategoriesPageMetas_ProductCategories_ProductCategoryID",
                table: "ProductCategoriesPageMetas",
                column: "ProductCategoryID",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategoriesPageMetas_ProductCategories_ProductCategoryID",
                table: "ProductCategoriesPageMetas");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategoriesPageMetas_ProductCategoryID",
                table: "ProductCategoriesPageMetas");

            migrationBuilder.DropColumn(
                name: "ProductCategoryID",
                table: "ProductCategoriesPageMetas");

            migrationBuilder.AddColumn<int>(
                name: "MetasId",
                table: "ProductCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_MetasId",
                table: "ProductCategories",
                column: "MetasId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_ProductCategoriesPageMetas_MetasId",
                table: "ProductCategories",
                column: "MetasId",
                principalTable: "ProductCategoriesPageMetas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
