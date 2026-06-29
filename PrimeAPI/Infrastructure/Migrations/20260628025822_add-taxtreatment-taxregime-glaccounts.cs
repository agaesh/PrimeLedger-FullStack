using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addtaxtreatmenttaxregimeglaccounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxCodeHistory");

            migrationBuilder.DropTable(
                name: "TaxCodeSetup");

            migrationBuilder.CreateTable(
                name: "TaxRegime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodeType = table.Column<int>(type: "int", nullable: false),
                    effective_from = table.Column<DateTime>(type: "datetime2", nullable: false),
                    effective_to = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxRegime", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxTreatment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tax_percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    purchase_gl_id = table.Column<int>(type: "int", nullable: false),
                    sales_gl_id = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxTreatment", x => x.Id);
                    table.CheckConstraint("CK_TaxTreatment_DifferentAccounts", "[purchase_gl_id] <> [sales_gl_id]");
                    table.ForeignKey(
                        name: "FK_TaxTreatment_GlAccounts_purchase_gl_id",
                        column: x => x.purchase_gl_id,
                        principalTable: "GlAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaxTreatment_GlAccounts_sales_gl_id",
                        column: x => x.sales_gl_id,
                        principalTable: "GlAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaxTreatment_code",
                table: "TaxTreatment",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaxTreatment_purchase_gl_id",
                table: "TaxTreatment",
                column: "purchase_gl_id");

            migrationBuilder.CreateIndex(
                name: "IX_TaxTreatment_sales_gl_id",
                table: "TaxTreatment",
                column: "sales_gl_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxRegime");

            migrationBuilder.DropTable(
                name: "TaxTreatment");

            migrationBuilder.CreateTable(
                name: "TaxCodeSetup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tax_rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    tax_type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    purchase_account_id = table.Column<int>(type: "int", nullable: true),
                    sales_account_id = table.Column<int>(type: "int", nullable: true),
                    effective_from = table.Column<DateTime>(type: "datetime2", nullable: false),
                    effective_to = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    tax_code_setup_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxCodeHistory", x => x.Id);
                    table.CheckConstraint("CK_TaxCodeHistory_DifferentAccounts", "[purchase_account_id] <> [sales_account_id]");
                    table.ForeignKey(
                        name: "FK_TaxCodeHistory_GlAccounts_purchase_account_id",
                        column: x => x.purchase_account_id,
                        principalTable: "GlAccounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaxCodeHistory_GlAccounts_sales_account_id",
                        column: x => x.sales_account_id,
                        principalTable: "GlAccounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaxCodeHistory_TaxCodeSetup_tax_code_setup_id",
                        column: x => x.tax_code_setup_id,
                        principalTable: "TaxCodeSetup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaxCodeHistory_purchase_account_id",
                table: "TaxCodeHistory",
                column: "purchase_account_id");

            migrationBuilder.CreateIndex(
                name: "IX_TaxCodeHistory_sales_account_id",
                table: "TaxCodeHistory",
                column: "sales_account_id");

            migrationBuilder.CreateIndex(
                name: "IX_TaxCodeHistory_tax_code_setup_id_purchase_account_id",
                table: "TaxCodeHistory",
                columns: new[] { "tax_code_setup_id", "purchase_account_id" },
                unique: true,
                filter: "[purchase_account_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TaxCodeSetup_code",
                table: "TaxCodeSetup",
                column: "code",
                unique: true);
        }
    }
}
