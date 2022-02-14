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
                name: "CanzonePlaylist",
                columns: table => new
                {
                    CanzonesSongID = table.Column<int>(type: "INTEGER", nullable: false),
                    PlaylistsPlaylistID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CanzonePlaylist", x => new { x.CanzonesSongID, x.PlaylistsPlaylistID });
                    table.ForeignKey(
                        name: "FK_CanzonePlaylist_Canzoni_CanzonesSongID",
                        column: x => x.CanzonesSongID,
                        principalTable: "Canzoni",
                        principalColumn: "SongID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CanzonePlaylist_Playlist_PlaylistsPlaylistID",
                        column: x => x.PlaylistsPlaylistID,
                        principalTable: "Playlist",
                        principalColumn: "PlaylistID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Canzoni",
                columns: new[] { "SongID", "AlbumName", "DownnloadNumber", "Duration", "IDUserUploader", "SongTitle" },
                values: new object[] { 1, "Album1", 0, 120, 1, "Canzone1" });

            migrationBuilder.InsertData(
                table: "Playlist",
                columns: new[] { "PlaylistID", "Name", "Private", "UserID" },
                values: new object[] { 1, "Playlist1", true, 1 });

            migrationBuilder.InsertData(
                table: "CanzonePlaylist",
                columns: new[] { "CanzonesSongID", "PlaylistsPlaylistID" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_CanzonePlaylist_PlaylistsPlaylistID",
                table: "CanzonePlaylist",
                column: "PlaylistsPlaylistID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CanzonePlaylist");

            migrationBuilder.DropTable(
                name: "Canzoni");

            migrationBuilder.DropTable(
                name: "Playlist");
        }
    }
}
