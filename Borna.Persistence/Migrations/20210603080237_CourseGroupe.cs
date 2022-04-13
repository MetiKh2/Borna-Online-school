using Microsoft.EntityFrameworkCore.Migrations;

namespace Borna.Persistence.Migrations
{
    public partial class CourseGroupe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseGroupes",
                columns: table => new
                {
                    GroupeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupeTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ParentID = table.Column<int>(type: "int", nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseGroupes", x => x.GroupeID);
                    table.ForeignKey(
                        name: "FK_CourseGroupes_CourseGroupes_ParentID",
                        column: x => x.ParentID,
                        principalTable: "CourseGroupes",
                        principalColumn: "GroupeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseGroupes_ParentID",
                table: "CourseGroupes",
                column: "ParentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseGroupes");
        }
    }
}
