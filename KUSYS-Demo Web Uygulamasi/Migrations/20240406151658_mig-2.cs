using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KUSYS_Demo_Web_Uygulamasi.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserCourse");

            migrationBuilder.CreateTable(
                name: "AppUserTbCourse",
                columns: table => new
                {
                    aCourseId = table.Column<int>(type: "int", nullable: false),
                    cAppUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTbCourse", x => new { x.aCourseId, x.cAppUserId });
                    table.ForeignKey(
                        name: "FK_AppUserTbCourse_AspNetUsers_cAppUserId",
                        column: x => x.cAppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserTbCourse_Courses_aCourseId",
                        column: x => x.aCourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthorityLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorityLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorityLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoleTbAuthorityLevel",
                columns: table => new
                {
                    AuthorityLevelsId = table.Column<int>(type: "int", nullable: false),
                    RolesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleTbAuthorityLevel", x => new { x.AuthorityLevelsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_AppRoleTbAuthorityLevel_AspNetRoles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppRoleTbAuthorityLevel_AuthorityLevels_AuthorityLevelsId",
                        column: x => x.AuthorityLevelsId,
                        principalTable: "AuthorityLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppRoleTbAuthorityLevel_RolesId",
                table: "AppRoleTbAuthorityLevel",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserTbCourse_cAppUserId",
                table: "AppUserTbCourse",
                column: "cAppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppRoleTbAuthorityLevel");

            migrationBuilder.DropTable(
                name: "AppUserTbCourse");

            migrationBuilder.DropTable(
                name: "AuthorityLevels");

            migrationBuilder.CreateTable(
                name: "AppUserCourse",
                columns: table => new
                {
                    aCourseId = table.Column<int>(type: "int", nullable: false),
                    cAppUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserCourse", x => new { x.aCourseId, x.cAppUserId });
                    table.ForeignKey(
                        name: "FK_AppUserCourse_AspNetUsers_cAppUserId",
                        column: x => x.cAppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserCourse_Courses_aCourseId",
                        column: x => x.aCourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserCourse_cAppUserId",
                table: "AppUserCourse",
                column: "cAppUserId");
        }
    }
}
