using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFileIdToCargaArchivo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RutaArchivo",
                table: "CargaArchivo");

            migrationBuilder.AddColumn<string>(
                name: "FileId",
                table: "CargaArchivo",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileId",
                table: "CargaArchivo");

            migrationBuilder.AddColumn<string>(
                name: "RutaArchivo",
                table: "CargaArchivo",
                type: "text",
                nullable: true);
        }
    }
}
