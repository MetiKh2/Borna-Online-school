using Microsoft.EntityFrameworkCore.Migrations;

namespace Borna.Persistence.Migrations
{
    public partial class Votes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseVotes",
                columns: table => new
                {
                    VoteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Vote = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseVotes", x => x.VoteID);
                    table.ForeignKey(
                        name: "FK_CourseVotes_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseVotes_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseVotes_CourseID",
                table: "CourseVotes",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseVotes_UserID",
                table: "CourseVotes",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseVotes");
        }
    }
}
