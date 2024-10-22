using TIService;
using TIEntitiesDTO;
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
        public void TestAsignarViaje()
        {
            Assert.Pass();
        }
    }
}