using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CrudPractice1.Migrations
{
    /// <inheritdoc />
    public partial class SeedLanguages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { "en", "英文" },
                    { "jp", "日文" },
                    { "zh", "中文" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "jp");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "zh");
        }
    }
}
