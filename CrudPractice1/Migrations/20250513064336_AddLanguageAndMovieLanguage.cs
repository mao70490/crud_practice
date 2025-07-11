using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudPractice1.Migrations
{
    /// <inheritdoc />
    public partial class AddLanguageAndMovieLanguage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupportedLanguages",
                table: "Movie2");

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "MovieLanguages",
                columns: table => new
                {
                    Movie2Id = table.Column<int>(type: "int", nullable: false),
                    LanguageCode = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieLanguages", x => new { x.Movie2Id, x.LanguageCode });
                    table.ForeignKey(
                        name: "FK_MovieLanguages_Languages_LanguageCode",
                        column: x => x.LanguageCode,
                        principalTable: "Languages",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieLanguages_Movie2_Movie2Id",
                        column: x => x.Movie2Id,
                        principalTable: "Movie2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieLanguages_LanguageCode",
                table: "MovieLanguages",
                column: "LanguageCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieLanguages");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.AddColumn<string>(
                name: "SupportedLanguages",
                table: "Movie2",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
