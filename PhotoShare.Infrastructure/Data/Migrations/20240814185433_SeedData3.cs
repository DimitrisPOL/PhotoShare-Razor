using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoShare.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2652f744-d840-4c18-9ecf-87e84355101f", "02174cf0–9412–4cfe-afbf-59f706d72cf6" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2652f744-d840-4c18-9ecf-87e84355101f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2652f744-d840-4c18-9ecf-87e84355101f", "2652f744-d840-4c18-9ecf-87e84355101f", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "City", "ConcurrencyStamp", "DOB", "Degree", "DisplayToFront", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePic", "SecurityStamp", "TwoFactorEnabled", "UserName", "YearsOfExperience" },
                values: new object[] { "02174cf0–9412–4cfe-afbf-59f706d72cf6", 0, "Athens", "0784b2e9-353c-4bbf-9b56-118aaabac4e0", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Masters", false, "example@gmail.com", true, false, null, "admin", null, "EXAMPLE@GMAIL.COM", "AQAAAAIAAYagAAAAELcUoqc7yLalRK83MmbWPVcyyrFdmBAGLbDkEvjdkyTdzLplsUavIbh9bd3yL63j2A==", null, false, new byte[0], "94357e50-7542-46c8-994b-ce484206bed4", false, "example@gmail.com", 0 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2652f744-d840-4c18-9ecf-87e84355101f", "02174cf0–9412–4cfe-afbf-59f706d72cf6" });
        }
    }
}
