using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BddAPI.Migrations
{
    /// <inheritdoc />
    public partial class MinorModelUpdates3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFree",
                table: "CommunityHomes");

            migrationBuilder.AddColumn<bool>(
                name: "IsFree",
                table: "Contracts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFree",
                table: "Contracts");

            migrationBuilder.AddColumn<bool>(
                name: "IsFree",
                table: "CommunityHomes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
