using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayingSpectre.Migrations
{
    /// <inheritdoc />
    public partial class fixingorderProdandOrdermodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderProdId",
                table: "Orders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderProdId",
                table: "Orders");
        }
    }
}
