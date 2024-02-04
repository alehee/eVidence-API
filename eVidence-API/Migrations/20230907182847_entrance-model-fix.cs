using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eVidence_API.Migrations
{
    public partial class entrancemodelfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Enter",
                table: "EntranceHistory",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enter",
                table: "EntranceHistory");
        }
    }
}
