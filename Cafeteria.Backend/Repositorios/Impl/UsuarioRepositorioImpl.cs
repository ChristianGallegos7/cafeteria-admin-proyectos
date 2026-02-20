using Cafeteria.Backend.Data;
using Cafeteria.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Cafeteria.Backend.Repositorios.Impl
{
    public class UsuarioRepositorioImpl : IUsuarioRepositorio
    {
        private readonly CafeteriaDbContext _context;

        public UsuarioRepositorioImpl(CafeteriaDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> CrearUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            return await _context.SaveChangesAsync() > 0 ? usuario : null;
        }

        public async Task<bool> ExisteCorreo(string correo)
        {
            var existe = await _context.Usuarios.AnyAsync(u => u.Correo == correo);
            return existe;
        }

        public async Task<Usuario> ObtenerUsuarioPorCorreo(string correo)
        {
            return await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Correo == correo);
        }

        public async Task<Usuario> ObtenerUsuarioPorId(int id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Usuario>> ObtenerUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }
    }
}
