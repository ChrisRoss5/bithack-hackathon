using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BddAPI.Migrations
{
    /// <inheritdoc />
    public partial class MoreOptimisation2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractRangeModels");

            migrationBuilder.AddColumn<Guid>(
                name: "ContractId",
                table: "RangeModels",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "RangeModels");

            migrationBuilder.CreateTable(
                name: "ContractRangeModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RangeModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractRangeModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractRangeModels_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractRangeModels_RangeModels_RangeModelId",
                        column: x => x.RangeModelId,
                        principalTable: "RangeModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractRangeModels_ContractId",
                table: "ContractRangeModels",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractRangeModels_RangeModelId",
                table: "ContractRangeModels",
                column: "RangeModelId");
        }
    }
}
