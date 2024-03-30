using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Phonebook.Migrations
{
    /// <inheritdoc />
    public partial class CodeReviewChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SMSHistory",
                table: "SMSHistory");

            migrationBuilder.RenameTable(
                name: "SMSHistory",
                newName: "SmsHistory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SmsHistory",
                table: "SmsHistory",
                column: "SMSHistoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SmsHistory",
                table: "SmsHistory");

            migrationBuilder.RenameTable(
                name: "SmsHistory",
                newName: "SMSHistory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SMSHistory",
                table: "SMSHistory",
                column: "SMSHistoryId");
        }
    }
}
