using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LeadTheWay.Data.Migrations
{
    public partial class NewTablesForNodesAndEdges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IntercityLinks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EdgeString = table.Column<string>(nullable: true),
                    Length = table.Column<double>(nullable: false),
                    DurationTicks = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<double>(nullable: false),
                    ServiceClass = table.Column<byte>(nullable: false),
                    TimetableString = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntercityLinks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransportVertices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportVertices", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IntercityLinks");

            migrationBuilder.DropTable(
                name: "TransportVertices");
        }
    }
}
