using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoCommon.Migrations
{
    public partial class init9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "Like",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "LikedByCurrentUser",
                table: "Posts",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Like",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "LikedByCurrentUser",
                table: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
