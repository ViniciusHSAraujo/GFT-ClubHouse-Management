using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GFT_ClubHouse__Management.Migrations {
    public partial class InitialMigrationFixed : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                "Addresses",
                table => new {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Street = table.Column<string>(maxLength: 80, nullable: false),
                    City = table.Column<string>(maxLength: 80, nullable: false),
                    Zip = table.Column<string>(nullable: false),
                    State = table.Column<string>(maxLength: 12, nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Addresses", x => x.Id); });

            migrationBuilder.CreateTable(
                "MusicalGenres",
                table => new {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 80, nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_MusicalGenres", x => x.Id); });

            migrationBuilder.CreateTable(
                "ClubHouses",
                table => new {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 80, nullable: false),
                    AddressId = table.Column<int>(nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_ClubHouses", x => x.Id);
                    table.ForeignKey(
                        "FK_ClubHouses_Addresses_AddressId",
                        x => x.AddressId,
                        "Addresses",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Users",
                table => new {
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
                constraints: table => {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        "FK_Users_Addresses_AddressId",
                        x => x.AddressId,
                        "Addresses",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Events",
                table => new {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 80, nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    ClubHouseId = table.Column<int>(nullable: false),
                    MusicalGenreId = table.Column<int>(nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        "FK_Events_ClubHouses_ClubHouseId",
                        x => x.ClubHouseId,
                        "ClubHouses",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Events_MusicalGenres_MusicalGenreId",
                        x => x.MusicalGenreId,
                        "MusicalGenres",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                "Addresses",
                new[] {"Id", "City", "State", "Street", "Zip"},
                new object[] {1, "Seattle", "Washington", "1234 1St Ave", "98101"});

            migrationBuilder.InsertData(
                "Users",
                new[] {"Id", "AddressId", "Email", "LastName", "Name", "Password", "Phone", "Roles"},
                new object[]
                    {1, 1, "admin@admin.com", "Default", "Admin", "690e2695b6aa8f08dc1fd736072e5819", "123456789", 0});

            migrationBuilder.CreateIndex(
                "IX_ClubHouses_AddressId",
                "ClubHouses",
                "AddressId");

            migrationBuilder.CreateIndex(
                "IX_Events_ClubHouseId",
                "Events",
                "ClubHouseId");

            migrationBuilder.CreateIndex(
                "IX_Events_MusicalGenreId",
                "Events",
                "MusicalGenreId");

            migrationBuilder.CreateIndex(
                "IX_Users_AddressId",
                "Users",
                "AddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                "Events");

            migrationBuilder.DropTable(
                "Users");

            migrationBuilder.DropTable(
                "ClubHouses");

            migrationBuilder.DropTable(
                "MusicalGenres");

            migrationBuilder.DropTable(
                "Addresses");
        }
    }
}