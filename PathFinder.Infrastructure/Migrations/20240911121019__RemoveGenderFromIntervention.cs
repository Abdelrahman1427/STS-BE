using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PathFinder.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _RemoveGenderFromIntervention : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyStatuse_User_CreatedBy",
                table: "CompanyStatuse");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyStatuse_User_UpdatedBy",
                table: "CompanyStatuse");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialServiceProvider_CompanyStatuse_CompanyStatusId",
                table: "FinancialServiceProvider");

            migrationBuilder.DropForeignKey(
                name: "FK_privateSector_CompanyStatuse_CompanyStatusId",
                table: "privateSector");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyStatuse",
                table: "CompanyStatuse");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "Intervention");

            migrationBuilder.RenameTable(
                name: "CompanyStatuse",
                newName: "CompanyStatus");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyStatuse_UpdatedBy",
                table: "CompanyStatus",
                newName: "IX_CompanyStatus_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyStatuse_CreatedBy",
                table: "CompanyStatus",
                newName: "IX_CompanyStatus_CreatedBy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyStatus",
                table: "CompanyStatus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyStatus_User_CreatedBy",
                table: "CompanyStatus",
                column: "CreatedBy",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyStatus_User_UpdatedBy",
                table: "CompanyStatus",
                column: "UpdatedBy",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialServiceProvider_CompanyStatus_CompanyStatusId",
                table: "FinancialServiceProvider",
                column: "CompanyStatusId",
                principalTable: "CompanyStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_privateSector_CompanyStatus_CompanyStatusId",
                table: "privateSector",
                column: "CompanyStatusId",
                principalTable: "CompanyStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyStatus_User_CreatedBy",
                table: "CompanyStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyStatus_User_UpdatedBy",
                table: "CompanyStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialServiceProvider_CompanyStatus_CompanyStatusId",
                table: "FinancialServiceProvider");

            migrationBuilder.DropForeignKey(
                name: "FK_privateSector_CompanyStatus_CompanyStatusId",
                table: "privateSector");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyStatus",
                table: "CompanyStatus");

            migrationBuilder.RenameTable(
                name: "CompanyStatus",
                newName: "CompanyStatuse");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyStatus_UpdatedBy",
                table: "CompanyStatuse",
                newName: "IX_CompanyStatuse_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyStatus_CreatedBy",
                table: "CompanyStatuse",
                newName: "IX_CompanyStatuse_CreatedBy");

            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "Intervention",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyStatuse",
                table: "CompanyStatuse",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyStatuse_User_CreatedBy",
                table: "CompanyStatuse",
                column: "CreatedBy",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyStatuse_User_UpdatedBy",
                table: "CompanyStatuse",
                column: "UpdatedBy",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialServiceProvider_CompanyStatuse_CompanyStatusId",
                table: "FinancialServiceProvider",
                column: "CompanyStatusId",
                principalTable: "CompanyStatuse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_privateSector_CompanyStatuse_CompanyStatusId",
                table: "privateSector",
                column: "CompanyStatusId",
                principalTable: "CompanyStatuse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
