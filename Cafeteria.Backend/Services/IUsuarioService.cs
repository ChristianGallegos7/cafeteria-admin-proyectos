using Cafeteria.Backend.Dtos;
using Cafeteria.Backend.Models;

namespace Cafeteria.Backend.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDto>> ObtenerUsuarios();
        Task<UsuarioDto> ObtenerUsuarioPorId(int id);
        Task<UsuarioDto> ObtenerUsuarioPorCorreo(string correo);
        Task<UsuarioDto> CrearUsuario(UsuarioDto usuario);
        Task<bool> ExisteCorreo(string correo);
        Task<UsuarioDto> IniciarSesion(string correo, string clave);
    }
}
