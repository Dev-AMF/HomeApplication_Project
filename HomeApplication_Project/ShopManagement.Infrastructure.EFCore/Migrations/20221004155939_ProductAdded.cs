using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopManagement.Infrastructure.EFCore.Migrations
{
    public partial class ProductAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductsPageMetas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Keywords = table.Column<string>(maxLength: 80, nullable: true),
                    MetaDescription = table.Column<string>(maxLength: 150, nullable: true),
                    Slug = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsPageMetas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductsPictures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Path = table.Column<string>(maxLength: 100, nullable: true),
                    Alt = table.Column<string>(maxLength: 255, nullable: true),
                    Title = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsPictures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Code = table.Column<string>(maxLength: 15, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    UnitPrice = table.Column<double>(nullable: false),
                    IsInStock = table.Column<bool>(nullable: false),
                    ShortDescription = table.Column<string>(maxLength: 500, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    PictureId = table.Column<int>(nullable: false),
                    MetasId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_ProductsPageMetas_MetasId",
                        column: x => x.MetasId,
                        principalTable: "ProductsPageMetas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_ProductsPictures_PictureId",
                        column: x => x.PictureId,
                        principalTable: "ProductsPictures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_MetasId",
                table: "Products",
                column: "MetasId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_PictureId",
                table: "Products",
                column: "PictureId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductsPageMetas");

            migrationBuilder.DropTable(
                name: "ProductsPictures");
        }
    }
}
