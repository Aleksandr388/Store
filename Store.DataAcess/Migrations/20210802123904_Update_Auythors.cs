using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.DataAcess.Migrations
{
    public partial class Update_Auythors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MigrationTestField",
                table: "Authors");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2021, 8, 2, 15, 39, 3, 416, DateTimeKind.Local).AddTicks(8736));

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2021, 8, 2, 15, 39, 3, 420, DateTimeKind.Local).AddTicks(394));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
