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
                    EleCanzoniSongID = table.Column<int>(type: "INTEGER", nullable: false),
                    ElePlaylistPlaylistID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CanzonePlaylist", x => new { x.EleCanzoniSongID, x.ElePlaylistPlaylistID });
                    table.ForeignKey(
                        name: "FK_CanzonePlaylist_Canzoni_EleCanzoniSongID",
                        column: x => x.EleCanzoniSongID,
                        principalTable: "Canzoni",
                        principalColumn: "SongID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CanzonePlaylist_Playlist_ElePlaylistPlaylistID",
                        column: x => x.ElePlaylistPlaylistID,
                        principalTable: "Playlist",
                        principalColumn: "PlaylistID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CanzonePlaylist_ElePlaylistPlaylistID",
                table: "CanzonePlaylist",
                column: "ElePlaylistPlaylistID");
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
