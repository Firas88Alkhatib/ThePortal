using Microsoft.EntityFrameworkCore.Migrations;

namespace ThePortal.Migrations
{
    public partial class fixRegionkys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacebookData_AspNetUsers_ApplicationUserId",
                table: "FacebookData");

            migrationBuilder.DropForeignKey(
                name: "FK_GoogleData_AspNetUsers_ApplicationUserId",
                table: "GoogleData");

            migrationBuilder.DropIndex(
                name: "IX_GoogleData_ApplicationUserId",
                table: "GoogleData");

            migrationBuilder.DropIndex(
                name: "IX_FacebookData_ApplicationUserId",
                table: "FacebookData");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "GoogleData",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "FacebookData",
                newName: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GoogleData_UserId",
                table: "GoogleData",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FacebookData_UserId",
                table: "FacebookData",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_FacebookData_AspNetUsers_UserId",
                table: "FacebookData",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GoogleData_AspNetUsers_UserId",
                table: "GoogleData",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacebookData_AspNetUsers_UserId",
                table: "FacebookData");

            migrationBuilder.DropForeignKey(
                name: "FK_GoogleData_AspNetUsers_UserId",
                table: "GoogleData");

            migrationBuilder.DropIndex(
                name: "IX_GoogleData_UserId",
                table: "GoogleData");

            migrationBuilder.DropIndex(
                name: "IX_FacebookData_UserId",
                table: "FacebookData");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "GoogleData",
                newName: "ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "FacebookData",
                newName: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GoogleData_ApplicationUserId",
                table: "GoogleData",
                column: "ApplicationUserId",
                unique: true,
                filter: "[ApplicationUserId] IS NOT NULL");

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

            migrationBuilder.AddForeignKey(
                name: "FK_GoogleData_AspNetUsers_ApplicationUserId",
                table: "GoogleData",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
