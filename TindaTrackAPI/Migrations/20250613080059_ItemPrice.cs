using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TindaTrackAPI.Migrations
{
    /// <inheritdoc />
    public partial class ItemPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Barangay_Municipality_MunicipalityId",
                table: "Barangay");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Barangay_BarangayId",
                table: "Sales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Municipality",
                table: "Municipality");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Barangay",
                table: "Barangay");

            migrationBuilder.RenameTable(
                name: "Municipality",
                newName: "Municipalities");

            migrationBuilder.RenameTable(
                name: "Barangay",
                newName: "Barangays");

            migrationBuilder.RenameIndex(
                name: "IX_Barangay_MunicipalityId",
                table: "Barangays",
                newName: "IX_Barangays_MunicipalityId");

            migrationBuilder.AddColumn<int>(
                name: "UnitPrice",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Municipalities",
                table: "Municipalities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Barangays",
                table: "Barangays",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Barangays_Municipalities_MunicipalityId",
                table: "Barangays",
                column: "MunicipalityId",
                principalTable: "Municipalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Barangays_BarangayId",
                table: "Sales",
                column: "BarangayId",
                principalTable: "Barangays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Barangays_Municipalities_MunicipalityId",
                table: "Barangays");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Barangays_BarangayId",
                table: "Sales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Municipalities",
                table: "Municipalities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Barangays",
                table: "Barangays");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "Items");

            migrationBuilder.RenameTable(
                name: "Municipalities",
                newName: "Municipality");

            migrationBuilder.RenameTable(
                name: "Barangays",
                newName: "Barangay");

            migrationBuilder.RenameIndex(
                name: "IX_Barangays_MunicipalityId",
                table: "Barangay",
                newName: "IX_Barangay_MunicipalityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Municipality",
                table: "Municipality",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Barangay",
                table: "Barangay",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Barangay_Municipality_MunicipalityId",
                table: "Barangay",
                column: "MunicipalityId",
                principalTable: "Municipality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Barangay_BarangayId",
                table: "Sales",
                column: "BarangayId",
                principalTable: "Barangay",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
