using Microsoft.EntityFrameworkCore.Migrations;

namespace GridironBulgaria.Web.Data.Migrations
{
    public partial class AddedForeignTeams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AwayForeignTeamLogoUrl",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AwayForeignTeamName",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeForeignTeamLogoUrl",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeForeignTeamName",
                table: "Games",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AwayForeignTeamLogoUrl",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "AwayForeignTeamName",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "HomeForeignTeamLogoUrl",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "HomeForeignTeamName",
                table: "Games");
        }
    }
}
