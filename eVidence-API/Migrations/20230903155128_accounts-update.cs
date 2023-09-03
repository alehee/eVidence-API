using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eVidence_API.Migrations
{
    public partial class accountsupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Enter",
                table: "TemporaryEntranceHistory",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Accounts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enter",
                table: "TemporaryEntranceHistory");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Accounts");
        }
    }
}
