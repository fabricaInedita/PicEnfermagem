using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PicEnfermagem.Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class novacolunaemalternative : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "alternative",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "alternative");
        }
    }
}
