using TIService;
using EntitiesDTO;
using TIEntities;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
namespace TITesting
{
    public class ViajeServiceTesting
    {
        ViajeService viajeService = new ViajeService();
        [SetUp]
        public void Setup()
        {
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
    }
}