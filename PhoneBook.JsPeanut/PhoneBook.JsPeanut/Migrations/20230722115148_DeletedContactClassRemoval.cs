using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhoneBook.JsPeanut.Migrations
{
    /// <inheritdoc />
    public partial class DeletedContactClassRemoval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeletedContacts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeletedContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeletedContacts", x => x.Id);
                });
        }
    }
}
