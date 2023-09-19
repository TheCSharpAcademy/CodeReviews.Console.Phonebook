using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayingSpectre.Migrations
{
    /// <inheritdoc />
    public partial class addingorderProdtoOrderModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrderProds_OrderId",
                table: "OrderProds");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProds_OrderId",
                table: "OrderProds",
                column: "OrderId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrderProds_OrderId",
                table: "OrderProds");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProds_OrderId",
                table: "OrderProds",
                column: "OrderId");
        }
    }
}
