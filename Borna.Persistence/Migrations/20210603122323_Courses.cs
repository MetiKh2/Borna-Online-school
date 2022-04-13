using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Borna.Persistence.Migrations
{
    public partial class Courses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseLanguages",
                columns: table => new
                {
                    LanguageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseLanguages", x => x.LanguageID);
                });

            migrationBuilder.CreateTable(
                name: "CourseLevels",
                columns: table => new
                {
                    LevelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevelTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseLevels", x => x.LevelID);
                });

            migrationBuilder.CreateTable(
                name: "CourseStatuses",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStatuses", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevelID = table.Column<int>(type: "int", nullable: false),
                    LanguageID = table.Column<int>(type: "int", nullable: false),
                    ParentGroupeID = table.Column<int>(type: "int", nullable: false),
                    SubGroupeID = table.Column<int>(type: "int", nullable: true),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    TeacherID = table.Column<int>(type: "int", nullable: false),
                    CourseTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CourseDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    CourseImage = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CourseLevelLevelID = table.Column<int>(type: "int", nullable: true),
                    CourseLanguageLanguageID = table.Column<int>(type: "int", nullable: true),
                    CourseStatusStatusID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseID);
                    table.ForeignKey(
                        name: "FK_Courses_CourseGroupes_ParentGroupeID",
                        column: x => x.ParentGroupeID,
                        principalTable: "CourseGroupes",
                        principalColumn: "GroupeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_CourseGroupes_SubGroupeID",
                        column: x => x.SubGroupeID,
                        principalTable: "CourseGroupes",
                        principalColumn: "GroupeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Courses_CourseLanguages_CourseLanguageLanguageID",
                        column: x => x.CourseLanguageLanguageID,
                        principalTable: "CourseLanguages",
                        principalColumn: "LanguageID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Courses_CourseLevels_CourseLevelLevelID",
                        column: x => x.CourseLevelLevelID,
                        principalTable: "CourseLevels",
                        principalColumn: "LevelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Courses_CourseStatuses_CourseStatusStatusID",
                        column: x => x.CourseStatusStatusID,
                        principalTable: "CourseStatuses",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Courses_Users_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseEpisodes",
                columns: table => new
                {
                    EpisodeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    EpisodeTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EpisodeFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFree = table.Column<bool>(type: "bit", nullable: false),
                    EpisodeTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseEpisodes", x => x.EpisodeID);
                    table.ForeignKey(
                        name: "FK_CourseEpisodes_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseEpisodes_CourseID",
                table: "CourseEpisodes",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseLanguageLanguageID",
                table: "Courses",
                column: "CourseLanguageLanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseLevelLevelID",
                table: "Courses",
                column: "CourseLevelLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseStatusStatusID",
                table: "Courses",
                column: "CourseStatusStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ParentGroupeID",
                table: "Courses",
                column: "ParentGroupeID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SubGroupeID",
                table: "Courses",
                column: "SubGroupeID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TeacherID",
                table: "Courses",
                column: "TeacherID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseEpisodes");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "CourseLanguages");

            migrationBuilder.DropTable(
                name: "CourseLevels");

            migrationBuilder.DropTable(
                name: "CourseStatuses");
        }
    }
}
