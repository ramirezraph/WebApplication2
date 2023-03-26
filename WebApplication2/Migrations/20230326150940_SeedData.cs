using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DOB", "DeptId", "EmailAdress", "FullName", "PhoneNumber" },
                values: new object[] { 1, new DateTime(1999, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "john@email.com", "John Doe", "09920098321" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
