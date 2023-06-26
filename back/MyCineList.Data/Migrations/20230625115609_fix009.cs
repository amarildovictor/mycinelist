using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCineList.Data.Migrations
{
    /// <inheritdoc />
    public partial class fix009 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "USER_MOVIE_LIST");

            migrationBuilder.CreateTable(
                name: "USER_MOVIES_RATING",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MovieID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_MOVIES_RATING", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USER_MOVIES_RATING_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_USER_MOVIES_RATING_MOVIE_MovieID",
                        column: x => x.MovieID,
                        principalTable: "MOVIE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_USER_MOVIES_RATING_MovieID",
                table: "USER_MOVIES_RATING",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_MOVIES_RATING_UserId_MovieID",
                table: "USER_MOVIES_RATING",
                columns: new[] { "UserId", "MovieID" },
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USER_MOVIES_RATING");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "USER_MOVIE_LIST",
                type: "int",
                nullable: true);
        }
    }
}
