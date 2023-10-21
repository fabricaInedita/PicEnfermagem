using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PicEnfermagem.Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Course",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StudentCode",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Course",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StudentCode",
                table: "AspNetUsers");
        }
    }
}
