using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubuProtokol.DataAccess.Migrations
{
    public partial class maild : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Protokol",
                keyColumn: "Id",
                keyValue: null,
                column: "MailKontrol",
                value: false);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
