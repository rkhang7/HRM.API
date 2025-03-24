using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRM.API.Migrations
{
    /// <inheritdoc />
    public partial class CheckInCheckOutFaceUrlAttendanceUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FaceUrl",
                table: "Attendances",
                newName: "CheckInFaceUrl");

            migrationBuilder.AddColumn<string>(
                name: "CheckoutFaceUrl",
                table: "Attendances",
                type: "varchar(500)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckoutFaceUrl",
                table: "Attendances");

            migrationBuilder.RenameColumn(
                name: "CheckInFaceUrl",
                table: "Attendances",
                newName: "FaceUrl");
        }
    }
}
