using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDataForRegionsAndDifficulty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulty",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("6dbd7c6c-950d-4fe8-89bf-bcdb1528e388"), "Medium" },
                    { new Guid("a099141b-063f-4a33-ac06-d7fd87f83adf"), "Easy" },
                    { new Guid("a6c2969c-ed99-49bc-ae68-d8bc2395a09a"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("377ae495-f8c1-47d8-9c52-cc1127f13f2b"), "AKL", "Auckland", "https://images.pexels.com/photos/831910/pexels-photo-831910.jpeg" },
                    { new Guid("57e08216-a177-4cb7-a15c-aa867b2d3f9f"), "BOP", "Bay of Plenty", "https://images.pexels.com/photos/403781/pexels-photo-403781.jpeg" },
                    { new Guid("906cb139-415a-4bbb-a174-1a1faf9fb1f6"), "NSN", "Nelson", "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg" },
                    { new Guid("a8c61f5c-b1be-4c17-b8fa-b8ea1f03dd83"), "NTL", "Northland", "https://images.pexels.com/photos/1022479/pexels-photo-1022479.jpeg" },
                    { new Guid("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"), "WGN", "Wellington", "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg" },
                    { new Guid("f077a22e-4248-4bf6-b564-c7cf4e250263"), "STL", "Southland", "https://images.pexels.com/photos/3396655/pexels-photo-3396655.jpeg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulty",
                keyColumn: "Id",
                keyValue: new Guid("6dbd7c6c-950d-4fe8-89bf-bcdb1528e388"));

            migrationBuilder.DeleteData(
                table: "Difficulty",
                keyColumn: "Id",
                keyValue: new Guid("a099141b-063f-4a33-ac06-d7fd87f83adf"));

            migrationBuilder.DeleteData(
                table: "Difficulty",
                keyColumn: "Id",
                keyValue: new Guid("a6c2969c-ed99-49bc-ae68-d8bc2395a09a"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("377ae495-f8c1-47d8-9c52-cc1127f13f2b"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("57e08216-a177-4cb7-a15c-aa867b2d3f9f"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("906cb139-415a-4bbb-a174-1a1faf9fb1f6"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a8c61f5c-b1be-4c17-b8fa-b8ea1f03dd83"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f077a22e-4248-4bf6-b564-c7cf4e250263"));
        }
    }
}
