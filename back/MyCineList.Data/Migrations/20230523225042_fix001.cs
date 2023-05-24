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
            migrationBuilder.AlterColumn<decimal>(
                name: "IMDBAggregateRatting",
                table: "MOVIE",
                type: "decimal(4,2)",
                precision: 4,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "IMDBAggregateRatting",
                table: "MOVIE",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,2)",
                oldPrecision: 4,
                oldScale: 2,
                oldNullable: true);
        }
    }
}
