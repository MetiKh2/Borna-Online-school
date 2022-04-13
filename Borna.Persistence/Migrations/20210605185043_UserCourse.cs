using Microsoft.EntityFrameworkCore.Migrations;

namespace Borna.Persistence.Migrations
{
    public partial class UserCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseEpisodes_Courses_CourseID",
                table: "CourseEpisodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseGroupes_ParentGroupeID",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Users_TeacherID",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_Permission_PermissionID",
                table: "RolePermission");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_Roles_RoleID",
                table: "RolePermission");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInRoles_Roles_RoleID",
                table: "UserInRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInRoles_Users_UserID",
                table: "UserInRoles");

            migrationBuilder.CreateTable(
                name: "UserCourses",
                columns: table => new
                {
                    UC_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    CourseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourses", x => x.UC_ID);
                    table.ForeignKey(
                        name: "FK_UserCourses_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserCourses_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_CourseID",
                table: "UserCourses",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_UserID",
                table: "UserCourses",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEpisodes_Courses_CourseID",
                table: "CourseEpisodes",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseGroupes_ParentGroupeID",
                table: "Courses",
                column: "ParentGroupeID",
                principalTable: "CourseGroupes",
                principalColumn: "GroupeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Users_TeacherID",
                table: "Courses",
                column: "TeacherID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserID",
                table: "Orders",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Permission_PermissionID",
                table: "RolePermission",
                column: "PermissionID",
                principalTable: "Permission",
                principalColumn: "PermissionID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Roles_RoleID",
                table: "RolePermission",
                column: "RoleID",
                principalTable: "Roles",
                principalColumn: "RoleID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInRoles_Roles_RoleID",
                table: "UserInRoles",
                column: "RoleID",
                principalTable: "Roles",
                principalColumn: "RoleID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInRoles_Users_UserID",
                table: "UserInRoles",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseEpisodes_Courses_CourseID",
                table: "CourseEpisodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseGroupes_ParentGroupeID",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Users_TeacherID",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_Permission_PermissionID",
                table: "RolePermission");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_Roles_RoleID",
                table: "RolePermission");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInRoles_Roles_RoleID",
                table: "UserInRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInRoles_Users_UserID",
                table: "UserInRoles");

            migrationBuilder.DropTable(
                name: "UserCourses");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEpisodes_Courses_CourseID",
                table: "CourseEpisodes",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseGroupes_ParentGroupeID",
                table: "Courses",
                column: "ParentGroupeID",
                principalTable: "CourseGroupes",
                principalColumn: "GroupeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Users_TeacherID",
                table: "Courses",
                column: "TeacherID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserID",
                table: "Orders",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Permission_PermissionID",
                table: "RolePermission",
                column: "PermissionID",
                principalTable: "Permission",
                principalColumn: "PermissionID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Roles_RoleID",
                table: "RolePermission",
                column: "RoleID",
                principalTable: "Roles",
                principalColumn: "RoleID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInRoles_Roles_RoleID",
                table: "UserInRoles",
                column: "RoleID",
                principalTable: "Roles",
                principalColumn: "RoleID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInRoles_Users_UserID",
                table: "UserInRoles",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
