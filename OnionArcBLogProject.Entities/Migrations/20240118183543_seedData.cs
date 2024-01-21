using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnionArcBLogProject.Entities.Migrations
{
    public partial class seedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "CreatedComputerName", "CreatedDate", "CreatedIp", "FirstName", "ImageUrl", "LastIpAdress", "LastLogin", "LastName", "ModifiedComputerName", "ModifiedDate", "ModifiedIp", "Status", "Title", "UserEmail", "UserName", "UserPassword" },
                values: new object[] { new Guid("66412714-d7d6-4a86-8830-2bc3f4c6a351"), null, new DateTime(2024, 1, 18, 21, 35, 43, 794, DateTimeKind.Local).AddTicks(8875), null, "Admin", null, null, null, "Admin", null, new DateTime(2024, 1, 18, 21, 35, 43, 794, DateTimeKind.Local).AddTicks(8697), null, 1, null, "admin@blog.com", "Admin", "Asdf_1234" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("66412714-d7d6-4a86-8830-2bc3f4c6a351"));
        }
    }
}
