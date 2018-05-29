using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaCloud.Domain.Migrations
{
    public partial class AddedSeasonPosterPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PosterPath",
                table: "Season",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PosterPath",
                table: "Season");
        }
    }
}
