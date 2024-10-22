using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIService;
using TIEntities;

namespace TITesting
{
    public class ProductoServiceTest
    {
        ProductoService service = new ProductoService();

        [SetUp]
        public void Setup()
        {
        }

        [Test]  
        public void ValidarCompletitudProductoCompleto_OK()
        {
            var producto = new Producto() 
            {
                Marca = "Guaymallen",
                AltoCaja = 10,
                AnchoCaja = 10,
                ProfundidadCaja = 12,
                PrecioUnitario = 15,
                StockMinimo = 10,
                Stock = 200
            };

            //var result = service.ValidarCompletitudProducto(producto);

            //Assert.IsTrue(result.Success);
        }

        [Test]
        public void ValidarCompletitudProductoIncompleto_FALSE()
        {
            var producto = new Producto
            {
                Marca = null, 
                AltoCaja = 10,
                AnchoCaja = 20,
                ProfundidadCaja = 30,
                PrecioUnitario = 500,
                StockMinimo = 5,
                Stock = 10
            };

            //var result = service.ValidarCompletitudProducto(producto);

            //Assert.IsFalse(result.Success);
            //Assert.AreEqual("Falta la Marca", result.Message);
        }

    }
}
