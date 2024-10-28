using System.ComponentModel.DataAnnotations;

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