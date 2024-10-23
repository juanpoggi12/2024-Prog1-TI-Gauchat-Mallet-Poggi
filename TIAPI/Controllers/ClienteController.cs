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
            clienteService.AgregarCliente(clienteDTO);
            if (!ModelState.IsValid) { 
            return BadRequest(ModelState);
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (result.Status == 404)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Message);
        }
    }
}