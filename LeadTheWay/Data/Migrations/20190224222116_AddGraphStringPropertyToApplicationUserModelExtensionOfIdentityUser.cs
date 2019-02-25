using Microsoft.EntityFrameworkCore.Migrations;

namespace LeadTheWay.Data.Migrations
{
    public partial class AddGraphStringPropertyToApplicationUserModelExtensionOfIdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GraphString",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GraphString",
                table: "AspNetUsers");
        }
    }
}
