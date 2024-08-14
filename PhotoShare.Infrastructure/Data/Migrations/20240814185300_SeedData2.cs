using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoShare.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0784b2e9-353c-4bbf-9b56-118aaabac4e0", "AQAAAAIAAYagAAAAELcUoqc7yLalRK83MmbWPVcyyrFdmBAGLbDkEvjdkyTdzLplsUavIbh9bd3yL63j2A==", "94357e50-7542-46c8-994b-ce484206bed4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4c8aca6c-fcad-430d-a0a4-af2a20e7eea7", "AQAAAAIAAYagAAAAEAZo72CVYTwnyw+aPFPdqsshSxUN/iQuBWa/fcJwGZUcrzsQVFOGDvrzez4NkHtUcA==", "275888a4-cce9-4b6f-9cbe-c9645da48d0e" });
        }
    }
}
