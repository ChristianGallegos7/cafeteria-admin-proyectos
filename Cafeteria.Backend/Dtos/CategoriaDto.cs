namespace Cafeteria.Backend.Dtos
{
    public class CategoriaDto
    {
        public int Id { get; set; }

        public required string Nombre { get; set; }

        public string? Descripcion { get; set; }

        public bool EsActivo { get; set; }

        public int CantidadProductos { get; set; }
    }
}
