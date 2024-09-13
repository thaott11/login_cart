using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoGH_OnTap.Migrations
{
    public partial class hehe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SanPhams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GioHang",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GioHang_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GHCTs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    SanPhamID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GioHangID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GHCTs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GHCTs_GioHang_GioHangID",
                        column: x => x.GioHangID,
                        principalTable: "GioHang",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GHCTs_SanPhams_SanPhamID",
                        column: x => x.SanPhamID,
                        principalTable: "SanPhams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GHCTs_GioHangID",
                table: "GHCTs",
                column: "GioHangID");

            migrationBuilder.CreateIndex(
                name: "IX_GHCTs_SanPhamID",
                table: "GHCTs",
                column: "SanPhamID");

            migrationBuilder.CreateIndex(
                name: "IX_GioHang_AccountID",
                table: "GioHang",
                column: "AccountID",
                unique: true,
                filter: "[AccountID] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GHCTs");

            migrationBuilder.DropTable(
                name: "GioHang");

            migrationBuilder.DropTable(
                name: "SanPhams");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
