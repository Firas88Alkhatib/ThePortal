using Microsoft.EntityFrameworkCore.Migrations;

namespace ThePortal.Migrations
{
    public partial class test3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_FacebookData_FacebookDataId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FacebookDataId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "FacebookData");

            migrationBuilder.DropColumn(
                name: "FacebookDataId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "FacebookData",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FacebookData_ApplicationUserId",
                table: "FacebookData",
                column: "ApplicationUserId",
                unique: true,
                filter: "[ApplicationUserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_FacebookData_AspNetUsers_ApplicationUserId",
                table: "FacebookData",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacebookData_AspNetUsers_ApplicationUserId",
                table: "FacebookData");

            migrationBuilder.DropIndex(
                name: "IX_FacebookData_ApplicationUserId",
                table: "FacebookData");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "FacebookData");

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "FacebookData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FacebookDataId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FacebookDataId",
                table: "AspNetUsers",
                column: "FacebookDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_FacebookData_FacebookDataId",
                table: "AspNetUsers",
                column: "FacebookDataId",
                principalTable: "FacebookData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
