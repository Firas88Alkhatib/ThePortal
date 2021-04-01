using Microsoft.EntityFrameworkCore.Migrations;

namespace ThePortal.Migrations
{
    public partial class addGoogleData1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_GoogleData_GoogleDataId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GoogleDataId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GoogleDataId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "GoogleData",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoogleData_ApplicationUserId",
                table: "GoogleData",
                column: "ApplicationUserId",
                unique: true,
                filter: "[ApplicationUserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_GoogleData_AspNetUsers_ApplicationUserId",
                table: "GoogleData",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoogleData_AspNetUsers_ApplicationUserId",
                table: "GoogleData");

            migrationBuilder.DropIndex(
                name: "IX_GoogleData_ApplicationUserId",
                table: "GoogleData");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "GoogleData");

            migrationBuilder.AddColumn<int>(
                name: "GoogleDataId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GoogleDataId",
                table: "AspNetUsers",
                column: "GoogleDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_GoogleData_GoogleDataId",
                table: "AspNetUsers",
                column: "GoogleDataId",
                principalTable: "GoogleData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
