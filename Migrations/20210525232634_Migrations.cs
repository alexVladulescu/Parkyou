using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace Parkyou.Migrations
{
    public partial class Migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(767)", nullable: false),
                    Name = table.Column<string>(type: "varchar(127)", maxLength: 127, nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(127)", maxLength: 127, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    FirstName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Rol = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "varchar(127)", maxLength: 127, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "varchar(127)", maxLength: 127, nullable: true),
                    Email = table.Column<string>(type: "varchar(127)", maxLength: 127, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "varchar(127)", maxLength: 127, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "parking_spots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 127, nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Row = table.Column<string>(type: "text", nullable: false),
                    Col = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parking_spots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "reports",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    Title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    Row = table.Column<string>(type: "text", nullable: true),
                    Col = table.Column<string>(type: "text", nullable: true),
                    ReportedBy = table.Column<string>(type: "text", nullable: false),
                    Solved = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Resolution = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    Closed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(767)", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "administrators",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_administrators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_administrators_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(36)", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(127)", maxLength: 127, nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(127)", maxLength: 127, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "varchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(127)", maxLength: 127, nullable: false),
                    RoleId = table.Column<string>(type: "varchar(127)", maxLength: 127, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(127)", maxLength: 127, nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(127)", maxLength: 127, nullable: false),
                    Name = table.Column<string>(type: "varchar(127)", maxLength: 127, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    ParkSpotId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_users_parking_spots_ParkSpotId",
                        column: x => x.ParkSpotId,
                        principalTable: "parking_spots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Rol", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5e2e7d09-328e-4219-bf70-be03f70a6e2c", 0, "877761a2-3eea-4441-965d-740abd985da3", "vladulescualexandru1@gmail.com", true, "Admin", "Admin", true, null, "VLADULESCUALEXANDRU1@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEGkoJKEv7XkD6t07STWQOzJnTMMX9kigmNaTm2RcTKtp0THJWyUGTy9iQ5x6Bz+ahA==", null, false, "Administrator", "7f434309-a4d9-48e9-9ebb-8803db794577", false, "admin" });

            migrationBuilder.InsertData(
                table: "parking_spots",
                columns: new[] { "Id", "Col", "Row", "Status", "UserName" },
                values: new object[,]
                {
                    { 65, 5, "G", 0, "" },
                    { 64, 4, "G", 0, "" },
                    { 63, 3, "G", 0, "" },
                    { 62, 2, "G", 0, "" },
                    { 61, 1, "G", 0, "" },
                    { 60, 10, "F", 0, "" },
                    { 59, 9, "F", 0, "" },
                    { 58, 8, "F", 0, "" },
                    { 57, 7, "F", 0, "" },
                    { 56, 6, "F", 0, "" },
                    { 55, 5, "F", 0, "" },
                    { 54, 4, "F", 0, "" },
                    { 53, 3, "F", 0, "" },
                    { 52, 2, "F", 0, "" },
                    { 51, 1, "F", 0, "" },
                    { 50, 10, "E", 0, "" },
                    { 49, 9, "E", 0, "" },
                    { 48, 8, "E", 0, "" },
                    { 47, 7, "E", 0, "" },
                    { 66, 6, "G", 0, "" },
                    { 46, 6, "E", 0, "" },
                    { 67, 7, "G", 0, "" },
                    { 69, 9, "G", 0, "" },
                    { 88, 8, "I", 0, "" },
                    { 87, 7, "I", 0, "" },
                    { 86, 6, "I", 0, "" },
                    { 85, 5, "I", 0, "" },
                    { 84, 4, "I", 0, "" },
                    { 83, 3, "I", 0, "" },
                    { 82, 2, "I", 0, "" },
                    { 81, 1, "I", 0, "" },
                    { 80, 10, "H", 0, "" },
                    { 79, 9, "H", 0, "" },
                    { 78, 8, "H", 0, "" },
                    { 77, 7, "H", 0, "" },
                    { 76, 6, "H", 0, "" },
                    { 75, 5, "H", 0, "" },
                    { 74, 4, "H", 0, "" },
                    { 73, 3, "H", 0, "" },
                    { 72, 2, "H", 0, "" },
                    { 71, 1, "H", 0, "" },
                    { 70, 10, "G", 0, "" },
                    { 68, 8, "G", 0, "" },
                    { 89, 9, "I", 0, "" },
                    { 45, 5, "E", 0, "" },
                    { 43, 3, "E", 0, "" },
                    { 19, 9, "B", 0, "" },
                    { 18, 8, "B", 0, "" },
                    { 17, 7, "B", 0, "" },
                    { 16, 6, "B", 0, "" },
                    { 15, 5, "B", 0, "" },
                    { 14, 4, "B", 0, "" },
                    { 13, 3, "B", 0, "" },
                    { 12, 2, "B", 0, "" },
                    { 11, 1, "B", 0, "" },
                    { 10, 10, "A", 0, "" },
                    { 9, 9, "A", 0, "" },
                    { 8, 8, "A", 0, "" },
                    { 7, 7, "A", 0, "" },
                    { 6, 6, "A", 0, "" },
                    { 5, 5, "A", 0, "" },
                    { 4, 4, "A", 0, "" },
                    { 3, 3, "A", 0, "" },
                    { 2, 2, "A", 0, "" },
                    { 1, 1, "A", 0, "" },
                    { 20, 10, "B", 0, "" },
                    { 44, 4, "E", 0, "" },
                    { 21, 1, "C", 0, "" },
                    { 23, 3, "C", 0, "" },
                    { 42, 2, "E", 0, "" },
                    { 41, 1, "E", 0, "" },
                    { 40, 10, "D", 0, "" },
                    { 39, 9, "D", 0, "" },
                    { 38, 8, "D", 0, "" },
                    { 37, 7, "D", 0, "" },
                    { 36, 6, "D", 0, "" },
                    { 35, 5, "D", 0, "" },
                    { 34, 4, "D", 0, "" },
                    { 33, 3, "D", 0, "" },
                    { 32, 2, "D", 0, "" },
                    { 31, 1, "D", 0, "" },
                    { 30, 10, "C", 0, "" },
                    { 29, 9, "C", 0, "" },
                    { 28, 8, "C", 0, "" },
                    { 27, 7, "C", 0, "" },
                    { 26, 6, "C", 0, "" },
                    { 25, 5, "C", 0, "" },
                    { 24, 4, "C", 0, "" },
                    { 22, 2, "C", 0, "" },
                    { 90, 10, "I", 0, "" }
                });

            migrationBuilder.InsertData(
                table: "administrators",
                column: "Id",
                value: "5e2e7d09-328e-4219-bf70-be03f70a6e2c");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_ParkSpotId",
                table: "users",
                column: "ParkSpotId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "administrators");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "reports");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "parking_spots");
        }
    }
}
