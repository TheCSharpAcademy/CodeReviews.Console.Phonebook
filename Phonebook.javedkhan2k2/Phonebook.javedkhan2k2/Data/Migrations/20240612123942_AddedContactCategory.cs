using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Phonebook.javedkhan2k2.Migrations
{
    /// <inheritdoc />
    public partial class AddedContactCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContactCategoryId",
                table: "Contacts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContactCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ContactCategoryId",
                table: "Contacts",
                column: "ContactCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_ContactCategories_ContactCategoryId",
                table: "Contacts",
                column: "ContactCategoryId",
                principalTable: "ContactCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_ContactCategories_ContactCategoryId",
                table: "Contacts");

            migrationBuilder.DropTable(
                name: "ContactCategories");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_ContactCategoryId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ContactCategoryId",
                table: "Contacts");
        }
    }
}
