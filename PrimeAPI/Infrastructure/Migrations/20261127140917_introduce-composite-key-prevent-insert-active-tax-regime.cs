using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class introducecompositekeypreventinsertactivetaxregime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TaxTreatment_code",
                table: "TaxTreatment");

            migrationBuilder.CreateIndex(
                name: "UQ_TaxRegime_Active",
                table: "TaxTreatment",
                column: "code",
                unique: true,
                filter: "[is_active] = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_TaxRegime_Active",
                table: "TaxTreatment");

            migrationBuilder.CreateIndex(
                name: "IX_TaxTreatment_code",
                table: "TaxTreatment",
                column: "code",
                unique: true);
        }
    }
}
