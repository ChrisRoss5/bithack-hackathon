using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BddAPI.Migrations
{
    /// <inheritdoc />
    public partial class HugeModelOptimisations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "ContractDocuments");

            migrationBuilder.DropTable(
                name: "RecordDocuments");

            migrationBuilder.DropTable(
                name: "RequestDocuments");

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocalBoardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalBoardAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalBoardArea = table.Column<double>(type: "float", nullable: false),
                    LeasePurpose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RentPrice = table.Column<double>(type: "float", nullable: false),
                    Bail = table.Column<double>(type: "float", nullable: false),
                    HomeBills = table.Column<double>(type: "float", nullable: false),
                    Vat = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DateOfIssue = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_UserId",
                table: "Contracts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.CreateTable(
                name: "ContractDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Bail = table.Column<double>(type: "float", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfIssue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HomeBills = table.Column<double>(type: "float", nullable: false),
                    LeasePurpose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalBoardAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalBoardArea = table.Column<double>(type: "float", nullable: false),
                    LocalBoardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RentPrice = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Vat = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractDocuments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecordDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConditionAfter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConditionBefore = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DamageDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfInspection = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfIssue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LocalBoardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Problem = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordDocuments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfIssue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LocalBoardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestDocuments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommunityCenterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RecordDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RequestDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdditionalNotes = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpectedNumberOfPeople = table.Column<int>(type: "int", nullable: true),
                    RangeModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_CommunityCenters_CommunityCenterId",
                        column: x => x.CommunityCenterId,
                        principalTable: "CommunityCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_ContractDocuments_ContractDocumentId",
                        column: x => x.ContractDocumentId,
                        principalTable: "ContractDocuments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservations_RecordDocuments_RecordDocumentId",
                        column: x => x.RecordDocumentId,
                        principalTable: "RecordDocuments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservations_RequestDocuments_RequestDocumentId",
                        column: x => x.RequestDocumentId,
                        principalTable: "RequestDocuments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractDocuments_UserId",
                table: "ContractDocuments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordDocuments_UserId",
                table: "RecordDocuments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestDocuments_UserId",
                table: "RequestDocuments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CommunityCenterId",
                table: "Reservations",
                column: "CommunityCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ContractDocumentId",
                table: "Reservations",
                column: "ContractDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RecordDocumentId",
                table: "Reservations",
                column: "RecordDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RequestDocumentId",
                table: "Reservations",
                column: "RequestDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");
        }
    }
}
