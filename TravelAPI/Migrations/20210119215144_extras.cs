using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelAPI.Migrations
{
    public partial class extras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 7);
        }
    }
}
