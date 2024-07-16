using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finWise.Migrations
{
    /// <inheritdoc />
    public partial class reserdbcontext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionDetails_Users_UserId",
                table: "TransactionDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionDetails",
                table: "TransactionDetails");

            migrationBuilder.RenameTable(
                name: "TransactionDetails",
                newName: "Transactions");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionDetails_UserId",
                table: "Transactions",
                newName: "IX_Transactions_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_UserId",
                table: "Transactions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_UserId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "TransactionDetails");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_UserId",
                table: "TransactionDetails",
                newName: "IX_TransactionDetails_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionDetails",
                table: "TransactionDetails",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionDetails_Users_UserId",
                table: "TransactionDetails",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
