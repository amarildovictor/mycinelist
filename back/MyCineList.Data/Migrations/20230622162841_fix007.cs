using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCineList.Data.Migrations
{
    /// <inheritdoc />
    public partial class fix007 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_USER_MOVIE_LIST_UserId",
                table: "USER_MOVIE_LIST");

            migrationBuilder.RenameColumn(
                name: "isToEmailNotificate",
                table: "USER_MOVIE_LIST",
                newName: "IsToEmailNotificate");

            migrationBuilder.CreateIndex(
                name: "IX_USER_MOVIE_LIST_UserId_MovieID",
                table: "USER_MOVIE_LIST",
                columns: new[] { "UserId", "MovieID" },
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_USER_MOVIE_LIST_UserId_MovieID",
                table: "USER_MOVIE_LIST");

            migrationBuilder.RenameColumn(
                name: "IsToEmailNotificate",
                table: "USER_MOVIE_LIST",
                newName: "isToEmailNotificate");

            migrationBuilder.CreateIndex(
                name: "IX_USER_MOVIE_LIST_UserId",
                table: "USER_MOVIE_LIST",
                column: "UserId");
        }
    }
}
