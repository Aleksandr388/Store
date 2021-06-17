using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.DataAcess.Migrations
{
    public partial class Edit_Order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.RenameColumn(
                name: "Desription",
                table: "Orders",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Amounnt",
                table: "OrderItems",
                newName: "Amount");

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "CreationDate", "IsRemoved", "Name" },
                values: new object[] { 1L, new DateTime(2021, 6, 16, 13, 44, 40, 141, DateTimeKind.Local).AddTicks(9094), false, "Leonardo Davinchi" });

            migrationBuilder.InsertData(
                table: "PrintingEditions",
                columns: new[] { "Id", "CreationDate", "Curency", "Description", "IsRemoved", "Price", "Status", "Title", "Type" },
                values: new object[] { 1L, new DateTime(2021, 6, 16, 13, 44, 40, 145, DateTimeKind.Local).AddTicks(8882), 1, "The Lester Codex is a notebook of scientific records made by Leonardo da Vinci in Milan in 1504-1510.", false, 300m, 1, "Lesters codex", 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Orders",
                newName: "Desription");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "OrderItems",
                newName: "Amounnt");

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "CreationDate", "IsRemoved", "Name" },
                values: new object[] { 1L, new DateTime(2021, 5, 21, 11, 49, 40, 666, DateTimeKind.Local).AddTicks(5286), false, "Leonardo Davinchi" });

            migrationBuilder.InsertData(
                table: "PrintingEditions",
                columns: new[] { "Id", "CreationDate", "Curency", "Description", "IsRemoved", "Price", "Status", "Title", "Type" },
                values: new object[] { 1L, new DateTime(2021, 5, 21, 11, 49, 40, 669, DateTimeKind.Local).AddTicks(9098), 1, "The Lester Codex is a notebook of scientific records made by Leonardo da Vinci in Milan in 1504-1510.", false, 300m, 1, "Lesters codex", 2 });
        }
    }
}
