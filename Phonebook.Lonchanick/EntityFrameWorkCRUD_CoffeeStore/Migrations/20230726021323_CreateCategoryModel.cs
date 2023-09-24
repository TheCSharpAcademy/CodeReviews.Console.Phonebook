using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayingSpectre.Migrations
{
    /// <inheritdoc />
    public partial class createcategorymodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Coffees",
                table: "Coffees");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Coffees",
                newName: "Category");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Coffees",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Category",
                table: "Coffees",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "CoffeeId",
                table: "Coffees",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coffees",
                table: "Coffees",
                column: "CoffeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Coffees_Name",
                table: "Coffees",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Coffees",
                table: "Coffees");

            migrationBuilder.DropIndex(
                name: "IX_Coffees_Name",
                table: "Coffees");

            migrationBuilder.DropColumn(
                name: "CoffeeId",
                table: "Coffees");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Coffees",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Coffees",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Coffees",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coffees",
                table: "Coffees",
                column: "Id");
        }
    }
}
