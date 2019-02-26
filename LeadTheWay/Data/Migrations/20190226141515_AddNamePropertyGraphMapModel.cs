using Microsoft.EntityFrameworkCore.Migrations;

namespace LeadTheWay.Data.Migrations
{
    public partial class AddNamePropertyGraphMapModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "GraphMaps",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "GraphMaps");
        }
    }
}
