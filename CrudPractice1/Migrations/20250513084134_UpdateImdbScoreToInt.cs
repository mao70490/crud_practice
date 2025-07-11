using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudPractice1.Migrations
{
    /// <inheritdoc />
    public partial class UpdateImdbScoreToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ImdbScore",
                table: "Movie2",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "ImdbScore",
                table: "Movie2",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
