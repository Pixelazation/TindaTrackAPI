using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TindaTrackAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveGeoJSON : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GeoJsonBoundary",
                table: "Municipalities");

            migrationBuilder.DropColumn(
                name: "GeoJsonBoundary",
                table: "Barangays");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GeoJsonBoundary",
                table: "Municipalities",
                type: "json",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "GeoJsonBoundary",
                table: "Barangays",
                type: "json",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
