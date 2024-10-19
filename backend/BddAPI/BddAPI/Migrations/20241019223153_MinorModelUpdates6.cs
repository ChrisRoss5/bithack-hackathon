using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BddAPI.Migrations
{
    /// <inheritdoc />
    public partial class MinorModelUpdates6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vat",
                table: "CommunityHomes");

            migrationBuilder.AddColumn<double>(
                name: "Vat",
                table: "Contracts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vat",
                table: "Contracts");

            migrationBuilder.AddColumn<double>(
                name: "Vat",
                table: "CommunityHomes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
