using Microsoft.AspNetCore.Mvc;

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
    }
}
