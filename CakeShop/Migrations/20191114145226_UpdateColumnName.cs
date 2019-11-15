using Microsoft.EntityFrameworkCore.Migrations;

namespace CakeShop.Migrations
{
    public partial class UpdateColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Cart");

            migrationBuilder.AddColumn<int>(
                name: "TotalAmount",
                table: "Cart",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Cart");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Cart",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
