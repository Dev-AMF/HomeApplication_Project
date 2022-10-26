using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogManagement.Infrastructure.EFCore.Migrations
{
    public partial class ArticleAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(maxLength: 500, nullable: true),
                    ShortDescription = table.Column<string>(maxLength: 1000, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Picture = table.Column<string>(maxLength: 500, nullable: true),
                    PictureAlt = table.Column<string>(maxLength: 500, nullable: true),
                    PictureTitle = table.Column<string>(maxLength: 500, nullable: true),
                    PublishDate = table.Column<DateTime>(nullable: false),
                    Slug = table.Column<string>(maxLength: 600, nullable: true),
                    Keywords = table.Column<string>(maxLength: 100, nullable: true),
                    MetaDescription = table.Column<string>(maxLength: 150, nullable: true),
                    CanonicalAddress = table.Column<string>(maxLength: 1000, nullable: true),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_ArticleCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ArticleCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CategoryId",
                table: "Articles",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");
        }
    }
}
