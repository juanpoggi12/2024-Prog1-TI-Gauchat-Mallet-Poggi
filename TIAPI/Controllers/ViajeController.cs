using EntitiesDTO;
using Microsoft.AspNetCore.Mvc;
using TIService;

namespace TIAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViajeController : Controller
    {
        private ViajeService viajeService;

        public ViajeController()
        {
            viajeService = new ViajeService();
        }

        [HttpPost()]
        public IActionResult Post([FromBody] ViajeDTO viajeDTO)
        {
            var result = viajeService.AgregarViaje(viajeDTO);
            if (result.Success == false)
            {
                return BadRequest(result.Message);
            }
            return Ok();
        }
    }
}
