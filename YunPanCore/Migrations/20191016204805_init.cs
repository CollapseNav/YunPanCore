using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YunPanCore.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserDataInfo",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 40, nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    ChangedBy = table.Column<string>(maxLength: 40, nullable: false),
                    ChangedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserAccount = table.Column<string>(maxLength: 50, nullable: false),
                    UserName = table.Column<string>(maxLength: 20, nullable: false),
                    PassWord = table.Column<string>(maxLength: 20, nullable: false),
                    UserType = table.Column<string>(maxLength: 10, nullable: true),
                    FolderPath = table.Column<string>(maxLength: 200, nullable: false),
                    Gender = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    EmailAddress = table.Column<string>(maxLength: 50, nullable: true),
                    Phone = table.Column<string>(maxLength: 40, nullable: true),
                    Remark = table.Column<string>(maxLength: 233, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDataInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileInfo",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 40, nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    ChangedBy = table.Column<string>(maxLength: 40, nullable: false),
                    ChangedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FileName = table.Column<string>(maxLength: 30, nullable: false),
                    FileType = table.Column<string>(maxLength: 10, nullable: false),
                    FileSize = table.Column<string>(maxLength: 20, nullable: false),
                    FilePath = table.Column<string>(maxLength: 200, nullable: false),
                    HashCode = table.Column<string>(maxLength: 40, nullable: true),
                    MapPath = table.Column<string>(maxLength: 200, nullable: false),
                    OwnerId = table.Column<string>(maxLength: 40, nullable: false),
                    Shared = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileInfo_UserDataInfo_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "UserDataInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SharedFileInfo",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 40, nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    ChangedBy = table.Column<string>(maxLength: 40, nullable: false),
                    ChangedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OwnerName = table.Column<string>(maxLength: 20, nullable: true),
                    FileId = table.Column<string>(maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedFileInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SharedFileInfo_FileInfo_FileId",
                        column: x => x.FileId,
                        principalTable: "FileInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileInfo_Id",
                table: "FileInfo",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileInfo_OwnerId",
                table: "FileInfo",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedFileInfo_FileId",
                table: "SharedFileInfo",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDataInfo_Id",
                table: "UserDataInfo",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserDataInfo_UserAccount",
                table: "UserDataInfo",
                column: "UserAccount",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserDataInfo_UserName",
                table: "UserDataInfo",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SharedFileInfo");

            migrationBuilder.DropTable(
                name: "FileInfo");

            migrationBuilder.DropTable(
                name: "UserDataInfo");
        }
    }
}
