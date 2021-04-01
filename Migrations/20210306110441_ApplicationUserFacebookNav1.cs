using Microsoft.EntityFrameworkCore.Migrations;

namespace ThePortal.Migrations
{
    public partial class ApplicationUserFacebookNav1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FacebookDataId",
                table: "FacebookData",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacebookDataId",
                table: "FacebookData");
        }
    }
}
