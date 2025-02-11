using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicLibrary.Migrations
{
    /// <inheritdoc />
    public partial class RecreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "artist",
                columns: table => new
                {
                    artist_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stagename = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    label = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    origin = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__artist__6CD04001AE75753E", x => x.artist_id);
                });

            migrationBuilder.CreateTable(
                name: "genre",
                columns: table => new
                {
                    genre_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__genre__18428D428E986D21", x => x.genre_id);
                });

            migrationBuilder.CreateTable(
                name: "song",
                columns: table => new
                {
                    song_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    artist_id = table.Column<int>(type: "int", nullable: false),
                    genre_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__song__A535AE1C9B24CB56", x => x.song_id);
                    table.ForeignKey(
                        name: "FK__song__artist_id__60A75C0F",
                        column: x => x.artist_id,
                        principalTable: "artist",
                        principalColumn: "artist_id");
                    table.ForeignKey(
                        name: "FK__song__genre_id__619B8048",
                        column: x => x.genre_id,
                        principalTable: "genre",
                        principalColumn: "genre_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_song_artist_id",
                table: "song",
                column: "artist_id");

            migrationBuilder.CreateIndex(
                name: "IX_song_genre_id",
                table: "song",
                column: "genre_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "song");

            migrationBuilder.DropTable(
                name: "artist");

            migrationBuilder.DropTable(
                name: "genre");
        }
    }
}
