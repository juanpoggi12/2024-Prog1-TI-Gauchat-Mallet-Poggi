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
            var result = clienteService.AgregarCliente(clienteDTO);
            if (result.Success == false)
            {
                return BadRequest(result.Message);
            }
            return Ok();
        }

        [HttpGet()]
        public IActionResult Get()
        {
            var result = clienteService.ObtenerClientes();
            return Ok(result);
        }

        [HttpDelete("{Dni}")]
        public IActionResult Delete(int DNI)
        {
            var result = clienteService.EliminarCliente(DNI);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }

            return Ok();
        }

        [HttpPut("{DNI}")]
        public IActionResult Put(int DNI, [FromBody] ClienteDTO clienteDTO)
        {
            var result = clienteService.EditarCliente(DNI, clienteDTO);
            if (result.Status == 400)
            {
                return BadRequest();
            } else if (result.Status == 404)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}