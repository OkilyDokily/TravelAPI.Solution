using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelAPI.Migrations
{
    public partial class inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Rating = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ReviewId", "City", "Country", "Rating", "UserName" },
                values: new object[] { 1, "Portland", "USA", 3, "Pat Benatar" });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ReviewId", "City", "Country", "Rating", "UserName" },
                values: new object[] { 2, "Moskow", "Russia", 4, "Pat Benatar" });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ReviewId", "City", "Country", "Rating", "UserName" },
                values: new object[] { 3, "Sydney", "Australia", 5, "Charles Barkely" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");
        }
    }
}
