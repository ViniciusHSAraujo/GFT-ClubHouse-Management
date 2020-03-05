using Microsoft.EntityFrameworkCore.Migrations;

namespace GFT_ClubHouse__Management.Migrations {
    public partial class FixNameInClubHouseClass : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.RenameColumn(
                "Nome",
                "ClubHouses",
                "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.RenameColumn(
                "Name",
                "ClubHouses",
                "Nome");
        }
    }
}