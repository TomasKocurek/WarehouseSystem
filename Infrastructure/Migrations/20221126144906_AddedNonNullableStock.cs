using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddedNonNullableStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movements_StockItems_StockItemId",
                table: "Movements");

            migrationBuilder.DropForeignKey(
                name: "FK_StockItems_Products_ProductId",
                table: "StockItems");

            migrationBuilder.AddForeignKey(
                name: "FK_Movements_StockItems_StockItemId",
                table: "Movements",
                column: "StockItemId",
                principalTable: "StockItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockItems_Products_ProductId",
                table: "StockItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movements_StockItems_StockItemId",
                table: "Movements");

            migrationBuilder.DropForeignKey(
                name: "FK_StockItems_Products_ProductId",
                table: "StockItems");

            migrationBuilder.AddForeignKey(
                name: "FK_Movements_StockItems_StockItemId",
                table: "Movements",
                column: "StockItemId",
                principalTable: "StockItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockItems_Products_ProductId",
                table: "StockItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
