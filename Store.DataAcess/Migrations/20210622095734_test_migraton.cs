using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.DataAcess.Migrations
{
    public partial class test_migraton : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "OrderItems",
                newName: "Price");

            migrationBuilder.AddColumn<string>(
                name: "MigrationTestField",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2021, 6, 22, 12, 57, 33, 183, DateTimeKind.Local).AddTicks(6305));

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2021, 6, 22, 12, 57, 33, 187, DateTimeKind.Local).AddTicks(3379));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MigrationTestField",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "OrderItems",
                newName: "Amount");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2021, 6, 17, 16, 43, 59, 926, DateTimeKind.Local).AddTicks(7690));

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2021, 6, 17, 16, 43, 59, 930, DateTimeKind.Local).AddTicks(1364));
        }
    }
}
