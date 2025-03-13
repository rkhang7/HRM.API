using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRM.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCommomTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "Commoms",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Commoms",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Commoms");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Commoms",
                newName: "CreateAt");
        }
    }
}
