using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeClash.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Fixed_Rooms_Users_Issues_Models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_RoomId_IsAdmin",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_IssueId",
                table: "Rooms");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "character varying(320)",
                maxLength: 320,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoomId",
                table: "Users",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_IssueId",
                table: "Rooms",
                column: "IssueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_RoomId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_IssueId",
                table: "Rooms");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(320)",
                oldMaxLength: 320);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoomId_IsAdmin",
                table: "Users",
                columns: new[] { "RoomId", "IsAdmin" });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_IssueId",
                table: "Rooms",
                column: "IssueId",
                unique: true);
        }
    }
}
