using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PicEnfermagem.Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCorrectAnswer",
                table: "answer");

            migrationBuilder.RenameColumn(
                name: "SecondsAnswer",
                table: "answer",
                newName: "Punctuation");

            migrationBuilder.AddColumn<int>(
                name: "Punctuation",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Punctuation",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Punctuation",
                table: "answer",
                newName: "SecondsAnswer");

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrectAnswer",
                table: "answer",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
