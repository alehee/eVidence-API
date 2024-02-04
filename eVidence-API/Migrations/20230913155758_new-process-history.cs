using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace eVidence_API.Migrations
{
    public partial class newprocesshistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcessesHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    TemporaryEntranceId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    ProcessId = table.Column<int>(type: "int", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Stop = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessesHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessesHistory_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessesHistory_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessesHistory_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessesHistory_TemporaryEntranceHistory_TemporaryEntranceId",
                        column: x => x.TemporaryEntranceId,
                        principalTable: "TemporaryEntranceHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessesHistory_AccountId",
                table: "ProcessesHistory",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessesHistory_DepartmentId",
                table: "ProcessesHistory",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessesHistory_ProcessId",
                table: "ProcessesHistory",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessesHistory_TemporaryEntranceId",
                table: "ProcessesHistory",
                column: "TemporaryEntranceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessesHistory");
        }
    }
}
