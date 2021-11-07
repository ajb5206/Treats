using Microsoft.EntityFrameworkCore.Migrations;

namespace PierreTreats.Migrations
{
    public partial class AddUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"INSERT INTO AspNetRoles (Id, Name, NormalizedName) Values (1,'Owner','OWNER')");
            migrationBuilder.Sql($"INSERT INTO AspNetRoles (Id, Name, NormalizedName) Values (2, 'Patron', 'PATRON')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DELETE FROM AspNetRoles WHERE Id = 1");
            migrationBuilder.Sql($"DELETE FROM AspNetRoles WHERE Id = 2");
        }
    }
}
