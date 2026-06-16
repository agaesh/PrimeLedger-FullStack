using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addglaccounttablemigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GlAccount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    account_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    account_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    account_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    normal_balance = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    parent_acc_id = table.Column<int>(type: "int", nullable: true),
                    is_allow_posting = table.Column<bool>(type: "bit", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GlAccount_GlAccount_parent_acc_id",
                        column: x => x.parent_acc_id,
                        principalTable: "GlAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GlAccount_parent_acc_id",
                table: "GlAccount",
                column: "parent_acc_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlAccount");
        }
    }
}
