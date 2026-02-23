using System.ComponentModel.DataAnnotations.Schema;

namespace Cafeteria.Backend.Models
{
    public class Producto
    {
        public int Id { get; set; }

        public required string Nombre { get; set; }

        public string? Descripcion { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

        public string? ImagenUrl { get; set; }

        public bool EsActivo { get; set; }

        public bool EsDestacado { get; set; }

        public int TiempoPreparacion { get; set; }

        public int CategoriaId { get; set; }

        public Categoria? Categoria { get; set; }
    }
}
