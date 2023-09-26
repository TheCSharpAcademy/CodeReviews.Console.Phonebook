using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Phonebook.pcjb.Migrations
{
    /// <inheritdoc />
    public partial class RenameIdColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Category_CategoryID",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Contacts",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "ContactID",
                table: "Contacts",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_CategoryID",
                table: "Contacts",
                newName: "IX_Contacts_CategoryId");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Category",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Category_CategoryId",
                table: "Contacts",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Category_CategoryId",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Contacts",
                newName: "CategoryID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Contacts",
                newName: "ContactID");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_CategoryId",
                table: "Contacts",
                newName: "IX_Contacts_CategoryID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Category",
                newName: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Category_CategoryID",
                table: "Contacts",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
