using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhoneBook.Migrations
{
    /// <inheritdoc />
    public partial class AddedTempId1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TempId",
                table: "Contact",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TempId",
                table: "Contact");
        }
    }
}
