using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.DataAcess.Migrations
{
    public partial class Add_Printing_Editional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorInPrintingEditions");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2021, 5, 10, 12, 2, 47, 104, DateTimeKind.Local).AddTicks(9696));

            migrationBuilder.InsertData(
                table: "PrintingEditions",
                columns: new[] { "Id", "CreationDate", "Curency", "Description", "IsRemoved", "Price", "Status", "Title", "Type" },
                values: new object[] { 1L, new DateTime(2021, 5, 10, 12, 2, 47, 108, DateTimeKind.Local).AddTicks(8895), 1, "Default Discription", false, 300m, 1, "Default Printing Edition", 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.CreateTable(
                name: "AuthorInPrintingEditions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    PrintingEditionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorInPrintingEditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthorInPrintingEditions_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorInPrintingEditions_PrintingEditions_PrintingEditionId",
                        column: x => x.PrintingEditionId,
                        principalTable: "PrintingEditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2021, 5, 10, 11, 44, 22, 858, DateTimeKind.Local).AddTicks(723));

            migrationBuilder.CreateIndex(
                name: "IX_AuthorInPrintingEditions_AuthorId",
                table: "AuthorInPrintingEditions",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorInPrintingEditions_PrintingEditionId",
                table: "AuthorInPrintingEditions",
                column: "PrintingEditionId");
        }
    }
}
