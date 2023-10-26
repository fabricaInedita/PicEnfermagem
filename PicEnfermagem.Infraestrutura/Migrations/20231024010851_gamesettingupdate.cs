using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PicEnfermagem.Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class gamesettingupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "gamesetting",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_gamesetting_UserId",
                table: "gamesetting",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_gamesetting_AspNetUsers_UserId",
                table: "gamesetting",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_gamesetting_AspNetUsers_UserId",
                table: "gamesetting");

            migrationBuilder.DropIndex(
                name: "IX_gamesetting_UserId",
                table: "gamesetting");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "gamesetting");
        }
    }
}
