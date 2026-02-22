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

        public async Task<ProductoDto> ActualizarProducto(int id, ProductoDto dto)
        {
            try
            {
                var productoExistente = await _productoRepositorio.ObtenerProductoPorId(id);

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

                await _productoRepositorio.ActualizarProducto(id, producto);

                return new ProductoDto
                {
                    Id = id,
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    Precio = producto.Precio,
                    ImagenUrl = producto.ImagenUrl,
                    EsActivo = producto.EsActivo,
                    EsDestacado = producto.EsDestacado,
                    TiempoPreparacion = producto.TiempoPreparacion,
                    CategoriaId = producto.CategoriaId
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ProductoDto> CrearProducto(ProductoDto dto)
        {
            try
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

                return new ProductoDto
                {
                    Id = productoCreado.Id,
                    Nombre = productoCreado.Nombre,
                    Descripcion = productoCreado.Descripcion,
                    Precio = productoCreado.Precio,
                    ImagenUrl = productoCreado.ImagenUrl,
                    EsActivo = productoCreado.EsActivo,
                    EsDestacado = productoCreado.EsDestacado,
                    TiempoPreparacion = productoCreado.TiempoPreparacion,
                    CategoriaId = productoCreado.CategoriaId
                };
            }

            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> EliminarProducto(int id)
        {
            try
            {
                return await _productoRepositorio.EliminarProducto(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ProductoDto> ObtenerProductoPorId(int id)
        {
            try
            {
                 var producto = await _productoRepositorio.ObtenerProductoPorId(id);
            
                return new ProductoDto
                {
                    Id = producto.Id,
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    Precio = producto.Precio,
                    ImagenUrl = producto.ImagenUrl,
                    EsActivo = producto.EsActivo,
                    EsDestacado = producto.EsDestacado,
                    TiempoPreparacion = producto.TiempoPreparacion,
                    CategoriaId = producto.CategoriaId
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<IEnumerable<ProductoDto>> ObtenerProductos()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductoDto>> ObtenerProductosActivos()
        {
            throw new NotImplementedException();
        }
    }
}
