using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace marketplace_api.Migrations
{
    /// <inheritdoc />
    public partial class renameproductstatustable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductStatus_ProductStatusId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductStatus",
                table: "ProductStatus");

            migrationBuilder.RenameTable(
                name: "ProductStatus",
                newName: "ProductStatuses");

            migrationBuilder.AddColumn<long>(
                name: "CategoryId1",
                table: "Products",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductStatusId",
                table: "ProductStatuses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductStatuses",
                table: "ProductStatuses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId1",
                table: "Products",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId1",
                table: "Products",
                column: "CategoryId1",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductStatuses_ProductStatusId",
                table: "Products",
                column: "ProductStatusId",
                principalTable: "ProductStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId1",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductStatuses_ProductStatusId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId1",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductStatuses",
                table: "ProductStatuses");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "ProductStatuses",
                newName: "ProductStatus");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductStatusId",
                table: "ProductStatus",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductStatus",
                table: "ProductStatus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductStatus_ProductStatusId",
                table: "Products",
                column: "ProductStatusId",
                principalTable: "ProductStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
