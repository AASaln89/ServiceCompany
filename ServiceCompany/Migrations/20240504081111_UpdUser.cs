using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceCompany.Migrations
{
    /// <inheritdoc />
    public partial class UpdUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_Users_UserId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserProfiles");

            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfileId",
                table: "Users",
                column: "ProfileId",
                unique: true,
                filter: "[ProfileId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserProfiles_ProfileId",
                table: "Users",
                column: "ProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserProfiles_ProfileId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProfileId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserProfiles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_Users_UserId",
                table: "UserProfiles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
