using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceCompany.Migrations
{
    /// <inheritdoc />
    public partial class asd12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "UserId",
                table: "Users",
                column: "Id",
                principalTable: "UserProfiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "UserId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

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
    }
}
