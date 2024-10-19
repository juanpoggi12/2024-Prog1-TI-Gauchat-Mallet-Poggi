using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using NUnit.Framework;
using TIService;
using TIEntities;
using EntitiesDTO;

namespace TITesting
{
    public class CompraServiceTesting
    {
        CompraService compraService = new CompraService();
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
                FechaEntregaSolicitada = DateOnly.FromDateTime(DateTime.Now)
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
                FechaEntregaSolicitada = DateOnly.FromDateTime(DateTime.Now)
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
                FechaEntregaSolicitada = DateOnly.FromDateTime(DateTime.Now)
            };

            Result result = compraService.AgregarCompra(compraDTO);

            Assert.That(result.Message, Is.EqualTo("Producto no encontrado"));
            Assert.That(result.Status, Is.EqualTo(404));
        }
    }
}
