using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaCloud.Domain.Migrations
{
    public partial class AddedBackdrop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BackdropPath",
                table: "Items",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackdropPath",
                table: "Items");
        }
    }
}
