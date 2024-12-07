using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhoneBook.AnaClos.Migrations
{
    /// <inheritdoc />
    public partial class ErrorDuplicados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Categories_CategoryId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_CategoryId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Contacts");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_IdCategory",
                table: "Contacts",
                column: "IdCategory");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Categories_IdCategory",
                table: "Contacts",
                column: "IdCategory",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Categories_IdCategory",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_IdCategory",
                table: "Contacts");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Contacts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CategoryId",
                table: "Contacts",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Categories_CategoryId",
                table: "Contacts",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
