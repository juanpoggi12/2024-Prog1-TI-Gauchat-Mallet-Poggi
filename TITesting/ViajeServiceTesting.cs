using EntitiesDTO;
using TIData;
using TIEntities;
using TIService;

namespace TITesting
{
    public class ViajeServiceTesting
    {
        private ViajeService viajeService;

        private string archivoTemporalViajes;
        private string rutaOriginalViajes;
        private string archivoTemporalCompras;
        private string rutaOriginalCompras;
        private string archivoTemporalCamionetas;
        private string rutaOriginalCamionetas;
        private string archivoTemporalProductos;
        private string rutaOriginalProductos;

        [SetUp]
        public void Setup()
        {
            viajeService = new ViajeService();

            rutaOriginalViajes = Path.GetFullPath(Path.Combine("TIAPI", "Viaje.Json"));
            archivoTemporalViajes = Path.Combine(Path.GetTempPath(), "ViajeTest.json");
            ViajeFiles.SetRutaArchivo(archivoTemporalViajes);

            rutaOriginalCompras = Path.GetFullPath(Path.Combine("TIAPI", "compra.json"));
            archivoTemporalCompras = Path.Combine(Path.GetTempPath(), "CompraTest.json");
            CompraFiles.SetRutaArchivo(archivoTemporalCompras);

            rutaOriginalCamionetas = Path.GetFullPath(Path.Combine("TIAPI", "camioneta.json"));
            archivoTemporalCamionetas = Path.Combine(Path.GetTempPath(), "CamionetaTest.json");
            CamionetaFiles.SetRutaArchivo(archivoTemporalCamionetas);

            rutaOriginalProductos = Path.GetFullPath(Path.Combine("TIAPI", "producto.json"));
            archivoTemporalProductos = Path.Combine(Path.GetTempPath(), "ProductoTest.json");
            ProductoFiles.SetRutaArchivo(archivoTemporalProductos);

            Viaje viaje = new Viaje()
            {
                FechaDesde = new DateTime(2025, 05, 22),
                FechaHasta = new DateTime(2025, 05, 26),
            };

            ViajeFiles.EscribirViajeAJson(viaje);

            List<Compra> compras = new List<Compra>()
            {
                new Compra
                {
                    CodigoProducto = 3,
                    DniCliente = 45950945,
                    CantidadComprada = 1,
                    FechaEntregaSolicitada = new DateTime(2025, 10, 25),
                    FechaEntregaEstimada =  new DateTime(2025, 10, 25),
                    MontoTotal = 90.75,
                    Latitud = -35.0,
                    Longitud = -67.0,
                    PrecioProducto = 10.0
                },
                new Compra
                {
                    CodigoProducto = 2,
                    DniCliente = 4244634,
                    CantidadComprada = 1,
                    FechaEntregaSolicitada = new DateTime(2025, 10, 26),
                    FechaEntregaEstimada =  new DateTime(2025, 10, 26),
                    MontoTotal = 242.0,
                    Latitud = -32.0,
                    Longitud = -60.0,
                    PrecioProducto = 100.0
                },
                new Compra
                {
                    CodigoProducto = 1,
                    DniCliente = 45950945,
                    CantidadComprada = 1,
                    FechaEntregaSolicitada = new DateTime(2025, 10, 29),
                    FechaEntregaEstimada =  new DateTime(2025, 10, 29),
                    MontoTotal = 2087.25,
                    Latitud = -35.0,
                    Longitud = -67.0,
                    PrecioProducto = 100.0
                },
            };

            foreach (Compra compra in compras)
            {
                CompraFiles.EscribirCompraAJson(compra);
            }

            List<Camioneta> camionetas = new List<Camioneta>()
            {
                new Camioneta
                {
                    Patente = "AA003OD",
                    Tipo = "DOBLE CABINA",
                    TamañoDeCargaEnCm3 = 3300,
                    DistanciaMaximaEnKm = 350
                },
                new Camioneta
                {
                    Patente = "AE718OJ",
                    Tipo = "FURGON",
                    TamañoDeCargaEnCm3 = 5800,
                    DistanciaMaximaEnKm = 550
                },
                new Camioneta
                {
                    Patente = "AB578OE",
                    Tipo = "PISO CABINA",
                    TamañoDeCargaEnCm3 = 6700,
                    DistanciaMaximaEnKm = 750
                }
            };

            foreach (Camioneta camioneta in camionetas)
            {
                CamionetaFiles.EscribirCamionetaAJson(camioneta);
            }

            List<Producto> productos = new List<Producto>()
            {
                new Producto
                {
                    Nombre = "Coca Cola",
                    Marca = "Coca Cola Company",
                    AltoCaja = 10.0,
                    AnchoCaja = 10.0,
                    ProfundidadCaja = 5.0,
                    PrecioUnitario = 100.0,
                    StockMinimo = 10,
                    Stock = 2000
                },
                new Producto
                {
                    Nombre = "Rocklets",
                    Marca = "Arcor",
                    AltoCaja = 10.0,
                    AnchoCaja = 10.0,
                    ProfundidadCaja = 5.0,
                    PrecioUnitario = 100.0,
                    StockMinimo = 10,
                    Stock = 1000
                },
                new Producto
                {
                    Nombre = "Oreo",
                    Marca = "Arcor",
                    AltoCaja = 10.0,
                    AnchoCaja = 10.0,
                    ProfundidadCaja = 10.0,
                    PrecioUnitario = 10.0,
                    StockMinimo = 10,
                    Stock = 1000
                },
            };

            foreach (Producto producto in productos)
            {
                ProductoFiles.EscribirProductosAJson(producto);
            }
        }

