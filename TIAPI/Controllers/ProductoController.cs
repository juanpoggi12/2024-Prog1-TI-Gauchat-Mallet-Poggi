using EntitiesDTO;
using Microsoft.AspNetCore.Mvc;
using TIService;

namespace TIAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : ControllerBase
    {
        private ProductoService productoService;

        public ProductoController()
        {
            productoService = new ProductoService();
        }

        [HttpPost()]
        public IActionResult Post([FromBody] ProductoDTO productoDTO)
        {
            var Result = productoService.AgergarProducto(productoDTO);
            if (!Result.Success)
            {
                return BadRequest(Result.Message);
            }
            return Ok(Result.Message);
        }

        [HttpPut("{Codigo}")]
        public IActionResult Put(int Codigo, int CantidadAActualizar)
        {
            var Result = productoService.ActualizarStockProducto(Codigo, CantidadAActualizar);
            if (!Result.Success)
            {
                return NotFound(new { message = Result.Message });
            }
            return Ok(new { message = Result.Message});
        }
    }
}