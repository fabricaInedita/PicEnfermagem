using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PicEnfermagem.Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class renametableanswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_AspNetUsers_ApplicationUserId",
                table: "Answer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answer",
                table: "Answer");

            migrationBuilder.RenameTable(
                name: "Answer",
                newName: "answer");

            migrationBuilder.RenameIndex(
                name: "IX_Answer_ApplicationUserId",
                table: "answer",
                newName: "IX_answer_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_answer",
                table: "answer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_answer_AspNetUsers_ApplicationUserId",
                table: "answer",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_answer_AspNetUsers_ApplicationUserId",
                table: "answer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_answer",
                table: "answer");

            migrationBuilder.RenameTable(
                name: "answer",
                newName: "Answer");

            migrationBuilder.RenameIndex(
                name: "IX_answer_ApplicationUserId",
                table: "Answer",
                newName: "IX_Answer_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answer",
                table: "Answer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_AspNetUsers_ApplicationUserId",
                table: "Answer",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
