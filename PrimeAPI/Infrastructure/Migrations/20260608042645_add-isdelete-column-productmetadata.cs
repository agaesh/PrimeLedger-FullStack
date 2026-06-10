using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addisdeletecolumnproductmetadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "ProductMetadata",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "ProductMetadata");
        }
    }
}
