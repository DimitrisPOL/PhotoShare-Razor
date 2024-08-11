using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoShare.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialIdentityRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Degree",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "DisplayToFront",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePic",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<int>(
                name: "YearsOfExperience",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelephonePrefix1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelephonePrefix2 = table.Column<int>(type: "int", nullable: false),
                    Outposts = table.Column<int>(type: "int", nullable: false),
                    NumberOfPictures = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Photographers",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePic = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearsOfExperience = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photographers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AreaID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TelephonePrefix1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelephonePrefix2 = table.Column<int>(type: "int", nullable: false),
                    NumberOfOutposts = table.Column<int>(type: "int", nullable: false),
                    NumberOfPictures = table.Column<long>(type: "bigint", nullable: false),
                    NumberOfPhotographers = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Provinces_Countries_AreaID",
                        column: x => x.AreaID,
                        principalTable: "Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TelephonePrefix1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelephonePrefix2 = table.Column<int>(type: "int", nullable: false),
                    NumberOfOutposts = table.Column<int>(type: "int", nullable: false),
                    NumberOfPictures = table.Column<long>(type: "bigint", nullable: false),
                    NumberOfPhotographers = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Areas_Provinces_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Provinces",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SearchIndex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePic = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    AreaID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TelephonePrefix1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelephonePrefix2 = table.Column<int>(type: "int", nullable: false),
                    NumberOfOutposts = table.Column<int>(type: "int", nullable: false),
                    NumberOfPictures = table.Column<long>(type: "bigint", nullable: false),
                    NumberOfPhotographers = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Locations_Areas_AreaID",
                        column: x => x.AreaID,
                        principalTable: "Areas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Areas_CountryID",
                table: "Areas",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_AreaID",
                table: "Locations",
                column: "AreaID");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_AreaID",
                table: "Provinces",
                column: "AreaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Photographers");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DOB",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Degree",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DisplayToFront",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfilePic",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "YearsOfExperience",
                table: "AspNetUsers");
        }
    }
}
