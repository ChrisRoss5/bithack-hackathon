using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BddAPI.Migrations
{
    /// <inheritdoc />
    public partial class ModelsUpdatedAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RangeModels",
                table: "RangeModels");

            migrationBuilder.RenameTable(
                name: "RangeModels",
                newName: "ContractRanges");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractRanges",
                table: "ContractRanges",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractRanges",
                table: "ContractRanges");

            migrationBuilder.RenameTable(
                name: "ContractRanges",
                newName: "RangeModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RangeModels",
                table: "RangeModels",
                column: "Id");
        }
    }
}
