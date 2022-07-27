using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.DataAcess.Migrations
{
    public partial class Update_DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2022, 7, 27, 12, 0, 56, 371, DateTimeKind.Local).AddTicks(2342));

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2022, 7, 27, 12, 0, 56, 373, DateTimeKind.Local).AddTicks(6995));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2021, 8, 31, 11, 40, 52, 205, DateTimeKind.Local).AddTicks(6028));

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2021, 8, 31, 11, 40, 52, 208, DateTimeKind.Local).AddTicks(8391));
        }
    }
}
