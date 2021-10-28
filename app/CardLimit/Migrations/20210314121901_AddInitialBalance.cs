using Microsoft.EntityFrameworkCore.Migrations;

namespace CardLimit.Migrations
{
    public partial class AddInitialBalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "InitialBalance",
                table: "Card",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitialBalance",
                table: "Card");
        }
    }
}
