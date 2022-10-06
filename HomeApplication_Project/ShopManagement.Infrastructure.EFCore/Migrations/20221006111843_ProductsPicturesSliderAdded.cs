using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopManagement.Infrastructure.EFCore.Migrations
{
    public partial class ProductsPicturesSliderAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductPicturesSlider",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(maxLength: 500, nullable: false),
                    Path = table.Column<string>(maxLength: 1000, nullable: false),
                    Alt = table.Column<string>(maxLength: 500, nullable: false),
                    Title = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPicturesSlider", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPicturesSlider_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductPicturesSlider_ProductId",
                table: "ProductPicturesSlider",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductPicturesSlider");
        }
    }
}
