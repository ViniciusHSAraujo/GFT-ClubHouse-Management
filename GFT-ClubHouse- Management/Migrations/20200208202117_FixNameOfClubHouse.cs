using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GFT_ClubHouse__Management.Migrations
{
    public partial class FixNameOfClubHouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_HouseClubs_HouseClubId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "HouseClubs");

            migrationBuilder.RenameColumn(
                name: "HouseClubId",
                table: "Events",
                newName: "ClubHouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_HouseClubId",
                table: "Events",
                newName: "IX_Events_ClubHouseId");

            migrationBuilder.CreateTable(
                name: "ClubHouses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 80, nullable: false),
                    AddressId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubHouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClubHouses_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClubHouses_AddressId",
                table: "ClubHouses",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_ClubHouses_ClubHouseId",
                table: "Events",
                column: "ClubHouseId",
                principalTable: "ClubHouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_ClubHouses_ClubHouseId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "ClubHouses");

            migrationBuilder.RenameColumn(
                name: "ClubHouseId",
                table: "Events",
                newName: "HouseClubId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_ClubHouseId",
                table: "Events",
                newName: "IX_Events_HouseClubId");

            migrationBuilder.CreateTable(
                name: "HouseClubs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AddressId = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseClubs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HouseClubs_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HouseClubs_AddressId",
                table: "HouseClubs",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_HouseClubs_HouseClubId",
                table: "Events",
                column: "HouseClubId",
                principalTable: "HouseClubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
