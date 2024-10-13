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
            if (Result.Status == 404)
            {
                return NotFound(Result.Message);
            }else if (Result.Status == 400)
            {
                return BadRequest(Result.Message);
            }

            return Ok(Result.Message);
        }
    }
}