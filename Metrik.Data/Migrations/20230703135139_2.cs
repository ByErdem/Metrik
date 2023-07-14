using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Metrik.Data.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 3, 16, 51, 39, 434, DateTimeKind.Local).AddTicks(5582), new DateTime(2023, 7, 3, 16, 51, 39, 434, DateTimeKind.Local).AddTicks(5583) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 3, 16, 51, 39, 434, DateTimeKind.Local).AddTicks(4329), new DateTime(2023, 7, 3, 16, 51, 39, 434, DateTimeKind.Local).AddTicks(4330) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 3, 16, 15, 58, 629, DateTimeKind.Local).AddTicks(63), new DateTime(2023, 7, 3, 16, 15, 58, 629, DateTimeKind.Local).AddTicks(64) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate", "Username" },
                values: new object[] { new DateTime(2023, 7, 3, 16, 15, 58, 628, DateTimeKind.Local).AddTicks(8465), new DateTime(2023, 7, 3, 16, 15, 58, 628, DateTimeKind.Local).AddTicks(8466), "ecanturk" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }
    }
}
