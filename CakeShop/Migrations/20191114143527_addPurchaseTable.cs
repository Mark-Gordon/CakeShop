using Microsoft.EntityFrameworkCore.Migrations;

namespace CakeShop.Migrations
{
    public partial class addPurchaseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Cakes_CakeId",
                table: "Cart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cart",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_CakeId",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "CakeId",
                table: "Cart");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "Cart",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cart",
                table: "Cart",
                column: "CartId");

            migrationBuilder.CreateTable(
                name: "Purchase",
                columns: table => new
                {
                    PurchaseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(nullable: false),
                    CakeId = table.Column<int>(nullable: true),
                    Cost = table.Column<decimal>(nullable: false),
                    Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase", x => x.PurchaseId);
                    table.ForeignKey(
                        name: "FK_Purchase_Cakes_CakeId",
                        column: x => x.CakeId,
                        principalTable: "Cakes",
                        principalColumn: "CakeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Purchase_Cart_CartId",
                        column: x => x.CartId,
                        principalTable: "Cart",
                        principalColumn: "CartId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_CakeId",
                table: "Purchase",
                column: "CakeId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_CartId",
                table: "Purchase",
                column: "CartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Purchase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cart",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Cart");

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartId",
                table: "Cart",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "CakeId",
                table: "Cart",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cart",
                table: "Cart",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_CakeId",
                table: "Cart",
                column: "CakeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Cakes_CakeId",
                table: "Cart",
                column: "CakeId",
                principalTable: "Cakes",
                principalColumn: "CakeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
