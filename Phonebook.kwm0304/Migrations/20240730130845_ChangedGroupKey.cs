using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Phonebook.kwm0304.Migrations
{
    /// <inheritdoc />
    public partial class ChangedGroupKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_ContactGroups_GroupName",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_GroupName",
                table: "Contacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactGroups",
                table: "ContactGroups");

            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "Contacts");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Contacts",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GroupName",
                table: "ContactGroups",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "ContactGroups",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactGroups",
                table: "ContactGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_GroupId",
                table: "Contacts",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_ContactGroups_GroupId",
                table: "Contacts",
                column: "GroupId",
                principalTable: "ContactGroups",
                principalColumn: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_ContactGroups_GroupId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_GroupId",
                table: "Contacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactGroups",
                table: "ContactGroups");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "ContactGroups");

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "Contacts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GroupName",
                table: "ContactGroups",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactGroups",
                table: "ContactGroups",
                column: "GroupName");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_GroupName",
                table: "Contacts",
                column: "GroupName");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_ContactGroups_GroupName",
                table: "Contacts",
                column: "GroupName",
                principalTable: "ContactGroups",
                principalColumn: "GroupName");
        }
    }
}
