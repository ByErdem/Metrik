using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Metrik.Data.Migrations
{
    /// <inheritdoc />
    public partial class _5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "VARBINARY(500)");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 7, 14, 15, 10, 984, DateTimeKind.Local).AddTicks(6666), new DateTime(2023, 7, 7, 14, 15, 10, 984, DateTimeKind.Local).AddTicks(6667) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate", "PasswordHash" },
                values: new object[] { new DateTime(2023, 7, 7, 14, 15, 10, 984, DateTimeKind.Local).AddTicks(5337), new DateTime(2023, 7, 7, 14, 15, 10, 984, DateTimeKind.Local).AddTicks(5338), "XmZRggxyPmXv+Shr9pTaaA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "Users",
                type: "VARBINARY(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

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
    }
}
