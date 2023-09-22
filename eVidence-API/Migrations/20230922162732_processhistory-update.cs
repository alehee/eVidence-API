using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eVidence_API.Migrations
{
    public partial class processhistoryupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessesHistory_Accounts_AccountId",
                table: "ProcessesHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcessesHistory_TemporaryEntranceHistory_TemporaryEntranceId",
                table: "ProcessesHistory");

            migrationBuilder.AlterColumn<int>(
                name: "TemporaryEntranceId",
                table: "ProcessesHistory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "ProcessesHistory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessesHistory_Accounts_AccountId",
                table: "ProcessesHistory",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessesHistory_TemporaryEntranceHistory_TemporaryEntranceId",
                table: "ProcessesHistory",
                column: "TemporaryEntranceId",
                principalTable: "TemporaryEntranceHistory",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessesHistory_Accounts_AccountId",
                table: "ProcessesHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcessesHistory_TemporaryEntranceHistory_TemporaryEntranceId",
                table: "ProcessesHistory");

            migrationBuilder.AlterColumn<int>(
                name: "TemporaryEntranceId",
                table: "ProcessesHistory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "ProcessesHistory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessesHistory_Accounts_AccountId",
                table: "ProcessesHistory",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessesHistory_TemporaryEntranceHistory_TemporaryEntranceId",
                table: "ProcessesHistory",
                column: "TemporaryEntranceId",
                principalTable: "TemporaryEntranceHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
