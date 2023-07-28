using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LucianoNicolasArrieta.PhoneBook.Migrations
{
    /// <inheritdoc />
    public partial class _20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Category_CategoryId",
                table: "Contact");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Contact",
                newName: "CategoryID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Contact",
                newName: "ContactID");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_CategoryId",
                table: "Contact",
                newName: "IX_Contact_CategoryID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Category",
                newName: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Category_CategoryID",
                table: "Contact",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Category_CategoryID",
                table: "Contact");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Contact",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "ContactID",
                table: "Contact",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_CategoryID",
                table: "Contact",
                newName: "IX_Contact_CategoryId");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Category",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Category_CategoryId",
                table: "Contact",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
