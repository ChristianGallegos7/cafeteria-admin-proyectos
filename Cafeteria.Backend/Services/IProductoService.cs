using Cafeteria.Backend.Dtos;
using Cafeteria.Backend.Models;

namespace Cafeteria.Backend.Services
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoDto>> ObtenerProductos();
        Task<IEnumerable<ProductoDto>> ObtenerProductosActivos();

        Task<ProductoDto> ObtenerProductoPorId(int id);
        Task<ProductoDto> CrearProducto(ProductoDto dto);
        Task<ProductoDto> ActualizarProducto(int id, ProductoDto dto);
        Task<bool> EliminarProducto(int id);
    }
}
