using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Phone_Book.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emails_Contacts_ContactId",
                table: "Emails");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_Contacts_ContactId",
                table: "PhoneNumbers");

            migrationBuilder.AlterColumn<int>(
                name: "ContactId",
                table: "PhoneNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContactId",
                table: "Emails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_Contacts_ContactId",
                table: "Emails",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_Contacts_ContactId",
                table: "PhoneNumbers",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emails_Contacts_ContactId",
                table: "Emails");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_Contacts_ContactId",
                table: "PhoneNumbers");

            migrationBuilder.AlterColumn<int>(
                name: "ContactId",
                table: "PhoneNumbers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ContactId",
                table: "Emails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_Contacts_ContactId",
                table: "Emails",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_Contacts_ContactId",
                table: "PhoneNumbers",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id");
        }
    }
}
