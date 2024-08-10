using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Phonebook.kwm0304.Migrations
{
    /// <inheritdoc />
    public partial class ChangedContactNumberType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ContactPhoneInt",
                table: "Contacts",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ContactPhoneInt",
                table: "Contacts",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
