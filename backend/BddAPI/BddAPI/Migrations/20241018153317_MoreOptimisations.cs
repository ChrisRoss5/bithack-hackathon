using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BddAPI.Migrations
{
    /// <inheritdoc />
    public partial class MoreOptimisations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CommunityCenters",
                table: "CommunityCenters");

            migrationBuilder.DropColumn(
                name: "Bail",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "DateFrom",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "DateTo",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "HomeBills",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "LeasePurpose",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "LocalBoardAddress",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "LocalBoardArea",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "LocalBoardName",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "RentPrice",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Vat",
                table: "Contracts");

            migrationBuilder.RenameTable(
                name: "CommunityCenters",
                newName: "CommunityHomes");

            migrationBuilder.AlterColumn<string>(
                name: "To",
                table: "RangeModels",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "From",
                table: "RangeModels",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<Guid>(
                name: "CommunityHomeId",
                table: "Contracts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<double>(
                name: "HomeBills",
                table: "CommunityHomes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RentPrice",
                table: "CommunityHomes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Vat",
                table: "CommunityHomes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommunityHomes",
                table: "CommunityHomes",
                column: "Id");

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

            migrationBuilder.CreateTable(
                name: "Records",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConditionBefore = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConditionAfter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DamageDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Problems = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Records", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Records_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Records_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_CommunityHomeId",
                table: "Contracts",
                column: "CommunityHomeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractRangeModels_ContractId",
                table: "ContractRangeModels",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractRangeModels_RangeModelId",
                table: "ContractRangeModels",
                column: "RangeModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_ContractId",
                table: "Records",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_UserId",
                table: "Records",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_CommunityHomes_CommunityHomeId",
                table: "Contracts",
                column: "CommunityHomeId",
                principalTable: "CommunityHomes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_CommunityHomes_CommunityHomeId",
                table: "Contracts");

            migrationBuilder.DropTable(
                name: "ContractRangeModels");

            migrationBuilder.DropTable(
                name: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_CommunityHomeId",
                table: "Contracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommunityHomes",
                table: "CommunityHomes");

            migrationBuilder.DropColumn(
                name: "CommunityHomeId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "HomeBills",
                table: "CommunityHomes");

            migrationBuilder.DropColumn(
                name: "RentPrice",
                table: "CommunityHomes");

            migrationBuilder.DropColumn(
                name: "Vat",
                table: "CommunityHomes");

            migrationBuilder.RenameTable(
                name: "CommunityHomes",
                newName: "CommunityCenters");

            migrationBuilder.AlterColumn<DateTime>(
                name: "To",
                table: "RangeModels",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "From",
                table: "RangeModels",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<double>(
                name: "Bail",
                table: "Contracts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateFrom",
                table: "Contracts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTo",
                table: "Contracts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "HomeBills",
                table: "Contracts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "LeasePurpose",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LocalBoardAddress",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "LocalBoardArea",
                table: "Contracts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "LocalBoardName",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "RentPrice",
                table: "Contracts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Vat",
                table: "Contracts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommunityCenters",
                table: "CommunityCenters",
                column: "Id");
        }
    }
}
