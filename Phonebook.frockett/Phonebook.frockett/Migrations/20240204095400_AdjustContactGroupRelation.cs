using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Phonebook.frockett.Migrations
{
    /// <inheritdoc />
    public partial class AdjustContactGroupRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_ContactGroup_ContactGroupId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_ContactGroupId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ContactGroupId",
                table: "Contacts");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_GroupId",
                table: "Contacts",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_ContactGroup_GroupId",
                table: "Contacts",
                column: "GroupId",
                principalTable: "ContactGroup",
                principalColumn: "ContactGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_ContactGroup_GroupId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_GroupId",
                table: "Contacts");

            migrationBuilder.AddColumn<int>(
                name: "ContactGroupId",
                table: "Contacts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ContactGroupId",
                table: "Contacts",
                column: "ContactGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_ContactGroup_ContactGroupId",
                table: "Contacts",
                column: "ContactGroupId",
                principalTable: "ContactGroup",
                principalColumn: "ContactGroupId");
        }
    }
}
