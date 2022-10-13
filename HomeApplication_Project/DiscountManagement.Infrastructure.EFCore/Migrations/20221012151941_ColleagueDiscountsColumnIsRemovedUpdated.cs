using Microsoft.EntityFrameworkCore.Migrations;

namespace DiscountManagement.Infrastructure.EFCore.Migrations
{
    public partial class ColleagueDiscountsColumnIsRemovedUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRemved",
                table: "ColleagueDiscounts");

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "ColleagueDiscounts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "ColleagueDiscounts");

            migrationBuilder.AddColumn<bool>(
                name: "IsRemved",
                table: "ColleagueDiscounts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
