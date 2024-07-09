using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalks.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedLengthInKmInsideWalkEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "LengthInKm",
                table: "Walks",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LengthInKm",
                table: "Walks");
        }
    }
}
