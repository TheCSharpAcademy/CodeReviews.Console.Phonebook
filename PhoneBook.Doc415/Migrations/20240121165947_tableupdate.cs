using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhoneBook.Doc415.Migrations
{
    /// <inheritdoc />
    public partial class tableupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Contacts");
        }
    }
}
