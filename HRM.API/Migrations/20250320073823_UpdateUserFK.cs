using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRM.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PositionCode",
                table: "Users",
                type: "varchar(12)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PositionCode",
                table: "Users",
                column: "PositionCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Commoms_PositionCode",
                table: "Users",
                column: "PositionCode",
                principalTable: "Commoms",
                principalColumn: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Commoms_PositionCode",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PositionCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PositionCode",
                table: "Users");
        }
    }
}
