using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinimalRSSReader.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionToRSSFeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RSSFeeds",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "PubDate",
                table: "RSSFeeds",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "RSSFeeds");

            migrationBuilder.DropColumn(
                name: "PubDate",
                table: "RSSFeeds");
        }
    }
}
