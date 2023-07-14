using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Metrik.Data.Migrations
{
    /// <inheritdoc />
    public partial class _3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModifiedByName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedByName",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ModifiedByName",
                table: "Roles");

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedByUserId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedByUserId",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedByUserId", "CreatedDate", "ModifiedByUserId", "ModifiedDate" },
                values: new object[] { -1, new DateTime(2023, 7, 7, 13, 14, 3, 61, DateTimeKind.Local).AddTicks(2063), -1, new DateTime(2023, 7, 7, 13, 14, 3, 61, DateTimeKind.Local).AddTicks(2064) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedByUserId", "CreatedDate", "ModifiedByUserId", "ModifiedDate" },
                values: new object[] { -1, new DateTime(2023, 7, 7, 13, 14, 3, 61, DateTimeKind.Local).AddTicks(720), -1, new DateTime(2023, 7, 7, 13, 14, 3, 61, DateTimeKind.Local).AddTicks(720) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "Roles");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByName",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByName",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByName",
                table: "Roles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByName",
                table: "Roles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedByName", "CreatedDate", "ModifiedByName", "ModifiedDate" },
                values: new object[] { "InitialCreate", new DateTime(2023, 7, 3, 16, 51, 39, 434, DateTimeKind.Local).AddTicks(5582), "InitialCreate", new DateTime(2023, 7, 3, 16, 51, 39, 434, DateTimeKind.Local).AddTicks(5583) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedByName", "CreatedDate", "ModifiedByName", "ModifiedDate" },
                values: new object[] { "InitialCreate", new DateTime(2023, 7, 3, 16, 51, 39, 434, DateTimeKind.Local).AddTicks(4329), "InitialCreate", new DateTime(2023, 7, 3, 16, 51, 39, 434, DateTimeKind.Local).AddTicks(4330) });
        }
    }
}
