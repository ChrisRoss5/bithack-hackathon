using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BddAPI.Migrations
{
    /// <inheritdoc />
    public partial class MinorModelUpdates2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "CommunityHomes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "CommunityHomes");
        }
    }
}
