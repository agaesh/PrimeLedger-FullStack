using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeglaccounttablenametoglaccounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GlAccount_GlAccount_parent_acc_id",
                table: "GlAccount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GlAccount",
                table: "GlAccount");

            migrationBuilder.RenameTable(
                name: "GlAccount",
                newName: "GlAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_GlAccount_parent_acc_id",
                table: "GlAccounts",
                newName: "IX_GlAccounts_parent_acc_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GlAccounts",
                table: "GlAccounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GlAccounts_GlAccounts_parent_acc_id",
                table: "GlAccounts",
                column: "parent_acc_id",
                principalTable: "GlAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GlAccounts_GlAccounts_parent_acc_id",
                table: "GlAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GlAccounts",
                table: "GlAccounts");

            migrationBuilder.RenameTable(
                name: "GlAccounts",
                newName: "GlAccount");

            migrationBuilder.RenameIndex(
                name: "IX_GlAccounts_parent_acc_id",
                table: "GlAccount",
                newName: "IX_GlAccount_parent_acc_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GlAccount",
                table: "GlAccount",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GlAccount_GlAccount_parent_acc_id",
                table: "GlAccount",
                column: "parent_acc_id",
                principalTable: "GlAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
