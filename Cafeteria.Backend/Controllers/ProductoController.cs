using Cafeteria.Backend.Dtos;
using Cafeteria.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cafeteria.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> ObtenerProductos()
        {
            var productos = await _productoService.ObtenerProductos();
            return Ok(productos);
        }

        [HttpGet("activos")]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> ObtenerProductosActivos()
        {
            var productos = await _productoService.ObtenerProductosActivos();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> ObtenerProductoPorId(int id)
        {
            var producto = await _productoService.ObtenerProductoPorId(id);

            if (producto == null)
                return NotFound(new { mensaje = "Producto no encontrado" });

            return Ok(producto);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ProductoDto>> CrearProducto([FromBody] ProductoDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                return BadRequest(new { mensaje = "El nombre del producto es requerido" });

            if (dto.Precio <= 0)
                return BadRequest(new { mensaje = "El precio debe ser mayor a 0" });

            var producto = await _productoService.CrearProducto(dto);

            if (producto == null)
                return BadRequest(new { mensaje = "No se pudo crear el producto" });

            return CreatedAtAction(nameof(ObtenerProductoPorId), new { id = producto.Id }, producto);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductoDto>> ActualizarProducto(int id, [FromBody] ProductoDto dto)
        {
            var productoExistente = await _productoService.ObtenerProductoPorId(id);

            if (productoExistente == null)
                return NotFound(new { mensaje = "Producto no encontrado" });

            var producto = await _productoService.ActualizarProducto(id, dto);
            return Ok(producto);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarProducto(int id)
        {
            var productoExistente = await _productoService.ObtenerProductoPorId(id);

            if (productoExistente == null)
                return NotFound(new { mensaje = "Producto no encontrado" });

            await _productoService.EliminarProducto(id);
            return Ok(new { mensaje = "Producto eliminado correctamente" });
        }
    }
}
