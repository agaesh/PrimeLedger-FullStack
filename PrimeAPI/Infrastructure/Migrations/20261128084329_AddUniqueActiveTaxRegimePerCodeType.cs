using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueActiveTaxRegimePerCodeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "UX_TaxRegime_Active_CodeType",
                table: "TaxRegime",
                column: "CodeType",
                unique: true,
                filter: "[is_active] = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UX_TaxRegime_Active_CodeType",
                table: "TaxRegime");

        }
    }
}
