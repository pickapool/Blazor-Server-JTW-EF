using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blazor_JTW_EF.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Middelname",
                table: "UserAccounts",
                newName: "Middlename");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Middlename",
                table: "UserAccounts",
                newName: "Middelname");
        }
    }
}
