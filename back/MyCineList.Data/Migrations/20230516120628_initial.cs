﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCineList.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IMAGE_MOVIE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImdbPrimaryImageUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IMAGE_MOVIE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MOVIE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageMovieID = table.Column<int>(type: "int", nullable: true),
                    IMDBID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IMDBAggregateRatting = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: true),
                    IMDBTitleTypeID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IMDBTiltleText = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ReleaseYear = table.Column<int>(type: "int", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MOVIE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MOVIE_IMAGE_MOVIE_ImageMovieID",
                        column: x => x.ImageMovieID,
                        principalTable: "IMAGE_MOVIE",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "GENRE_MOVIE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieID = table.Column<int>(type: "int", nullable: true),
                    IMDBGenreID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GENRE_MOVIE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GENRE_MOVIE_MOVIE_MovieID",
                        column: x => x.MovieID,
                        principalTable: "MOVIE",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PLOT_MOVIE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieID = table.Column<int>(type: "int", nullable: true),
                    IMDBPlainText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IMDBLanguageID = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLOT_MOVIE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PLOT_MOVIE_MOVIE_MovieID",
                        column: x => x.MovieID,
                        principalTable: "MOVIE",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PRINCIPAL_CAST_MOVIE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieID = table.Column<int>(type: "int", nullable: true),
                    ImageID = table.Column<int>(type: "int", nullable: true),
                    IMDBNameID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IMDBName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRINCIPAL_CAST_MOVIE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRINCIPAL_CAST_MOVIE_IMAGE_MOVIE_ImageID",
                        column: x => x.ImageID,
                        principalTable: "IMAGE_MOVIE",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PRINCIPAL_CAST_MOVIE_MOVIE_MovieID",
                        column: x => x.MovieID,
                        principalTable: "MOVIE",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PRINCIPAL_CAST_MOVIE_CHARACTER",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrincipalCastMovieID = table.Column<int>(type: "int", nullable: true),
                    IMDBCharacterName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRINCIPAL_CAST_MOVIE_CHARACTER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRINCIPAL_CAST_MOVIE_CHARACTER_PRINCIPAL_CAST_MOVIE_PrincipalCastMovieID",
                        column: x => x.PrincipalCastMovieID,
                        principalTable: "PRINCIPAL_CAST_MOVIE",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GENRE_MOVIE_IMDBGenreID",
                table: "GENRE_MOVIE",
                column: "IMDBGenreID");

            migrationBuilder.CreateIndex(
                name: "IX_GENRE_MOVIE_MovieID",
                table: "GENRE_MOVIE",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_IMAGE_MOVIE_ImdbPrimaryImageUrl",
                table: "IMAGE_MOVIE",
                column: "ImdbPrimaryImageUrl");

            migrationBuilder.CreateIndex(
                name: "IX_MOVIE_ImageMovieID",
                table: "MOVIE",
                column: "ImageMovieID");

            migrationBuilder.CreateIndex(
                name: "IX_MOVIE_IMDBID",
                table: "MOVIE",
                column: "IMDBID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MOVIE_IMDBTiltleText",
                table: "MOVIE",
                column: "IMDBTiltleText");

            migrationBuilder.CreateIndex(
                name: "IX_PLOT_MOVIE_IMDBLanguageID",
                table: "PLOT_MOVIE",
                column: "IMDBLanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_PLOT_MOVIE_MovieID",
                table: "PLOT_MOVIE",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_PRINCIPAL_CAST_MOVIE_ImageID",
                table: "PRINCIPAL_CAST_MOVIE",
                column: "ImageID");

            migrationBuilder.CreateIndex(
                name: "IX_PRINCIPAL_CAST_MOVIE_IMDBName",
                table: "PRINCIPAL_CAST_MOVIE",
                column: "IMDBName");

            migrationBuilder.CreateIndex(
                name: "IX_PRINCIPAL_CAST_MOVIE_MovieID",
                table: "PRINCIPAL_CAST_MOVIE",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_PRINCIPAL_CAST_MOVIE_CHARACTER_IMDBCharacterName",
                table: "PRINCIPAL_CAST_MOVIE_CHARACTER",
                column: "IMDBCharacterName");

            migrationBuilder.CreateIndex(
                name: "IX_PRINCIPAL_CAST_MOVIE_CHARACTER_PrincipalCastMovieID",
                table: "PRINCIPAL_CAST_MOVIE_CHARACTER",
                column: "PrincipalCastMovieID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GENRE_MOVIE");

            migrationBuilder.DropTable(
                name: "PLOT_MOVIE");

            migrationBuilder.DropTable(
                name: "PRINCIPAL_CAST_MOVIE_CHARACTER");

            migrationBuilder.DropTable(
                name: "PRINCIPAL_CAST_MOVIE");

            migrationBuilder.DropTable(
                name: "MOVIE");

            migrationBuilder.DropTable(
                name: "IMAGE_MOVIE");
        }
    }
}
