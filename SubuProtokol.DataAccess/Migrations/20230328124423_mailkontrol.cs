using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubuProtokol.DataAccess.Migrations
{
    public partial class mailkontrol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "protokoldensorumlubirim",
                table: "Protokol");

            migrationBuilder.AddColumn<bool>(
                name: "MailKontrol",
                table: "Protokol",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MailKontrol",
                table: "Protokol");

            migrationBuilder.AddColumn<string>(
                name: "protokoldensorumlubirim",
                table: "Protokol",
                type: "text",
                nullable: true);
        }
    }
}
