using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeClash.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsSolutionSentInUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSentSolution",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSentSolution",
                table: "Users");
        }
    }
}
