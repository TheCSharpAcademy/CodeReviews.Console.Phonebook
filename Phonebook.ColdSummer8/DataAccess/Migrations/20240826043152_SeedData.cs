using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "contacts",
                columns: new[] { "ID", "Email", "Name", "PhoneNumber" },
                values: new object[] { 1, "Anon@gmail.com", "Anon", "12345678" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "contacts",
                keyColumn: "ID",
                keyValue: 1);
        }
    }
}
