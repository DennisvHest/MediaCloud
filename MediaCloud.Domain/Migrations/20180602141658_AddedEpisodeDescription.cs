using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaCloud.Domain.Migrations
{
    public partial class AddedEpisodeDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Episodes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Episodes");
        }
    }
}
