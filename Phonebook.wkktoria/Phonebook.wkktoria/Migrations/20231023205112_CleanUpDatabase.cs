using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Phonebook.wkktoria.Migrations
{
    /// <inheritdoc />
    public partial class CleanUpDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 4);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "CategoryId", "Email", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, 1, "john@email.com", "John", "123-123-123" },
                    { 2, 1, "adam@email.com", "Adam", "111-111-111" },
                    { 3, 2, "anne@email.com", "Anne", "321-321-321" },
                    { 4, 3, "victoria@email.com", "Victoria", "333-333-333" }
                });
        }
    }
}
