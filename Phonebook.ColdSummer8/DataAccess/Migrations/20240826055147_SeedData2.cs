using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "contacts",
                columns: new[] { "ID", "Email", "Name", "PhoneNumber" },
                values: new object[] { 2, "NotAnon@gmail.com", "Not Anon", "87654321" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "contacts",
                keyColumn: "ID",
                keyValue: 2);
        }
    }
}
