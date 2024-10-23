using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesDTO
{
    public class ViajeDTO
    {
        [DataType(DataType.DateTime)]
        public DateTime FechaDesde { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime FechaHasta { get; set; }
    }
}
