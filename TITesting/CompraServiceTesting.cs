using EntitiesDTO;
using TIEntities;
using TIService;

namespace TITesting
{
    public class CompraServiceTesting
    {
        private CompraService compraService = new CompraService();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AgregarCompraTestProductoError()
        {
            CompraDTO compraDTO = new CompraDTO()
            {
                CodigoProducto = 14,
                DniCliente = 46449991,
                CantidadComprada = 10,
                FechaEntregaSolicitada = DateTime.Now
            };

            Result result = compraService.AgregarCompra(compraDTO);

            Assert.That(result.Message, Is.EqualTo("Producto no encontrado"));
            Assert.That(result.Status, Is.EqualTo(404));
        }

        [Test]
        public void AgregarCompraTestClienteError()
        {
            CompraDTO compraDTO = new CompraDTO()
            {
                CodigoProducto = 1,
                DniCliente = 46449992,
                CantidadComprada = 5,
                FechaEntregaSolicitada = DateTime.Now
            };

            Result result = compraService.AgregarCompra(compraDTO);

            Assert.That(result.Message, Is.EqualTo("Cliente no encontrado"));
            Assert.That(result.Status, Is.EqualTo(404));
        }

        [Test]
        public void AgregarCompraTestOkey()
        {
            CompraDTO compraDTO = new CompraDTO()
            {
                CodigoProducto = 14,
                DniCliente = 46449991,
                CantidadComprada = 10,
                FechaEntregaSolicitada = DateTime.Now
            };

            Result result = compraService.AgregarCompra(compraDTO);

            Assert.That(result.Message, Is.EqualTo("Producto no encontrado"));
            Assert.That(result.Status, Is.EqualTo(404));
        }
    }
}