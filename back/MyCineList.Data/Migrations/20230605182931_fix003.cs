using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCineList.Data.Migrations
{
    /// <inheritdoc />
    public partial class fix003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ConsidererToResizingFunction",
                table: "IMAGE_MOVIE",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MediumImageUrl",
                table: "IMAGE_MOVIE",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SmallImageUrl",
                table: "IMAGE_MOVIE",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsidererToResizingFunction",
                table: "IMAGE_MOVIE");

            migrationBuilder.DropColumn(
                name: "MediumImageUrl",
                table: "IMAGE_MOVIE");

            migrationBuilder.DropColumn(
                name: "SmallImageUrl",
                table: "IMAGE_MOVIE");
        }
    }
}
