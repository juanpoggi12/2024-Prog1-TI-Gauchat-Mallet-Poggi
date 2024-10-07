using EntitiesDTO;
using Microsoft.AspNetCore.Mvc;
using TIService;

namespace TIAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private ClienteService clienteService;

        public ClienteController()
        {
            clienteService = new ClienteService();
        }

        [HttpPost()]
        public IActionResult Post([FromBody] ClienteDTO clienteDTO)
        {
            var Result = clienteService.AgregarCliente(clienteDTO);
            if (Result.Success == false)
            {
                return BadRequest(Result.Message);
            }
            return Ok();
        }

        [HttpGet()]
        public IActionResult Get()
        {
            var Result = clienteService.ObtenerClientes();
            return Ok(Result);
        }

        [HttpDelete("{Dni}")]
        public IActionResult Delete(int DNI)
        {
            var Result = clienteService.EliminarCliente(DNI);
            if (!Result.Success)
            {
                return NotFound(Result.Message);
            }

            return Ok();
        }

        [HttpPut("{DNI}")]
        public IActionResult Put(int DNI, [FromBody] ClienteDTO clienteDTO)
        {
            var Resut = clienteService.EditarCliente(DNI, clienteDTO);
            if (!Resut.Success)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}