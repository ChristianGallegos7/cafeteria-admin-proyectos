using Cafeteria.Backend.Dtos;
using Cafeteria.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cafeteria.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [Route("iniciarSesion")]
        public async Task<IActionResult> IniciarSesion(LoginDto dto)
        {
            try
            {
                var usuario = await _usuarioService.IniciarSesion(dto.Correo, dto.Clave);

                return Ok(usuario);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("registrar")]
        public async Task<IActionResult> CrearUsuario(UsuarioDto usuarioDto)
        {
            try
            {
                var usuario = await _usuarioService.CrearUsuario(usuarioDto);
                return Ok(usuario);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
