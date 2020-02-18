using Microsoft.EntityFrameworkCore.Migrations;

namespace GFT_ClubHouse__Management.Migrations
{
    public partial class RemoveIsSoldFromTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSold",
                table: "Tickets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSold",
                table: "Tickets",
                nullable: false,
                defaultValue: false);
        }
    }
}
