using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BddAPI.Migrations
{
    /// <inheritdoc />
    public partial class MinorModelUpdates4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LeasePurpose",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeasePurpose",
                table: "Contracts");
        }
    }
}
