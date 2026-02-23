using Cafeteria.Backend.Dtos;

namespace Cafeteria.Backend.Services
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaDto>> ObtenerCategorias();
        Task<IEnumerable<CategoriaDto>> ObtenerCategoriasActivas();
        Task<CategoriaDto?> ObtenerCategoriaPorId(int id);
        Task<CategoriaDto?> CrearCategoria(CategoriaDto dto);
        Task<CategoriaDto?> ActualizarCategoria(int id, CategoriaDto dto);
        Task<bool> EliminarCategoria(int id);
    }
}
