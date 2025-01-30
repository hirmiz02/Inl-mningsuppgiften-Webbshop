using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inlämningsuppgiften_Webbshop.Migrations
{
    /// <inheritdoc />
    public partial class addedDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopProduct_Products_ProductId",
                table: "TopProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TopProduct",
                table: "TopProduct");

            migrationBuilder.RenameTable(
                name: "TopProduct",
                newName: "TopProducts");

            migrationBuilder.RenameIndex(
                name: "IX_TopProduct_ProductId",
                table: "TopProducts",
                newName: "IX_TopProducts_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TopProducts",
                table: "TopProducts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TopProducts_Products_ProductId",
                table: "TopProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopProducts_Products_ProductId",
                table: "TopProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TopProducts",
                table: "TopProducts");

            migrationBuilder.RenameTable(
                name: "TopProducts",
                newName: "TopProduct");

            migrationBuilder.RenameIndex(
                name: "IX_TopProducts_ProductId",
                table: "TopProduct",
                newName: "IX_TopProduct_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TopProduct",
                table: "TopProduct",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TopProduct_Products_ProductId",
                table: "TopProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
