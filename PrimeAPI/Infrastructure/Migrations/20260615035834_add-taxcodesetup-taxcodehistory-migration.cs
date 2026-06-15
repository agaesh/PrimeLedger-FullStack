using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addtaxcodesetuptaxcodehistorymigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaxCodeSetup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tax_rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    tax_type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxCodeSetup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxCodeHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tax_code_setup_id = table.Column<int>(type: "int", nullable: false),
                    effective_from = table.Column<DateTime>(type: "datetime2", nullable: false),
                    effective_to = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    tax_direction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tax_account_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxCodeHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxCodeHistory_TaxCodeSetup_tax_code_setup_id",
                        column: x => x.tax_code_setup_id,
                        principalTable: "TaxCodeSetup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaxCodeHistory_tax_code_setup_id",
                table: "TaxCodeHistory",
                column: "tax_code_setup_id");

            migrationBuilder.CreateIndex(
                name: "IX_TaxCodeSetup_code",
                table: "TaxCodeSetup",
                column: "code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxCodeHistory");

            migrationBuilder.DropTable(
                name: "TaxCodeSetup");
        }
    }
}
