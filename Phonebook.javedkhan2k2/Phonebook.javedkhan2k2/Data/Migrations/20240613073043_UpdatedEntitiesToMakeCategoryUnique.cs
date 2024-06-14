using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Phonebook.javedkhan2k2.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEntitiesToMakeCategoryUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_ContactCategories_ContactCategoryId",
                table: "Contacts");

            migrationBuilder.AlterColumn<int>(
                name: "ContactCategoryId",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "ContactCategories",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ContactCategories_CategoryName",
                table: "ContactCategories",
                column: "CategoryName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_ContactCategories_ContactCategoryId",
                table: "Contacts",
                column: "ContactCategoryId",
                principalTable: "ContactCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_ContactCategories_ContactCategoryId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_ContactCategories_CategoryName",
                table: "ContactCategories");

            migrationBuilder.AlterColumn<int>(
                name: "ContactCategoryId",
                table: "Contacts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "ContactCategories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_ContactCategories_ContactCategoryId",
                table: "Contacts",
                column: "ContactCategoryId",
                principalTable: "ContactCategories",
                principalColumn: "Id");
        }
    }
}
