using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eVidence_API.Migrations
{
    public partial class group_department_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Groups_GroupId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_GroupId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Departments");

            migrationBuilder.CreateTable(
                name: "DepartmentGroup",
                columns: table => new
                {
                    DepartmentsId = table.Column<int>(type: "int", nullable: false),
                    GroupsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentGroup", x => new { x.DepartmentsId, x.GroupsId });
                    table.ForeignKey(
                        name: "FK_DepartmentGroup_Departments_DepartmentsId",
                        column: x => x.DepartmentsId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentGroup_Groups_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentGroup_GroupsId",
                table: "DepartmentGroup",
                column: "GroupsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentGroup");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_GroupId",
                table: "Departments",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Groups_GroupId",
                table: "Departments",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
