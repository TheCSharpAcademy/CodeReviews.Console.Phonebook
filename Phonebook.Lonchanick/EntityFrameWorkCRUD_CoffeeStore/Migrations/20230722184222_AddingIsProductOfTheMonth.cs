﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayingSpectre.Migrations
{
    /// <inheritdoc />
    public partial class addingisProductOfTheMonth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCoffeeOfTheMonth",
                table: "Coffees",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCoffeeOfTheMonth",
                table: "Coffees");
        }
    }
}
