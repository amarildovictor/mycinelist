using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCineList.Data.Migrations
{
    /// <inheritdoc />
    public partial class fix001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GENRE_IMDBGenreID",
                table: "GENRE");

            migrationBuilder.CreateIndex(
                name: "IX_GENRE_IMDBGenreID",
                table: "GENRE",
                column: "IMDBGenreID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GENRE_IMDBGenreID",
                table: "GENRE");

            migrationBuilder.CreateIndex(
                name: "IX_GENRE_IMDBGenreID",
                table: "GENRE",
                column: "IMDBGenreID");
        }
    }
}
