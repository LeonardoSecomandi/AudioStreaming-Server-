using Microsoft.EntityFrameworkCore.Migrations;

namespace Client_EasySound.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "eleclient",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    utente = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eleclient", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "eleclient");
        }
    }
}
