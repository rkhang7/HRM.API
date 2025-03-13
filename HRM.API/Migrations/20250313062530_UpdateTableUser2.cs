using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRM.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableUser2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Commoms_GenderId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Commoms_PositionId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_GenderId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PositionId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GenderCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "Gender",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PositionCode",
                table: "Users",
                column: "PositionCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Commoms_PositionCode",
                table: "Users",
                column: "PositionCode",
                principalTable: "Commoms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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
                name: "Gender",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "GenderCode",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_GenderId",
                table: "Users",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PositionId",
                table: "Users",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Commoms_GenderId",
                table: "Users",
                column: "GenderId",
                principalTable: "Commoms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Commoms_PositionId",
                table: "Users",
                column: "PositionId",
                principalTable: "Commoms",
                principalColumn: "Id");
        }
    }
}
