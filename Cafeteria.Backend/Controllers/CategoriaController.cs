using Cafeteria.Backend.Dtos;
using Cafeteria.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cafeteria.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDto>>> ObtenerCategorias()
        {
            var categorias = await _categoriaService.ObtenerCategorias();
            return Ok(categorias);
        }

        [HttpGet("activas")]
        public async Task<ActionResult<IEnumerable<CategoriaDto>>> ObtenerCategoriasActivas()
        {
            var categorias = await _categoriaService.ObtenerCategoriasActivas();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaDto>> ObtenerCategoriaPorId(int id)
        {
            var categoria = await _categoriaService.ObtenerCategoriaPorId(id);

            if (categoria == null)
                return NotFound(new { mensaje = "Categoría no encontrada" });

            return Ok(categoria);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CategoriaDto>> CrearCategoria([FromBody] CategoriaDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                return BadRequest(new { mensaje = "El nombre de la categoría es requerido" });

            var categoria = await _categoriaService.CrearCategoria(dto);
            return CreatedAtAction(nameof(ObtenerCategoriaPorId), new { id = categoria?.Id }, categoria);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoriaDto>> ActualizarCategoria(int id, [FromBody] CategoriaDto dto)
        {
            var categoriaExistente = await _categoriaService.ObtenerCategoriaPorId(id);

            if (categoriaExistente == null)
                return NotFound(new { mensaje = "Categoría no encontrada" });

            var categoria = await _categoriaService.ActualizarCategoria(id, dto);
            return Ok(categoria);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarCategoria(int id)
        {
            var categoriaExistente = await _categoriaService.ObtenerCategoriaPorId(id);

            if (categoriaExistente == null)
                return NotFound(new { mensaje = "Categoría no encontrada" });

            await _categoriaService.EliminarCategoria(id);
            return Ok(new { mensaje = "Categoría eliminada correctamente" });
        }
    }
}
