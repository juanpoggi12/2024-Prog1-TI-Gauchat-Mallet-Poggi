using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace EntitiesDTO
{
    public class CompraDTO
    {
        [Required(ErrorMessage = "Ingrese el codigo del producto")]
        [Range(0, int.MaxValue, ErrorMessage = "El codigo no puede ser negativo")]
        public int CodigoProducto { get; set; }
        [Required(ErrorMessage = "Ingrese el dni del cliente")]
        [Range(0, int.MaxValue, ErrorMessage = "Dni no puede ser negativo")]
        public int DniCliente { get; set; }
        [Required(ErrorMessage = "Ingrese la cantidad a comprar")]
        [Range(1, int.MaxValue, ErrorMessage = "la cantidad a comprar no puede ser negativa o 0")]
        public int CantidadComprada { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime FechaEntregaSolicitada { get; set; }
    }
}