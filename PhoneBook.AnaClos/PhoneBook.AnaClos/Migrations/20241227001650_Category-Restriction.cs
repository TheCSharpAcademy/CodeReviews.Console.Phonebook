using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhoneBook.AnaClos.Migrations
{
    /// <inheritdoc />
    public partial class CategoryRestriction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Category_IdCategory",
                table: "Contact");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Category_IdCategory",
                table: "Contact",
                column: "IdCategory",
                principalTable: "Category",
                principalColumn: "IdCategory",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Category_IdCategory",
                table: "Contact");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Category_IdCategory",
                table: "Contact",
                column: "IdCategory",
                principalTable: "Category",
                principalColumn: "IdCategory",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
