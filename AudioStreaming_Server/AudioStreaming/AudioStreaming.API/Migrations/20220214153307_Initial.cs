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
                    SongID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SongTitle = table.Column<string>(type: "TEXT", nullable: false),
                    AlbumName = table.Column<string>(type: "TEXT", nullable: true),
                    IDUserUploader = table.Column<int>(type: "INTEGER", nullable: false),
                    Duration = table.Column<int>(type: "INTEGER", nullable: false),
                    DownnloadNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canzoni", x => x.SongID);
                });

            migrationBuilder.CreateTable(
                name: "Playlist",
                columns: table => new
                {
                    PlaylistID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Private = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlist", x => x.PlaylistID);
                });

            migrationBuilder.CreateTable(
                name: "CanzonePlaylists",
                columns: table => new
                {
                    SongID = table.Column<int>(type: "INTEGER", nullable: false),
                    PlaylistID = table.Column<int>(type: "INTEGER", nullable: false),
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CanzonePlaylists", x => new { x.SongID, x.PlaylistID });
                    table.ForeignKey(
                        name: "FK_CanzonePlaylists_Canzoni_SongID",
                        column: x => x.SongID,
                        principalTable: "Canzoni",
                        principalColumn: "SongID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CanzonePlaylists_Playlist_PlaylistID",
                        column: x => x.PlaylistID,
                        principalTable: "Playlist",
                        principalColumn: "PlaylistID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CanzonePlaylists_PlaylistID",
                table: "CanzonePlaylists",
                column: "PlaylistID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CanzonePlaylists");

            migrationBuilder.DropTable(
                name: "Canzoni");

            migrationBuilder.DropTable(
                name: "Playlist");
        }
    }
}
