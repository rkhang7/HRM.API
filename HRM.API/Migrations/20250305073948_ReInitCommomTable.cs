using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRM.API.Migrations
{
    /// <inheritdoc />
    public partial class ReInitCommomTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MasterEntity",
                table: "MasterEntity");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "MasterEntity");

            migrationBuilder.RenameTable(
                name: "MasterEntity",
                newName: "Commoms");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Commoms",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GroupCode",
                table: "Commoms",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Commoms",
                type: "varchar(12)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(12)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "Commoms",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Commoms",
                table: "Commoms",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Commoms",
                table: "Commoms");

            migrationBuilder.RenameTable(
                name: "Commoms",
                newName: "MasterEntity");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MasterEntity",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "GroupCode",
                table: "MasterEntity",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "MasterEntity",
                type: "varchar(12)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(12)");

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "MasterEntity",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "MasterEntity",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MasterEntity",
                table: "MasterEntity",
                column: "Id");
        }
    }
}
