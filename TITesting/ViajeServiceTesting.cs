using TIService;
using EntitiesDTO;
using TIEntities;
using TIData;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using TIData;
namespace TITesting
{
    public class ViajeServiceTesting
    {
        ViajeService viajeService;
        private string archivoTemporalViajes;
        private string rutaOriginalViajes;
        private string archivoTemporalCompras;
        private string rutaOriginalCompras;

        [SetUp]
        public void Setup()
        {
            viajeService = new ViajeService();

            // Guardar la ruta original del archivo (en la carpeta "Archivos")
            rutaOriginalViajes = Path.GetFullPath(Path.Combine("TIAPI", "Viaje.json"));

            // Crear un archivo temporal para las pruebas
            archivoTemporalViajes = Path.Combine(Path.GetTempPath(), "ViajeTest.json");

            // Establecer la ruta del archivo temporal
            ViajeFiles.SetRutaArchivo(archivoTemporalViajes);

            rutaOriginalCompras = Path.GetFullPath(Path.Combine("TIAPI", "compra.json"));
            archivoTemporalCompras = Path.Combine(Path.GetTempPath(), "CompraTest.json");
            CompraFiles.SetRutaArchivo(archivoTemporalCompras);

            List<Compra> compras = new List<Compra>() 
            {
                new Compra
                {
                    CodigoProducto = 3,
                    DniCliente = 45950945,
                    CantidadComprada = 10,
                    FechaEntregaSolicitada = new DateTime(2025-10-25),
                    FechaEntregaEstimada =  new DateTime(2025-10-25),
                    MontoTotal = 90.75,
                    Latitud = -35.0,
                    Longitud = -67.0,
                    PrecioProducto = 10.0
                },
                new Compra
                {
                    CodigoProducto = 2,
                    DniCliente = 4244634,
                    CantidadComprada = 10,
                    FechaEntregaSolicitada = new DateTime(2025-10-26),
                    FechaEntregaEstimada =  new DateTime(2025-10-26),
                    MontoTotal = 242.0,
                    Latitud = -32.0,
                    Longitud = -60.0,
                    PrecioProducto = 100.0
                },
                new Compra
                {
                    CodigoProducto = 1,
                    DniCliente = 45950945,
                    CantidadComprada = 10,
                    FechaEntregaSolicitada = new DateTime(2025-10-29),
                    FechaEntregaEstimada =  new DateTime(2025-10-29),
                    MontoTotal = 2087.25,
                    Latitud = -35.0,
                    Longitud = -67.0,
                    PrecioProducto = 100.0
                },

            };

            foreach(Compra compra in compras)
            {
                CompraFiles.EscribirCompraAJson(compra);
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
        public void AsignarViajeNoHayComraTest()
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
                FechaDesde = new DateTime(2024, 10, 19),
                FechaHasta = new DateTime(2024, 10, 19)
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
                FechaDesde = new DateTime(2025, 10, 22),
                FechaHasta = new DateTime(2025, 10, 26)
            };

            Result result = viajeService.AgregarViaje(viaje);

            Assert.That(result.Message, Is.EqualTo("El viaje se cargo correctamente"));
        }

        [TearDown]
        public void TearDown()
        {
            // Eliminar el archivo temporal después de las pruebas
            if (File.Exists(archivoTemporalViajes))
            {
                File.Delete(archivoTemporalViajes);
            }
            if (File.Exists(archivoTemporalCompras))
            {
                File.Delete(archivoTemporalCompras);
            }

            // Restaurar la ruta original del archivo
            ViajeFiles.SetRutaArchivo(rutaOriginalViajes);
            CompraFiles.SetRutaArchivo(rutaOriginalCompras);

        }
    }
}