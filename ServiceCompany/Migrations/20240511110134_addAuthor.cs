using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceCompany.Migrations
{
    /// <inheritdoc />
    public partial class addAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_AuthorId",
                table: "Users",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_AuthorId",
                table: "Projects",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_AuthorId",
                table: "Companies",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Users_AuthorId",
                table: "Companies",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_AuthorId",
                table: "Projects",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_AuthorId",
                table: "Users",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Users_AuthorId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_AuthorId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_AuthorId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AuthorId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Projects_AuthorId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Companies_AuthorId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Companies");
        }
    }
}
