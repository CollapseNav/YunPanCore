using Microsoft.EntityFrameworkCore.Migrations;

namespace YunPanCore.Migrations
{
    public partial class fit_mysql : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IsDeleted",
                table: "UserDataInfo",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.AlterColumn<int>(
                name: "IsDeleted",
                table: "SharedFileInfo",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.AlterColumn<int>(
                name: "Shared",
                table: "FileInfo",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.AlterColumn<int>(
                name: "IsDeleted",
                table: "FileInfo",
                nullable: false,
                oldClrType: typeof(short));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "IsDeleted",
                table: "UserDataInfo",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<short>(
                name: "IsDeleted",
                table: "SharedFileInfo",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<short>(
                name: "Shared",
                table: "FileInfo",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<short>(
                name: "IsDeleted",
                table: "FileInfo",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
