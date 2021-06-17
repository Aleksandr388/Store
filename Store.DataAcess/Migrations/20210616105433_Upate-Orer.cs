using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.DataAcess.Migrations
{
    public partial class UpateOrer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_StoreUserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_StoreUserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "StoreUserId",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2021, 6, 16, 13, 54, 32, 411, DateTimeKind.Local).AddTicks(2547));

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2021, 6, 16, 13, 54, 32, 414, DateTimeKind.Local).AddTicks(6251));

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserId",
                table: "Orders");

            migrationBuilder.AddColumn<long>(
                name: "StoreUserId",
                table: "Orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2021, 6, 16, 13, 48, 10, 867, DateTimeKind.Local).AddTicks(2722));

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2021, 6, 16, 13, 48, 10, 870, DateTimeKind.Local).AddTicks(6374));

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StoreUserId",
                table: "Orders",
                column: "StoreUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_StoreUserId",
                table: "Orders",
                column: "StoreUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
