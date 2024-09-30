using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TIEntities
{
    public class Compra
    {
        public int Codigo { get; set; }
        public int CodigoProducto { get; set; }
        public int DniCliente { get; set; }
        public DateTime FechaCompra {  get; set; }
        public int CantidadComprada { get; set; }
        public DateTime FechaEntregaSolicitada { get; set; }
        public EnumEstadoCompra Estado {  get; set; }
        public double MontoTotal { get; set; }
        public string PuntoDestino { get; set; }
        public double PrecioProducto { get; set; }
        public double CalcularMontoTotal()
        {
            double Monto = (PrecioProducto * CantidadComprada) * 1.21;
            if (CantidadComprada > 4)
            {
              Monto =  CalcularPorcentajeDescuento(Monto);
            }
            return Monto;
        }
        public double CalcularPorcentajeDescuento(double Monto)
        {
            return Monto * 0.75;
        }

        public Compra()
        {
            MontoTotal = CalcularMontoTotal();
            FechaCompra = DateTime.Now;
            Estado = EnumEstadoCompra.OPEN;
        }

    }
}
