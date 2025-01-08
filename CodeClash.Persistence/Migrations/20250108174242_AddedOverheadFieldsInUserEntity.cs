using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeClash.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedOverheadFieldsInUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "CompetitionOverhead",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ProgramWorkingTime",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "SentTime",
                table: "Users",
                type: "time without time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CompetitionOverhead",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProgramWorkingTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SentTime",
                table: "Users");
        }
    }
}