        [Test]
        public void AsignarViajeFechaMenorALaActualTest()
        {
            ViajeDTO viaje = new ViajeDTO()
            {
                FechaDesde = new DateTime(2023, 10, 19),
                FechaHasta = new DateTime(2022, 10, 19)
            };

            Result result = viajeService.AgregarViaje(viaje);

            Assert.That(result.Message, Is.EqualTo("La fecha de inicio es menor a la actual"));
            Assert.That(result.Status, Is.EqualTo(400));
        }

        [Test]
        public void AsignarViajeFechaMayorA7DiasTest()
        {
            ViajeDTO viaje = new ViajeDTO()
            {
                FechaDesde = new DateTime(2025, 10, 19),
                FechaHasta = new DateTime(2025, 10, 27)
            };

            Result result = viajeService.AgregarViaje(viaje);

            Assert.That(result.Message, Is.EqualTo("La fecha de entrega es mayor a 7 dias de la fecha de salida"));
            Assert.That(result.Status, Is.EqualTo(400));
        }

        [Test]
        public void AsignarViajeNoHayCompraTest()
        {
            ViajeDTO viaje = new ViajeDTO()
            {
                FechaDesde = new DateTime(2027, 10, 19),
                FechaHasta = new DateTime(2027, 10, 21)
            };

            Result result = viajeService.AgregarViaje(viaje);

            Assert.That(result.Message, Is.EqualTo("No hay ninguna compra ingresada en este rango de fechas"));
            Assert.That(result.Status, Is.EqualTo(404));
        }

        [Test]
        public void AsignarViajeYaExisteViajeTest()
        {
            ViajeDTO viaje = new ViajeDTO()
            {
                FechaDesde = new DateTime(2025, 05, 24),
                FechaHasta = new DateTime(2025, 05, 26)
            };

            Result result = viajeService.AgregarViaje(viaje);

            Assert.That(result.Message, Is.EqualTo("Ya existe un viaje entre esas fechas"));
            Assert.That(result.Status, Is.EqualTo(400));
        }

        [Test]
        public void AsignarViajeOkey()
        {
            ViajeDTO viaje = new ViajeDTO()
            {
                FechaDesde = new DateTime(2025, 10, 25),
                FechaHasta = new DateTime(2025, 10, 29)
            };

            Result result = viajeService.AgregarViaje(viaje);

            Assert.That(result.Message, Is.EqualTo("El viaje se cargo correctamente"));
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(archivoTemporalViajes))
            {
                File.Delete(archivoTemporalViajes);
            }
            if (File.Exists(archivoTemporalCompras))
            {
                File.Delete(archivoTemporalCompras);
            }
            if (File.Exists(archivoTemporalCamionetas))
            {
                File.Delete(archivoTemporalCamionetas);
            }
            if (File.Exists(archivoTemporalProductos))
            {
                File.Delete(archivoTemporalProductos);
            }
            ViajeFiles.SetRutaArchivo(rutaOriginalViajes);
            CompraFiles.SetRutaArchivo(rutaOriginalCompras);
            CamionetaFiles.SetRutaArchivo(rutaOriginalCamionetas);
            ProductoFiles.SetRutaArchivo(rutaOriginalProductos);
        }
    }
}