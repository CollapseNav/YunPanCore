using Microsoft.EntityFrameworkCore.Migrations;

namespace YunPanCore.Migrations
{
    public partial class add_cap_to_userdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cap",
                table: "UserDataInfo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cap",
                table: "UserDataInfo");
        }
    }
}
