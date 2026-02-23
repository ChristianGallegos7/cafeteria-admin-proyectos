namespace Cafeteria.Backend.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public required string Nombre { get; set; }

        public required string Apellido { get; set; }

        public required string Correo { get; set; }

        public required string Clave { get; set; }

        public bool EsActivo { get; set; }

        public int RolId { get; set; }

        public Rol? Rol { get; set; }
    }
}
