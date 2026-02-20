namespace Cafeteria.Backend.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Correo { get; set; }

        public string Clave { get; set; }

        public bool EsActivo { get; set; }

        public int IdRol { get; set; }

        public Rol Rol { get; set; }
    }
}
