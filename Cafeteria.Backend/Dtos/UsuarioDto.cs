namespace Cafeteria.Backend.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }

        public required string Nombre { get; set; }

        public required string Apellido { get; set; }

        public required string Correo { get; set; }

        public string? Clave { get; set; }

        public bool EsActivo { get; set; }

        public int RolId { get; set; }

        public string? NombreRol { get; set; }

        public string? Token { get; set; }
    }
}