using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubuProtokol.DataAccess.Migrations
{
    public partial class firstcommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Konu",
                table: "Protokol",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Konu",
                table: "Protokol");
        }
    }
}
