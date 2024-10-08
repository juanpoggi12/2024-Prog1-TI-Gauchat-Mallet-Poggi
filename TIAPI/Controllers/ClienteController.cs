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

        [HttpDelete("{dni:int}")]
        public IActionResult Delete(int dni)
        {
            var result = clienteService.EliminarCliente(dni);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }

            return Ok();
        }

        [HttpPut("{dni:int}")]
        public IActionResult Put(int dni, [FromBody] ClienteDTO clienteDTO)
        {
            var result = clienteService.EditarCliente(dni, clienteDTO);
            if (result.Status == 400)
            {
                return BadRequest(result.Message);
            } else if (result.Status == 404)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Message);
        }
    }
}