using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoShare.Data.Migrations
{
    /// <inheritdoc />
    public partial class cleanup : Migration
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
                values: new object[] { "02174cf0–9412–4cfe-afbf-59f706d72cf6", 0, "Athens", "8993483f-3132-499c-b0df-8750e7cd30f3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Masters", false, "example@gmail.com", true, false, null, "admin", null, "EXAMPLE@GMAIL.COM", "AQAAAAIAAYagAAAAECeSb9vasTrAb8TrfbQP+Qnn4IBSltMwO/PXphH2BlFl94gSqAcCuKT9VpcMWy+r5w==", null, false, new byte[0], "9c3b4499-ec3e-4d41-86be-b111fb000135", false, "example@gmail.com", 0 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2652f744-d840-4c18-9ecf-87e84355101f", "02174cf0–9412–4cfe-afbf-59f706d72cf6" });
        }
    }
}
