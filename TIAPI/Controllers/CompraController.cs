using EntitiesDTO;
using Microsoft.AspNetCore.Mvc;
using TIService;

namespace TIAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompraController : ControllerBase
    {
        private CompraService compraService { get; set; }

        public CompraController()
        {
            compraService = new CompraService();
        }

        [HttpPost()]
        public IActionResult Post([FromBody] CompraDTO compraDTO)
        {
            var Result = compraService.AgregarCompra(compraDTO);
            if (!Result.Success && Result.Message == "Cliente no encontrado")
            {
                return NotFound(Result.Message);
            }

            if (!Result.Success)
            {
                return BadRequest(Result.Message);
            }

            return Ok();
        }
    }
}