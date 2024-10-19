using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using NUnit.Framework;
using TIEntities;

namespace TITesting
{
    public class CompraEntity
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GradosARadianesTest()
        {
            double resultado = Compra.GradosARadianes(10);

            Assert.That(resultado, Is.EqualTo(0.17453292519943295));
        }

        [Test]
        public void CalcularDistanciaTest()
        {
            Compra compra = new Compra()
            { 
                Latitud = -35,
                Longitud = -67
            };

            double resultado = compra.CalcularDistancia();

            Assert.That(resultado, Is.EqualTo(661.18427631671841));
        }
    }
}
