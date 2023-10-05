using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoCommon.Migrations
{
    public partial class init10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LikedByCurrentUser",
                table: "Posts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "LikedByCurrentUser",
                table: "Posts",
                type: "bit",
                nullable: true);
        }
    }
}
