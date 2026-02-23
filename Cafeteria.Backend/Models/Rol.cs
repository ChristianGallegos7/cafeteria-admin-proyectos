namespace Cafeteria.Backend.Models
{
    public class Rol
    {
        public int Id { get; set; }

        public required string Nombre { get; set; }

        public string? Descripcion { get; set; }

        public bool EsActivo { get; set; }
    }
}
