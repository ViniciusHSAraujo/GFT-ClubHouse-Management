using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GFT_ClubHouse__Management.Migrations
{
    public partial class InitialMigrationFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Street = table.Column<string>(maxLength: 80, nullable: false),
                    City = table.Column<string>(maxLength: 80, nullable: false),
                    Zip = table.Column<string>(nullable: false),
                    State = table.Column<string>(maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MusicalGenres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 80, nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicalGenres", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: false),
                    AddressId = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(maxLength: 64, nullable: false),
                    Roles = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 80, nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    ClubHouseId = table.Column<int>(nullable: false),
                    MusicalGenreId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_ClubHouses_ClubHouseId",
                        column: x => x.ClubHouseId,
                        principalTable: "ClubHouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_MusicalGenres_MusicalGenreId",
                        column: x => x.MusicalGenreId,
                        principalTable: "MusicalGenres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "State", "Street", "Zip" },
                values: new object[] { 1, "Seattle", "Washington", "1234 1St Ave", "98101" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AddressId", "Email", "LastName", "Name", "Password", "Phone", "Roles" },
                values: new object[] { 1, 1, "admin@admin.com", "Default", "Admin", "2285d2badca55370a0d794a9df898c29922d21504c5c2c7fcb984c75328ad424", "123456789", 0 });

            migrationBuilder.CreateIndex(
                name: "IX_ClubHouses_AddressId",
                table: "ClubHouses",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_ClubHouseId",
                table: "Events",
                column: "ClubHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_MusicalGenreId",
                table: "Events",
                column: "MusicalGenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddressId",
                table: "Users",
                column: "AddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ClubHouses");

            migrationBuilder.DropTable(
                name: "MusicalGenres");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
