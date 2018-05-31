using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaCloud.Domain.Migrations
{
    public partial class AddedEpisodeStillPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StillPath",
                table: "Episodes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StillPath",
                table: "Episodes");
        }
    }
}
