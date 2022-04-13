using Microsoft.EntityFrameworkCore.Migrations;

namespace Borna.Persistence.Migrations
{
    public partial class isFreeInEpisodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "CourseEpisodes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "CourseEpisodes");
        }
    }
}
