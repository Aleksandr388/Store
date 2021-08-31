using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.DataAcess.Migrations
{
    public partial class add_new_FullName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2021, 8, 30, 19, 9, 47, 566, DateTimeKind.Local).AddTicks(1447));

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2021, 8, 30, 19, 9, 47, 569, DateTimeKind.Local).AddTicks(3398));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2021, 8, 30, 19, 8, 4, 825, DateTimeKind.Local).AddTicks(9979));

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2021, 8, 30, 19, 8, 4, 829, DateTimeKind.Local).AddTicks(3575));
        }
    }
}
