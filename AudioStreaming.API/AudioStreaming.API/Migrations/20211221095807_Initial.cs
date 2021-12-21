using Microsoft.EntityFrameworkCore.Migrations;

namespace AudioStreaming.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Canzoni",
                columns: table => new
                {
                    SongID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SongTitle = table.Column<string>(nullable: false),
                    AlbumName = table.Column<string>(nullable: true),
                    IDUserUploader = table.Column<int>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    DownnloadNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canzoni", x => x.SongID);
                });

            migrationBuilder.CreateTable(
                name: "Playlist",
                columns: table => new
                {
                    PlaylistID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    SongsIDs = table.Column<string>(nullable: false),
                    Private = table.Column<bool>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlist", x => x.PlaylistID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Canzoni");

            migrationBuilder.DropTable(
                name: "Playlist");
        }
    }
}
