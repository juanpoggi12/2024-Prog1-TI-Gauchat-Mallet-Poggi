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
    public class CompraEntityTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CompraOkey()
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
