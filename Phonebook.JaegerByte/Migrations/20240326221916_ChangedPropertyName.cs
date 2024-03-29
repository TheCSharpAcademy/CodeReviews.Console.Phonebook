using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Phonebook.JaegerByte.Migrations
{
    /// <inheritdoc />
    public partial class ChangedPropertyName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecondName",
                table: "TblPhonebook",
                newName: "LastName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "TblPhonebook",
                newName: "SecondName");
        }
    }
}
