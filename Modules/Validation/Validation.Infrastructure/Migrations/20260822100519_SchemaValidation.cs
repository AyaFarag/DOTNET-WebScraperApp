using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Validation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SchemaValidation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ValidationError_ValidationResults_ValidationResultId",
                table: "ValidationError");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ValidationExecution",
                table: "ValidationExecution");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ValidationError",
                table: "ValidationError");

            migrationBuilder.RenameTable(
                name: "ValidationResults",
                newName: "ValidationResults",
                newSchema: "Validation");

            migrationBuilder.RenameTable(
                name: "ProcessedEvents",
                newName: "ProcessedEvents",
                newSchema: "Validation");

            migrationBuilder.RenameTable(
                name: "ValidationExecution",
                newName: "ValidationExecutions",
                newSchema: "Validation");

            migrationBuilder.RenameTable(
                name: "ValidationError",
                newName: "ValidationErrors",
                newSchema: "Validation");

            migrationBuilder.RenameIndex(
                name: "IX_ValidationExecution_BatchId",
                schema: "Validation",
                table: "ValidationExecutions",
                newName: "IX_ValidationExecutions_BatchId");

            migrationBuilder.RenameIndex(
                name: "IX_ValidationError_ValidationResultId",
                schema: "Validation",
                table: "ValidationErrors",
                newName: "IX_ValidationErrors_ValidationResultId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ValidationExecutions",
                schema: "Validation",
                table: "ValidationExecutions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ValidationErrors",
                schema: "Validation",
                table: "ValidationErrors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ValidationErrors_ValidationResults_ValidationResultId",
                schema: "Validation",
                table: "ValidationErrors",
                column: "ValidationResultId",
                principalSchema: "Validation",
                principalTable: "ValidationResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ValidationErrors_ValidationResults_ValidationResultId",
                schema: "Validation",
                table: "ValidationErrors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ValidationExecutions",
                schema: "Validation",
                table: "ValidationExecutions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ValidationErrors",
                schema: "Validation",
                table: "ValidationErrors");

            migrationBuilder.RenameTable(
                name: "ValidationResults",
                schema: "Validation",
                newName: "ValidationResults");

            migrationBuilder.RenameTable(
                name: "ProcessedEvents",
                schema: "Validation",
                newName: "ProcessedEvents");

            migrationBuilder.RenameTable(
                name: "ValidationExecutions",
                schema: "Validation",
                newName: "ValidationExecution");

            migrationBuilder.RenameTable(
                name: "ValidationErrors",
                schema: "Validation",
                newName: "ValidationError");

            migrationBuilder.RenameIndex(
                name: "IX_ValidationExecutions_BatchId",
                table: "ValidationExecution",
                newName: "IX_ValidationExecution_BatchId");

            migrationBuilder.RenameIndex(
                name: "IX_ValidationErrors_ValidationResultId",
                table: "ValidationError",
                newName: "IX_ValidationError_ValidationResultId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ValidationExecution",
                table: "ValidationExecution",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ValidationError",
                table: "ValidationError",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ValidationError_ValidationResults_ValidationResultId",
                table: "ValidationError",
                column: "ValidationResultId",
                principalTable: "ValidationResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
