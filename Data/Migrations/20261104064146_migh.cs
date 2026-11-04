using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class migh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "SchoolAddress", "StudentsCount" },
                values: new object[] { new DateTime(2026, 11, 4, 8, 41, 46, 199, DateTimeKind.Local).AddTicks(3111), "Kazanlak", 1 });

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "SchoolAddress" },
                values: new object[] { new DateTime(2026, 11, 4, 8, 41, 46, 199, DateTimeKind.Local).AddTicks(3158), "Kazanlak" });

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "SchoolAddress", "StudentsCount" },
                values: new object[] { new DateTime(2026, 11, 4, 8, 41, 46, 199, DateTimeKind.Local).AddTicks(3161), "Sofia", 2 });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                column: "Address",
                value: "Kazanlak");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                column: "Address",
                value: "Kazanlak");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3,
                column: "Address",
                value: "Sofia");

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Address",
                value: "Kazanlak");

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Address",
                value: "Kazanlak");

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Address",
                value: "Sofia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "SchoolAddress", "StudentsCount" },
                values: new object[] { new DateTime(2026, 11, 3, 10, 26, 1, 432, DateTimeKind.Local).AddTicks(8282), "Kazanlak, Bulgaria", 0 });

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "SchoolAddress" },
                values: new object[] { new DateTime(2026, 11, 3, 10, 26, 1, 432, DateTimeKind.Local).AddTicks(8323), "Kazanlak, Bulgaria" });

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "SchoolAddress", "StudentsCount" },
                values: new object[] { new DateTime(2026, 11, 3, 10, 26, 1, 432, DateTimeKind.Local).AddTicks(8325), "Sofia, Bulgaria", 0 });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                column: "Address",
                value: "Kazanlak, Bulgaria");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                column: "Address",
                value: "Kazanlak, Bulgaria");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3,
                column: "Address",
                value: "Sofia, Bulgaria");

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Address",
                value: "Kazanlak, Bulgaria");

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Address",
                value: "Kazanlak, Bulgaria");

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Address",
                value: "Sofia, Bulgaria");
        }
    }
}
