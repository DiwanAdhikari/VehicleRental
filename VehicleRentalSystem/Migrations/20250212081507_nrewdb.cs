using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleRentalSystem.Migrations
{
    /// <inheritdoc />
    public partial class nrewdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_VehicleOwner_VehicleOwnerId",
                table: "Vehicle");

            migrationBuilder.DropTable(
                name: "VehicleOwner");

            migrationBuilder.DropIndex(
                name: "IX_Vehicle_VehicleOwnerId",
                table: "Vehicle");

            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleOwnerBasicInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleOwnerBasicInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleOwnerBasicInfo_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehicleDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleOwnerBasicInfoId = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    BillbookNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillbookImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductionYear = table.Column<DateOnly>(type: "date", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehiclePhotos = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleDetails_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleDetails_VehicleOwnerBasicInfo_VehicleOwnerBasicInfoId",
                        column: x => x.VehicleOwnerBasicInfoId,
                        principalTable: "VehicleOwnerBasicInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleDetails_BrandId",
                table: "VehicleDetails",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleDetails_VehicleOwnerBasicInfoId",
                table: "VehicleDetails",
                column: "VehicleOwnerBasicInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleOwnerBasicInfo_GenderId",
                table: "VehicleOwnerBasicInfo",
                column: "GenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleDetails");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "VehicleOwnerBasicInfo");

            migrationBuilder.CreateTable(
                name: "VehicleOwner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleOwner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleOwner_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_VehicleOwnerId",
                table: "Vehicle",
                column: "VehicleOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleOwner_GenderId",
                table: "VehicleOwner",
                column: "GenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_VehicleOwner_VehicleOwnerId",
                table: "Vehicle",
                column: "VehicleOwnerId",
                principalTable: "VehicleOwner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
