using Microsoft.EntityFrameworkCore.Migrations;

namespace GFT_ClubHouse__Management.Migrations {
    public partial class RemoveIsSoldFromTicket : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropColumn(
                "IsSold",
                "Tickets");
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.AddColumn<bool>(
                "IsSold",
                "Tickets",
                nullable: false,
                defaultValue: false);
        }
    }
}