using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubuProtokol.DataAccess.Migrations
{
    public partial class UnitType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BirimTuruAd",
                table: "Unit",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SabisBirimId",
                table: "Unit",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirimTuruAd",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "SabisBirimId",
                table: "Unit");
        }
    }
}
