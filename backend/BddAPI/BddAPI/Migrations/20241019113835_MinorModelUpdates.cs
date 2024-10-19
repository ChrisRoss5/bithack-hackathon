using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BddAPI.Migrations
{
    /// <inheritdoc />
    public partial class MinorModelUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFree",
                table: "CommunityHomes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFree",
                table: "CommunityHomes");
        }
    }
}
