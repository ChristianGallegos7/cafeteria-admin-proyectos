using Cafeteria.Backend.Dtos;
using Cafeteria.Backend.Models;
using Cafeteria.Backend.Repositorios;

namespace Cafeteria.Backend.Services.Impl
{
    public class CategoriaServiceImpl : ICategoriaService
    {
        private readonly ICategoriaRepositorio _categoriaRepositorio;

        public CategoriaServiceImpl(ICategoriaRepositorio categoriaRepositorio)
        {
            _categoriaRepositorio = categoriaRepositorio;
        }

        public async Task<IEnumerable<CategoriaDto>> ObtenerCategorias()
        {
            var categorias = await _categoriaRepositorio.ObtenerCategorias();
            return categorias.Select(MapToDto);
        }

        public async Task<IEnumerable<CategoriaDto>> ObtenerCategoriasActivas()
        {
            var categorias = await _categoriaRepositorio.ObtenerCategoriasActivas();
            return categorias.Select(MapToDto);
        }

        public async Task<CategoriaDto?> ObtenerCategoriaPorId(int id)
        {
            var categoria = await _categoriaRepositorio.ObtenerCategoriaPorId(id);

            if (categoria == null)
                return null;

            return MapToDto(categoria);
        }

        public async Task<CategoriaDto?> CrearCategoria(CategoriaDto dto)
        {
            var categoria = new Categoria
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                EsActivo = dto.EsActivo
            };

            var categoriaCreada = await _categoriaRepositorio.CrearCategoria(categoria);

            if (categoriaCreada == null)
                return null;

            return MapToDto(categoriaCreada);
        }

        public async Task<CategoriaDto?> ActualizarCategoria(int id, CategoriaDto dto)
        {
            var categoria = new Categoria
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                EsActivo = dto.EsActivo
            };

            var categoriaActualizada = await _categoriaRepositorio.ActualizarCategoria(id, categoria);

            if (categoriaActualizada == null)
                return null;

            return MapToDto(categoriaActualizada);
        }

        public async Task<bool> EliminarCategoria(int id)
        {
            return await _categoriaRepositorio.EliminarCategoria(id);
        }

        private static CategoriaDto MapToDto(Categoria c)
        {
            return new CategoriaDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                EsActivo = c.EsActivo,
                CantidadProductos = c.Productos?.Count(p => p.EsActivo) ?? 0
            };
        }
    }
}
