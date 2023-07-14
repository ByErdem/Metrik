using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Metrik.Data.Migrations
{
    /// <inheritdoc />
    public partial class _4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 7, 14, 13, 15, 898, DateTimeKind.Local).AddTicks(7665), new DateTime(2023, 7, 7, 14, 13, 15, 898, DateTimeKind.Local).AddTicks(7666) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate", "PasswordHash" },
                values: new object[] { new DateTime(2023, 7, 7, 14, 13, 15, 898, DateTimeKind.Local).AddTicks(6212), new DateTime(2023, 7, 7, 14, 13, 15, 898, DateTimeKind.Local).AddTicks(6213), new byte[] { 88, 109, 90, 82, 103, 103, 120, 121, 80, 109, 88, 118, 43, 83, 104, 114, 57, 112, 84, 97, 97, 65, 61, 61 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 7, 13, 14, 3, 61, DateTimeKind.Local).AddTicks(2063), new DateTime(2023, 7, 7, 13, 14, 3, 61, DateTimeKind.Local).AddTicks(2064) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate", "PasswordHash" },
                values: new object[] { new DateTime(2023, 7, 7, 13, 14, 3, 61, DateTimeKind.Local).AddTicks(720), new DateTime(2023, 7, 7, 13, 14, 3, 61, DateTimeKind.Local).AddTicks(720), new byte[] { 50, 48, 50, 99, 98, 57, 54, 50, 97, 99, 53, 57, 48, 55, 53, 98, 57, 54, 52, 98, 48, 55, 49, 53, 50, 100, 50, 51, 52, 98, 55, 48 } });
        }
    }
}
