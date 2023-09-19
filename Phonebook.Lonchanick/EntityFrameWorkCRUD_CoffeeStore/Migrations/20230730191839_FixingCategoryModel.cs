using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayingSpectre.Migrations
{
    /// <inheritdoc />
    public partial class fixingcategorymodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Coffees",
                newName: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Coffees_CategoryId",
                table: "Coffees",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coffees_Categories_CategoryId",
                table: "Coffees",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "categoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coffees_Categories_CategoryId",
                table: "Coffees");

            migrationBuilder.DropIndex(
                name: "IX_Coffees_CategoryId",
                table: "Coffees");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Coffees",
                newName: "Category");
        }
    }
}
