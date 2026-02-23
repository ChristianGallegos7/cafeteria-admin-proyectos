using Cafeteria.Backend.Dtos;
using Cafeteria.Backend.Models;
using Cafeteria.Backend.Repositorios;

namespace Cafeteria.Backend.Services.Impl
{
    public class ProductoServiceImpl : IProductoService
    {
        private readonly IProductosRepositorio _productoRepositorio;

        public ProductoServiceImpl(IProductosRepositorio productosRepositorio)
        {
            _productoRepositorio = productosRepositorio;
        }

        public async Task<ProductoDto?> ActualizarProducto(int id, ProductoDto dto)
        {
            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                ImagenUrl = dto.ImagenUrl,
                EsActivo = dto.EsActivo,
                EsDestacado = dto.EsDestacado,
                TiempoPreparacion = dto.TiempoPreparacion,
                CategoriaId = dto.CategoriaId
            };

            var productoActualizado = await _productoRepositorio.ActualizarProducto(id, producto);

            if (productoActualizado == null)
                return null;

            return MapToDto(productoActualizado);
        }

        public async Task<ProductoDto?> CrearProducto(ProductoDto dto)
        {
            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                ImagenUrl = dto.ImagenUrl,
                EsActivo = dto.EsActivo,
                EsDestacado = dto.EsDestacado,
                TiempoPreparacion = dto.TiempoPreparacion,
                CategoriaId = dto.CategoriaId
            };

            var productoCreado = await _productoRepositorio.CrearProducto(producto);

            if (productoCreado == null)
                return null;

            return MapToDto(productoCreado);
        }

        public async Task<bool> EliminarProducto(int id)
        {
            return await _productoRepositorio.EliminarProducto(id);
        }

        public async Task<ProductoDto?> ObtenerProductoPorId(int id)
        {
            var producto = await _productoRepositorio.ObtenerProductoPorId(id);

            if (producto == null)
                return null;

            return MapToDto(producto);
        }

        public async Task<IEnumerable<ProductoDto>> ObtenerProductos()
        {
            var productos = await _productoRepositorio.ObtenerProductos();
            return productos.Select(MapToDto);
        }

        public async Task<IEnumerable<ProductoDto>> ObtenerProductosActivos()
        {
            var productos = await _productoRepositorio.ObtenerProductosActivos();
            return productos.Select(MapToDto);
        }

        private static ProductoDto MapToDto(Producto p)
        {
            return new ProductoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Precio = p.Precio,
                ImagenUrl = p.ImagenUrl,
                EsActivo = p.EsActivo,
                EsDestacado = p.EsDestacado,
                TiempoPreparacion = p.TiempoPreparacion,
                CategoriaId = p.CategoriaId,
                NombreCategoria = p.Categoria?.Nombre
            };
        }
    }
}
