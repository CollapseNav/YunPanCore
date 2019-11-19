using Microsoft.EntityFrameworkCore.Migrations;

namespace YunPanCore.Migrations
{
    public partial class change_filename_length : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ChangedBy",
                table: "UserDataInfo",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "ChangedBy",
                table: "SharedFileInfo",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "FileInfo",
                maxLength: 248,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "ChangedBy",
                table: "FileInfo",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 40);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ChangedBy",
                table: "UserDataInfo",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChangedBy",
                table: "SharedFileInfo",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "FileInfo",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 248);

            migrationBuilder.AlterColumn<string>(
                name: "ChangedBy",
                table: "FileInfo",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 40,
                oldNullable: true);
        }
    }
}
