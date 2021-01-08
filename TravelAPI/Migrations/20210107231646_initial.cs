using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelAPI.Migrations
{
    public partial class initial : Migration
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
                    UserName = table.Column<string>(nullable: false),
                    Country = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ReviewId", "City", "Country", "Rating", "UserName" },
                values: new object[,]
                {
                    { 1, "Portland", "USA", 3, "Pat Benatar" },
                    { 2, "Moskow", "Russia", 4, "Pat Benatar" },
                    { 3, "Sydney", "Australia", 5, "Yolo Banksy" },
                    { 4, "Sydney", "Australia", 5, "Kate Austen" },
                    { 5, "Sydney", "Australia", 5, "Kaitlinn Bennet" },
                    { 6, "Sydney", "Australia", 5, "Hosia" },
                    { 7, "Sydney", "Australia", 5, "Charlie Bonkadonk" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");
        }
    }
}
