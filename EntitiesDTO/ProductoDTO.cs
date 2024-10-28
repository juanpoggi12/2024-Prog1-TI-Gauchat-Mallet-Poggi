using System.ComponentModel.DataAnnotations;

namespace EntitiesDTO
{
    public class ProductoDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La marca es obligatoria")]
        [StringLength(50, ErrorMessage = "La marca no debe exceder los 50 caracteres")]
        public string Marca { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "El alto de la caja debe ser mayor o igual a 1")]
        public double AltoCaja { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "El ancho de la caja debe ser mayor o igual a 1")]
        public double AnchoCaja { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "La profundidad de la caja debe ser mayor o igual a 1")]
        public double ProfundidadCaja { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio unitario debe ser mayor que 0")]
        public double PrecioUnitario { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock mínimo debe ser un número positivo")]
        public int StockMinimo { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser un número positivo")]
        public int Stock { get; set; }
    }
}