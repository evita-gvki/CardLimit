using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CardLimit.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    CardId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CardHolderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvailableBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.CardId);
                });

            migrationBuilder.CreateTable(
                name: "Limit",
                columns: table => new
                {
                    LimitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    AggregateAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CardId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Limit", x => x.LimitId);
                    table.ForeignKey(
                        name: "FK_Limit_Card_CardId",
                        column: x => x.CardId,
                        principalTable: "Card",
                        principalColumn: "CardId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Card_CardId",
                table: "Card",
                column: "CardId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Limit_CardId",
                table: "Limit",
                column: "CardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Limit");

            migrationBuilder.DropTable(
                name: "Card");
        }
    }
}
