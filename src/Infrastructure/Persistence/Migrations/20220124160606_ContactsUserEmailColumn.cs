using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZuumApp.Infrastructure.Persistence.Migrations
{
    public partial class ContactsUserEmailColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Contacts",
                newName: "UserEmail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "Contacts",
                newName: "UserId");
        }
    }
}
