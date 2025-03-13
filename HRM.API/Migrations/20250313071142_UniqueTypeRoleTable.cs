using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRM.API.Migrations
{
    /// <inheritdoc />
    public partial class UniqueTypeRoleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Commoms_PositionCode",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "PositionCode",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Type",
                table: "Role",
                column: "Type",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Commoms_PositionCode",
                table: "Users",
                column: "PositionCode",
                principalTable: "Commoms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Commoms_PositionCode",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Role_Type",
                table: "Role");

            migrationBuilder.AlterColumn<int>(
                name: "PositionCode",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Commoms_PositionCode",
                table: "Users",
                column: "PositionCode",
                principalTable: "Commoms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
