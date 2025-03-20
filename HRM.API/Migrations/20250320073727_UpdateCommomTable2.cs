using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRM.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCommomTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Commoms_PositionCode",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PositionCode",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Commoms",
                table: "Commoms");

            migrationBuilder.DropColumn(
                name: "PositionCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Commoms");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Commoms");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Commoms");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Commoms",
                table: "Commoms",
                column: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Commoms",
                table: "Commoms");

            migrationBuilder.AddColumn<int>(
                name: "PositionCode",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Commoms",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Commoms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Commoms",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Commoms",
                table: "Commoms",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PositionCode",
                table: "Users",
                column: "PositionCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Commoms_PositionCode",
                table: "Users",
                column: "PositionCode",
                principalTable: "Commoms",
                principalColumn: "Id");
        }
    }
}
