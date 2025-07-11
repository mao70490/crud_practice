using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudPractice1.Migrations
{
    /// <inheritdoc />
    public partial class Rating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Awards",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<long>(
                name: "BoxOffice",
                table: "Movie",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Budget",
                table: "Movie",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Cast",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Director",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DurationMinutes",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "ImdbScore",
                table: "Movie",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsStreaming",
                table: "Movie",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OriginalTitle",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PosterUrl",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductionCompany",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Rating",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Synopsis",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TrailerUrl",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Awards",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "BoxOffice",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Budget",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Cast",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Director",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "DurationMinutes",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "ImdbScore",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "IsStreaming",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "OriginalTitle",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "PosterUrl",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "ProductionCompany",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Synopsis",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "TrailerUrl",
                table: "Movie");
        }
    }
}
