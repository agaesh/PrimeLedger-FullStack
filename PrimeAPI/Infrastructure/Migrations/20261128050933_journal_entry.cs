using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class journal_entry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JournalEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    entry_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JournalEntryLine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    journal_entry_id = table.Column<int>(type: "int", nullable: false),
                    account_id = table.Column<int>(type: "int", nullable: false),
                    debit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    credit = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntryLine", x => x.Id);
                    table.CheckConstraint("CK_JournalEntryLine_OneSidedAmount", "([debit] >= 0 AND [credit] >= 0) AND (([debit] > 0 AND [credit] = 0) OR ([credit] > 0 AND [debit] = 0))");
                    table.ForeignKey(
                        name: "FK_JournalEntryLine_GlAccounts_account_id",
                        column: x => x.account_id,
                        principalTable: "GlAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JournalEntryLine_JournalEntries_journal_entry_id",
                        column: x => x.journal_entry_id,
                        principalTable: "JournalEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntryLine_account_id",
                table: "JournalEntryLine",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntryLine_journal_entry_id",
                table: "JournalEntryLine",
                column: "journal_entry_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JournalEntryLine");

            migrationBuilder.DropTable(
                name: "JournalEntries");
        }
    }
}
