using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removetaxaccountidintroducedualaccountid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TaxCodeHistory_tax_code_setup_id",
                table: "TaxCodeHistory");

            migrationBuilder.DropColumn(
                name: "tax_direction",
                table: "TaxCodeHistory");

            migrationBuilder.RenameColumn(
                name: "tax_account_id",
                table: "TaxCodeHistory",
                newName: "sales_account_id");

            migrationBuilder.AddColumn<int>(
                name: "purchase_account_id",
                table: "TaxCodeHistory",
                type: "int",
                nullable: true);

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

            migrationBuilder.AddCheckConstraint(
                name: "CK_TaxCodeHistory_DifferentAccounts",
                table: "TaxCodeHistory",
                sql: "[purchase_account_id] <> [sales_account_id]");

            migrationBuilder.AddForeignKey(
                name: "FK_TaxCodeHistory_GlAccounts_purchase_account_id",
                table: "TaxCodeHistory",
                column: "purchase_account_id",
                principalTable: "GlAccounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaxCodeHistory_GlAccounts_sales_account_id",
                table: "TaxCodeHistory",
                column: "sales_account_id",
                principalTable: "GlAccounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaxCodeHistory_GlAccounts_purchase_account_id",
                table: "TaxCodeHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxCodeHistory_GlAccounts_sales_account_id",
                table: "TaxCodeHistory");

            migrationBuilder.DropIndex(
                name: "IX_TaxCodeHistory_purchase_account_id",
                table: "TaxCodeHistory");

            migrationBuilder.DropIndex(
                name: "IX_TaxCodeHistory_sales_account_id",
                table: "TaxCodeHistory");

            migrationBuilder.DropIndex(
                name: "IX_TaxCodeHistory_tax_code_setup_id_purchase_account_id",
                table: "TaxCodeHistory");

            migrationBuilder.DropCheckConstraint(
                name: "CK_TaxCodeHistory_DifferentAccounts",
                table: "TaxCodeHistory");

            migrationBuilder.DropColumn(
                name: "purchase_account_id",
                table: "TaxCodeHistory");

            migrationBuilder.RenameColumn(
                name: "sales_account_id",
                table: "TaxCodeHistory",
                newName: "tax_account_id");

            migrationBuilder.AddColumn<string>(
                name: "tax_direction",
                table: "TaxCodeHistory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TaxCodeHistory_tax_code_setup_id",
                table: "TaxCodeHistory",
                column: "tax_code_setup_id");
        }
    }
}
