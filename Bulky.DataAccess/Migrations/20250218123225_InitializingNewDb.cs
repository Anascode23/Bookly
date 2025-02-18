using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookly.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitializingNewDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Products_productId",
                table: "ShoppingCarts");

            migrationBuilder.RenameColumn(
                name: "productId",
                table: "ShoppingCarts",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCarts_productId",
                table: "ShoppingCarts",
                newName: "IX_ShoppingCarts_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Products_ProductId",
                table: "ShoppingCarts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Products_ProductId",
                table: "ShoppingCarts");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ShoppingCarts",
                newName: "productId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCarts_ProductId",
                table: "ShoppingCarts",
                newName: "IX_ShoppingCarts_productId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Products_productId",
                table: "ShoppingCarts",
                column: "productId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
