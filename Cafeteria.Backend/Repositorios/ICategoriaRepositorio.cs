using Cafeteria.Backend.Models;

namespace Cafeteria.Backend.Repositorios
{
    public interface ICategoriaRepositorio
    {
        Task<IEnumerable<Categoria>> ObtenerCategorias();
        Task<IEnumerable<Categoria>> ObtenerCategoriasActivas();
        Task<Categoria?> ObtenerCategoriaPorId(int id);
        Task<Categoria?> CrearCategoria(Categoria categoria);
        Task<Categoria?> ActualizarCategoria(int id, Categoria categoria);
        Task<bool> EliminarCategoria(int id);
    }
}
