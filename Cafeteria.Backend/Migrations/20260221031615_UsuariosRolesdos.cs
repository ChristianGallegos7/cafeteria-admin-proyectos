using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cafeteria.Backend.Migrations
{
    /// <inheritdoc />
    public partial class UsuariosRolesdos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdRol",
                table: "Usuarios");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdRol",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
