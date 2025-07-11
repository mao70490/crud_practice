using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudPractice1.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMovieModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStreaming",
                table: "Movie");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsStreaming",
                table: "Movie",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
