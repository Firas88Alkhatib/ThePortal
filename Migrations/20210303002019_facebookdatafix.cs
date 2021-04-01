using Microsoft.EntityFrameworkCore.Migrations;

namespace ThePortal.Migrations
{
    public partial class facebookdatafix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "FacebookData");

            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "FacebookData",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountName",
                table: "FacebookData",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdAccountId",
                table: "FacebookData",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessToken",
                table: "FacebookData");

            migrationBuilder.DropColumn(
                name: "AccountName",
                table: "FacebookData");

            migrationBuilder.DropColumn(
                name: "AdAccountId",
                table: "FacebookData");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "FacebookData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
