using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PicEnfermagem.Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class alterrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_answer_AspNetUsers_ApplicationUserId",
                table: "answer");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "answer",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_answer_ApplicationUserId",
                table: "answer",
                newName: "IX_answer_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_answer_AspNetUsers_UserId",
                table: "answer",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_answer_AspNetUsers_UserId",
                table: "answer");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "answer",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_answer_UserId",
                table: "answer",
                newName: "IX_answer_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_answer_AspNetUsers_ApplicationUserId",
                table: "answer",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
