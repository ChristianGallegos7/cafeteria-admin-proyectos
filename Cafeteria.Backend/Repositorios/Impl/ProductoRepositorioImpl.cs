using Cafeteria.Backend.Data;
using Cafeteria.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Cafeteria.Backend.Repositorios.Impl
{
    public class ProductoRepositorioImpl : IProductosRepositorio
    {

        private readonly CafeteriaDbContext _context;

        public ProductoRepositorioImpl(CafeteriaDbContext context)
        {
            _context = context;
        }

        public async Task<Producto> ActualizarProducto(int id, Producto producto)
        {
            var productoDb = await _context.Productos.FindAsync(id);

            if (productoDb == null)
            {
                return null;
            }

            productoDb.Nombre = producto.Nombre;
            productoDb.Descripcion = producto.Descripcion;
            productoDb.Precio = producto.Precio;
            productoDb.ImagenUrl = producto.ImagenUrl;
            productoDb.EsActivo = producto.EsActivo;
            productoDb.EsDestacado = producto.EsDestacado;
            productoDb.TiempoPreparacion = producto.TiempoPreparacion;
            productoDb.CategoriaId = producto.CategoriaId;

            await _context.SaveChangesAsync();

            return productoDb;
        }

        public async Task<Producto> CrearProducto(Producto producto)
        {
            await _context.Productos.AddAsync(producto);
            var resultado = await _context.SaveChangesAsync();

            return resultado > 0 ? producto : null;
        }

        public async Task<bool> EliminarProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return false;
            }

            producto.EsActivo = false;

            producto.EsDestacado = false;

            var resultado = await _context.SaveChangesAsync();

            return resultado > 0;
        }

        public async Task<Producto> ObtenerProductoPorId(int id)
        {
           return await _context.Productos.FindAsync(id);
        }

        public async Task<IEnumerable<Producto>> ObtenerProductos()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task<IEnumerable<Producto>> ObtenerProductosActivos()
        {
            return await _context.Productos.Where(p => p.EsActivo).ToListAsync();
        }
    }
}
