using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scraper.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "data_head",
                table: "notices");

            migrationBuilder.DropColumn(
                name: "data_meta",
                table: "notices");

            migrationBuilder.DropColumn(
                name: "data_title",
                table: "notices");

            migrationBuilder.AddColumn<string>(
                name: "error_scraping",
                table: "notices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "headers",
                table: "notices",
                type: "jsonb",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "error_scraping",
                table: "notices");

            migrationBuilder.DropColumn(
                name: "headers",
                table: "notices");

            migrationBuilder.AddColumn<string>(
                name: "data_head",
                table: "notices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "data_meta",
                table: "notices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "data_title",
                table: "notices",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
