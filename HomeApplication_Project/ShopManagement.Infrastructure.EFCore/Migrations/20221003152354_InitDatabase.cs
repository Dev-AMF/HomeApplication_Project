using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopManagement.Infrastructure.EFCore.Migrations
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductCategoriesPageMetas",
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
                    table.PrimaryKey("PK_ProductCategoriesPageMetas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategoriesPictures",
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
                    table.PrimaryKey("PK_ProductCategoriesPictures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    PictureId = table.Column<int>(nullable: false),
                    MetasId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCategories_ProductCategoriesPageMetas_MetasId",
                        column: x => x.MetasId,
                        principalTable: "ProductCategoriesPageMetas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategories_ProductCategoriesPictures_PictureId",
                        column: x => x.PictureId,
                        principalTable: "ProductCategoriesPictures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_MetasId",
                table: "ProductCategories",
                column: "MetasId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_PictureId",
                table: "ProductCategories",
                column: "PictureId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "ProductCategoriesPageMetas");

            migrationBuilder.DropTable(
                name: "ProductCategoriesPictures");
        }
    }
}
