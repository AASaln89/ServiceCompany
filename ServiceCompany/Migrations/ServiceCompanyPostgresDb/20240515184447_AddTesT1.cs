using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceCompany.Migrations.ServiceCompanyPostgresDb
{
    /// <inheritdoc />
    public partial class AddTesT1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ids",
                table: "Tests");

            migrationBuilder.AddColumn<string>(
                name: "NNam",
                table: "Tests",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NNam",
                table: "Tests");

            migrationBuilder.AddColumn<List<int>>(
                name: "Ids",
                table: "Tests",
                type: "integer[]",
                nullable: true);
        }
    }
}
