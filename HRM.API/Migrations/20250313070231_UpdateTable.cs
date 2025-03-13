using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRM.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Users",
                type: "varchar(500)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Commoms_Code",
                table: "Commoms",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Commoms_Code",
                table: "Commoms");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Users");
        }
    }
}
