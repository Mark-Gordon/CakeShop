using Microsoft.EntityFrameworkCore.Migrations;

namespace CakeShop.Migrations
{
    public partial class FixLongDescriptionColumnNameInCakeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LongDescritpion",
                table: "Cakes");

            migrationBuilder.AddColumn<string>(
                name: "LongDescription",
                table: "Cakes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LongDescription",
                table: "Cakes");

            migrationBuilder.AddColumn<string>(
                name: "LongDescritpion",
                table: "Cakes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
