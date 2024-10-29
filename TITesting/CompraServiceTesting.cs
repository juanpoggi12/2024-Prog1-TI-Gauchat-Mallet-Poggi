using EntitiesDTO;
using TIData;
using TIEntities;
using TIService;

namespace TITesting
{
    public class CompraServiceTesting
    {
        private CompraService compraService = new CompraService();
        private string archivoTemporalCompras;
        private string rutaOriginalCompras;
        private string archivoTemporalProductos;
        private string rutaOriginalProductos;
        private string archivoTemporalClientes;
        private string rutaOriginalClientes;
        [SetUp]
        public void Setup()
        {
            rutaOriginalCompras = Path.GetFullPath(Path.Combine("TIAPI", "compra.json"));
            archivoTemporalCompras = Path.Combine(Path.GetTempPath(), "CompraTest.json");
            CompraFiles.SetRutaArchivo(archivoTemporalCompras);
            rutaOriginalProductos = Path.GetFullPath(Path.Combine("TIAPI", "producto.json"));
            archivoTemporalProductos = Path.Combine(Path.GetTempPath(), "ProductoTest.json");
            rutaOriginalClientes = Path.GetFullPath(Path.Combine("TIAPI", "cliente.json"));
            archivoTemporalClientes = Path.Combine(Path.GetTempPath(), "ClienteTest.json");
            ProductoFiles.SetRutaArchivo(archivoTemporalProductos);
            Producto producto = new Producto { 
            Codigo = 1,
            Stock = 20,
            AltoCaja = 10,
            AnchoCaja = 10,
            StockMinimo = 10,
            Marca = "Marca",
            Nombre = "Nombre",
            PrecioUnitario = 10,
            ProfundidadCaja = 10,
            };
            ProductoFiles.EscribirProductosAJson(producto);
        }

        [Test]
        public void AgregarCompraTestProductoError()
        {
            CompraDTO compraDTO = new CompraDTO()
            {
                CodigoProducto = 14,
                DniCliente = 46449991,
                CantidadComprada = 10,
                FechaEntregaSolicitada = DateTime.Now.AddDays(2)
            };

            Result result = compraService.AgregarCompra(compraDTO);

            Assert.That(result.Message, Is.EqualTo("Producto no encontrado"));
            Assert.That(result.Status, Is.EqualTo(404));
        }

        [Test]
        public void AgregarCompraTestClienteError()
        {
            Cliente cliente = new Cliente
            {
                Dni = 45950945,
                FechaDeNacimiento = DateTime.Now,
                Apellido = "Apellido",
                Email = "n@gmail.com",
                FechaCreacion = DateTime.Now.AddDays(1),
                Nombre = "Nombre",
                Latitud = 20,
                Longitud = 20,
                Telefono = "3232323232"
            };
            ClienteFiles.EscribirClienteAJson(cliente);
            CompraDTO compraDTO = new CompraDTO()
            {
                CodigoProducto = 1,
                DniCliente = 46449992,
                CantidadComprada = 5,
                FechaEntregaSolicitada = DateTime.Now.AddDays(10),
            };

            Result result = compraService.AgregarCompra(compraDTO);

            Assert.That(result.Message, Is.EqualTo("Cliente no encontrado"));
            Assert.That(result.Status, Is.EqualTo(404));
        }

        [Test]
        public void AgregarCompraTestOkey()
        {
            Cliente cliente = new Cliente
            {
                Dni = 45950945,
                FechaDeNacimiento = DateTime.Now,
                Apellido = "Apellido",
                Email = "n@gmail.com",
                FechaCreacion = DateTime.Now.AddDays(1),
                Nombre = "Nombre",
                Latitud = 20,
                Longitud = 20,
                Telefono = "3232323232"
            };
                ClienteFiles.EscribirClienteAJson(cliente);
            CompraDTO compraDTO = new CompraDTO()
            {
                CodigoProducto = 1,
                DniCliente = 45950945,
                CantidadComprada = 10,
                FechaEntregaSolicitada = DateTime.Now.AddDays(20)
            };

            Result result = compraService.AgregarCompra(compraDTO);

          
            Assert.That(result.Message, Is.EqualTo("La compra se agrego correctamente"));
        }
        [TearDown]
        public void TearDown()
        {
            if (File.Exists(archivoTemporalCompras))
            {
                File.Delete(archivoTemporalCompras);
            }
            if (File.Exists(archivoTemporalProductos))
            {
                File.Delete(archivoTemporalProductos);
            }
            if (File.Exists(archivoTemporalClientes))
            {
                File.Delete(archivoTemporalClientes);
            }
            CompraFiles.SetRutaArchivo(rutaOriginalCompras);
            ProductoFiles.SetRutaArchivo(rutaOriginalProductos);
            ClienteFiles.SetRutaArchivo(rutaOriginalClientes);
        }
    }
}