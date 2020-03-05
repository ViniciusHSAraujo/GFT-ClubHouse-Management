using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GFT_ClubHouse__Management.Migrations {
    public partial class SaleTable : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.AddColumn<int>(
                "SaleId",
                "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "UserId",
                "Tickets",
                nullable: true);

            migrationBuilder.CreateTable(
                "Sales",
                table => new {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Quantity = table.Column<int>(nullable: false),
                    SinglePrice = table.Column<double>(nullable: false),
                    EventId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        "FK_Sales_Events_EventId",
                        x => x.EventId,
                        "Events",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Sales_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                "Users",
                "Id",
                1,
                "Password",
                "690e2695b6aa8f08dc1fd736072e5819");

            migrationBuilder.CreateIndex(
                "IX_Tickets_SaleId",
                "Tickets",
                "SaleId");

            migrationBuilder.CreateIndex(
                "IX_Tickets_UserId",
                "Tickets",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_Sales_EventId",
                "Sales",
                "EventId");

            migrationBuilder.CreateIndex(
                "IX_Sales_UserId",
                "Sales",
                "UserId");

            migrationBuilder.AddForeignKey(
                "FK_Tickets_Sales_SaleId",
                "Tickets",
                "SaleId",
                "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Tickets_Users_UserId",
                "Tickets",
                "UserId",
                "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropForeignKey(
                "FK_Tickets_Sales_SaleId",
                "Tickets");

            migrationBuilder.DropForeignKey(
                "FK_Tickets_Users_UserId",
                "Tickets");

            migrationBuilder.DropTable(
                "Sales");

            migrationBuilder.DropIndex(
                "IX_Tickets_SaleId",
                "Tickets");

            migrationBuilder.DropIndex(
                "IX_Tickets_UserId",
                "Tickets");

            migrationBuilder.DropColumn(
                "SaleId",
                "Tickets");

            migrationBuilder.DropColumn(
                "UserId",
                "Tickets");

            migrationBuilder.UpdateData(
                "Users",
                "Id",
                1,
                "Password",
                "2285d2badca55370a0d794a9df898c29922d21504c5c2c7fcb984c75328ad424");
        }
    }
}