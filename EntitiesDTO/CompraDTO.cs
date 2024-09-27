using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesDTO
{
    public class CompraDTO
    {
        public int CodigoProducto { get; set; }
        public int DniCliente { get; set; }
        public int CantidadComprada { get; set; }
        public DateTime FechaEntregaSolicitada { get; set; }


    }
}
