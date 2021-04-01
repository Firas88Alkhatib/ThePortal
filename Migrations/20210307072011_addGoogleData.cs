using Microsoft.EntityFrameworkCore.Migrations;

namespace ThePortal.Migrations
{
    public partial class addGoogleData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GoogleDataId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GoogleData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccessToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoogleData", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_GoogleData_GoogleDataId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "GoogleData");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GoogleDataId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GoogleDataId",
                table: "AspNetUsers");
        }
    }
}
