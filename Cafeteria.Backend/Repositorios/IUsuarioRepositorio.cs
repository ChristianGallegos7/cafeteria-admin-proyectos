using Cafeteria.Backend.Models;

namespace Cafeteria.Backend.Repositorios
{
    public interface IUsuarioRepositorio
    {
        Task<IEnumerable<Usuario>> ObtenerUsuarios();
        Task<Usuario> ObtenerUsuarioPorId(int id);
        Task<Usuario> ObtenerUsuarioPorCorreo(string correo);
        Task<Usuario> CrearUsuario(Usuario usuario);
        Task<bool> ExisteCorreo(string correo);
    }
}
