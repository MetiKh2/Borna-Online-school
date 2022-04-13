using Microsoft.EntityFrameworkCore.Migrations;

namespace Borna.Persistence.Migrations
{
    public partial class orders2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserID1",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserID1",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserID1",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "CourseID",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserID",
                table: "Orders",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserID",
                table: "Orders",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserID",
                table: "Orders");

            migrationBuilder.AlterColumn<long>(
                name: "UserID",
                table: "Orders",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "CourseID",
                table: "Orders",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserID1",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserID1",
                table: "Orders",
                column: "UserID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserID1",
                table: "Orders",
                column: "UserID1",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
