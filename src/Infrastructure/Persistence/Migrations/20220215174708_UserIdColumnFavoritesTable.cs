using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZuumApp.Infrastructure.Persistence.Migrations
{
    public partial class UserIdColumnFavoritesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Favorites",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Favorites");
        }
    }
}
