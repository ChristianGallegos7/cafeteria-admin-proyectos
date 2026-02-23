using Cafeteria.Backend.Models;

namespace Cafeteria.Backend.Repositorios
{
    public interface IProductosRepositorio
    {
        Task<IEnumerable<Producto>> ObtenerProductos();

        Task<IEnumerable<Producto>> ObtenerProductosActivos();
        Task<Producto?> ObtenerProductoPorId(int id);

        Task<Producto?> CrearProducto(Producto producto);

        Task<Producto?> ActualizarProducto(int id, Producto producto);

        Task<bool> EliminarProducto(int id);
    }
}
