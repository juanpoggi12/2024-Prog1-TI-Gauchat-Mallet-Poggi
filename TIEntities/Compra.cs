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
        public bool Estado {  get; set; }
        public double MontoTotal { get; set; }

        public string PuntoDestino { get; set; }
        public double CalcularMontoTotal()
        {
           
        }
        public int CalcularPorcentajeIva()
        {

        }

    }
}
