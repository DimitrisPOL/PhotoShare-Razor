using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoShare.Data.Migrations
{
    /// <inheritdoc />
    public partial class schemaUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Areas_Provinces_CountryID",
                table: "Areas");

            migrationBuilder.DropForeignKey(
                name: "FK_Provinces_Countries_AreaID",
                table: "Provinces");

            migrationBuilder.RenameColumn(
                name: "AreaID",
                table: "Provinces",
                newName: "CountryID");

            migrationBuilder.RenameIndex(
                name: "IX_Provinces_AreaID",
                table: "Provinces",
                newName: "IX_Provinces_CountryID");

            migrationBuilder.RenameColumn(
                name: "CountryID",
                table: "Areas",
                newName: "ProvinceID");

            migrationBuilder.RenameIndex(
                name: "IX_Areas_CountryID",
                table: "Areas",
                newName: "IX_Areas_ProvinceID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_Provinces_ProvinceID",
                table: "Areas",
                column: "ProvinceID",
                principalTable: "Provinces",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Provinces_Countries_CountryID",
                table: "Provinces",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Areas_Provinces_ProvinceID",
                table: "Areas");

            migrationBuilder.DropForeignKey(
                name: "FK_Provinces_Countries_CountryID",
                table: "Provinces");

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

            migrationBuilder.RenameColumn(
                name: "CountryID",
                table: "Provinces",
                newName: "AreaID");

            migrationBuilder.RenameIndex(
                name: "IX_Provinces_CountryID",
                table: "Provinces",
                newName: "IX_Provinces_AreaID");

            migrationBuilder.RenameColumn(
                name: "ProvinceID",
                table: "Areas",
                newName: "CountryID");

            migrationBuilder.RenameIndex(
                name: "IX_Areas_ProvinceID",
                table: "Areas",
                newName: "IX_Areas_CountryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_Provinces_CountryID",
                table: "Areas",
                column: "CountryID",
                principalTable: "Provinces",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Provinces_Countries_AreaID",
                table: "Provinces",
                column: "AreaID",
                principalTable: "Countries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
