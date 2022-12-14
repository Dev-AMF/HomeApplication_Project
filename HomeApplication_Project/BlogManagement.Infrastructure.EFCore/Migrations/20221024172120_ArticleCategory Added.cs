using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogManagement.Infrastructure.EFCore.Migrations
{
    public partial class ArticleCategoryAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    Picture = table.Column<string>(maxLength: 500, nullable: true),
                    PictureAlt = table.Column<string>(maxLength: 500, nullable: true),
                    PictureTitle = table.Column<string>(maxLength: 500, nullable: true),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    ShowOrder = table.Column<int>(nullable: false),
                    Slug = table.Column<string>(maxLength: 600, nullable: true),
                    Keywords = table.Column<string>(maxLength: 100, nullable: true),
                    MetaDescription = table.Column<string>(maxLength: 150, nullable: true),
                    CanonicalAddress = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleCategories", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleCategories");
        }
    }
}
