using Microsoft.EntityFrameworkCore.Migrations;

namespace Shelter.Migrations
{
    public partial class RelationlWithUserMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Requesteds",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Dogs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requesteds_UserId",
                table: "Requesteds",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Dogs_UserId",
                table: "Dogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dogs_AspNetUsers_UserId",
                table: "Dogs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requesteds_AspNetUsers_UserId",
                table: "Requesteds",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dogs_AspNetUsers_UserId",
                table: "Dogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Requesteds_AspNetUsers_UserId",
                table: "Requesteds");

            migrationBuilder.DropIndex(
                name: "IX_Requesteds_UserId",
                table: "Requesteds");

            migrationBuilder.DropIndex(
                name: "IX_Dogs_UserId",
                table: "Dogs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Requesteds");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Dogs");
        }
    }
}
