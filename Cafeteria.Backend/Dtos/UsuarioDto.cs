using Cafeteria.Backend.Models;

namespace Cafeteria.Backend.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Correo { get; set; }

        // La clave suele omitirse en DTOs de lectura por seguridad
        public string Clave { get; set; }

        public bool EsActivo { get; set; }

        // Solo el ID para vincular el Rol
        public int RolId { get; set; }

        // Opcional: Solo el nombre del rol para mostrarlo en el frontend
        public string? NombreRol { get; set; }

        public string? Token { get; set; }
    }
}