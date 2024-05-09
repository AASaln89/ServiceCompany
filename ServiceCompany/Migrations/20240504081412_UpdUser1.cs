using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceCompany.Migrations
{
    /// <inheritdoc />
    public partial class UpdUser1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "ProfileId",
                table: "UserProfiles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_ProfileId",
                table: "UserProfiles",
                column: "ProfileId",
                unique: true,
                filter: "[ProfileId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_Users_ProfileId",
                table: "UserProfiles",
                column: "ProfileId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_Users_ProfileId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_ProfileId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "ProfileId",
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
    }
}
