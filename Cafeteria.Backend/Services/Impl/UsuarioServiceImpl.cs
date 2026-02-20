using Cafeteria.Backend.Dtos;
using Cafeteria.Backend.Models;
using Cafeteria.Backend.Repositorios;
using Cafeteria.Backend.Token;

namespace Cafeteria.Backend.Services.Impl
{
    public class UsuarioServiceImpl : IUsuarioService
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IGenerateToken _generateToken;

        public UsuarioServiceImpl(IUsuarioRepositorio usuarioRepositorio, IGenerateToken generateToken)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _generateToken = generateToken;
        }

        public async Task<UsuarioDto> CrearUsuario(UsuarioDto dto)
        {
            try
            {
                var usuario = new Usuario
                {
                    Nombre = dto.Nombre,
                    Apellido = dto.Apellido,
                    Correo = dto.Correo,
                    Clave = BCrypt.Net.BCrypt.HashPassword(dto.Clave),
                    IdRol = dto.IdRol,
                    EsActivo = true
                };

                var usuarioCreado = await _usuarioRepositorio.CrearUsuario(usuario);

                if (usuarioCreado == null)
                {
                    throw new Exception("No se pudo crear el usuario");
                }

                return new UsuarioDto
                {
                    Id = usuarioCreado.Id,
                    Nombre = usuarioCreado.Nombre,
                    Apellido = usuarioCreado.Apellido,
                    Correo = usuarioCreado.Correo,
                    EsActivo = usuarioCreado.EsActivo,
                    IdRol = usuarioCreado.IdRol,
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> ExisteCorreo(string correo)
        {
            try
            {
                return await _usuarioRepositorio.ExisteCorreo(correo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UsuarioDto> IniciarSesion(string correo, string clave)
        {
            try
            {
                var existe = await _usuarioRepositorio.ExisteCorreo(correo);

                if (!existe)
                {
                    throw new Exception("Correo no registrado");
                }

                var usuario = await _usuarioRepositorio.ObtenerUsuarioPorCorreo(correo);

                if (!BCrypt.Net.BCrypt.Verify(clave, usuario.Clave))
                {
                    throw new Exception("Clave incorrecta");
                }

                if (!usuario.EsActivo)
                {
                    throw new Exception("El usuario se encuentra inactivo");
                }

                var nombreRol = usuario.Rol?.Nombre ?? string.Empty;
                var token = await _generateToken.GenerarToken(usuario.Id, usuario.Correo, usuario.IdRol, nombreRol);

                return new UsuarioDto
                {
                    Id = usuario.Id,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Correo = usuario.Correo,
                    IdRol = usuario.IdRol,
                    EsActivo = usuario.EsActivo,
                    NombreRol = nombreRol,
                    Token = token
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UsuarioDto> ObtenerUsuarioPorCorreo(string correo)
        {
            try
            {
                var usuario = await _usuarioRepositorio.ObtenerUsuarioPorCorreo(correo);
                if (usuario == null)
                {
                    throw new Exception("Usuario no encontrado");
                }

                return new UsuarioDto
                {
                    Id = usuario.Id,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Correo = usuario.Correo,
                    IdRol = usuario.IdRol,
                    EsActivo = usuario.EsActivo
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UsuarioDto> ObtenerUsuarioPorId(int id)
        {
            try
            {
                var usuario = await _usuarioRepositorio.ObtenerUsuarioPorId(id);
                if (usuario == null)
                {
                    throw new Exception("Usuario no encontrado");
                }

                return new UsuarioDto
                {
                    Id = usuario.Id,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Correo = usuario.Correo,
                    IdRol = usuario.IdRol,
                    EsActivo = usuario.EsActivo
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<UsuarioDto>> ObtenerUsuarios()
        {
            try
            {
                var usuarios = await _usuarioRepositorio.ObtenerUsuarios();

                return usuarios.Select(u => new UsuarioDto
                {
                    Id = u.Id,
                    Nombre = u.Nombre,
                    Apellido = u.Apellido,
                    Correo = u.Correo,
                    IdRol = u.IdRol,
                    EsActivo = u.EsActivo
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
