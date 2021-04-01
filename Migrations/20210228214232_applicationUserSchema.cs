using Microsoft.EntityFrameworkCore.Migrations;

namespace ThePortal.Migrations
{
    public partial class applicationUserSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BusinessName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_FacebookData_FacebookDataId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FacebookDataId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BusinessName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FacebookDataId",
                table: "AspNetUsers");
        }
    }
}
