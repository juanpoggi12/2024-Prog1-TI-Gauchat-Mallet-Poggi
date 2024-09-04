using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIEntities
{
    public  class Camioneta
    {
        public string Patente { get; set; }
        public string Tipo { get; set; }
        public int TamañoDeCargaEnCm3 {  get; set; }
        public int DistanciaMaximaEnKm { get; set; }
        
    }
}
