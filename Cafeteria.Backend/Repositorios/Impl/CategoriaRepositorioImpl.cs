using Cafeteria.Backend.Data;
using Cafeteria.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Cafeteria.Backend.Repositorios.Impl
{
    public class CategoriaRepositorioImpl : ICategoriaRepositorio
    {
        private readonly CafeteriaDbContext _context;

        public CategoriaRepositorioImpl(CafeteriaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Categoria>> ObtenerCategorias()
        {
            return await _context.Categorias
                .Include(c => c.Productos)
                .ToListAsync();
        }

        public async Task<IEnumerable<Categoria>> ObtenerCategoriasActivas()
        {
            return await _context.Categorias
                .Include(c => c.Productos)
                .Where(c => c.EsActivo)
                .ToListAsync();
        }

        public async Task<Categoria?> ObtenerCategoriaPorId(int id)
        {
            return await _context.Categorias
                .Include(c => c.Productos)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Categoria?> CrearCategoria(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            var resultado = await _context.SaveChangesAsync();

            return resultado > 0 ? categoria : null;
        }

        public async Task<Categoria?> ActualizarCategoria(int id, Categoria categoria)
        {
            var categoriaDb = await _context.Categorias.FindAsync(id);

            if (categoriaDb == null)
                return null;

            categoriaDb.Nombre = categoria.Nombre;
            categoriaDb.Descripcion = categoria.Descripcion;
            categoriaDb.EsActivo = categoria.EsActivo;

            await _context.SaveChangesAsync();

            return categoriaDb;
        }

        public async Task<bool> EliminarCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
                return false;

            categoria.EsActivo = false;
            var resultado = await _context.SaveChangesAsync();

            return resultado > 0;
        }
    }
}
