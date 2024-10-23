using System.ComponentModel.DataAnnotations;

namespace EntitiesDTO
{
    public class ClienteDTO
    {
        [Required(ErrorMessage = "Ingrese el DNI del cliente")]
        [Range(8, int.MaxValue, ErrorMessage = "El DNI no puede ser negativo o 0")]
        public int Dni { get; set; }
        [Required(ErrorMessage = "Ingrese el nombre del cliente")]
        [StringLength(20, ErrorMessage = "El nombre no puede superar los 20 caracteres")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Ingrese el apellido del cliente")]
        [StringLength(50, ErrorMessage = "El apellido no puede superar los 50 caracteres")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "Ingrese el E-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Ingrese correctamente el E-mail")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Ingrese el numero de telefono cliente")]
        [RegularExpression(@"^\+?[0-9]{10,15}$", ErrorMessage = "El número de teléfono debe tener entre 10 y 15 dígitos, y puede incluir un signo '+' al principio")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Ingrese longitud")]
        [Range(-180, 180, ErrorMessage = "La longitud debe estar entre -180 y 180")]
        public double Longitud { get; set; }
        [Required(ErrorMessage = "Ingrese latitud")]
        [Range(-90, 90, ErrorMessage = "La latitud debe estar entre -90 y 90")]
        public double Latitud { get; set; }
        [Required(ErrorMessage = "Ingrese Fecha de nacimiento")]
        [DataType(DataType.DateTime, ErrorMessage = "Ingrese correctamente la fecha)]
        public DateTime FechaDeNacimiento { get; set; }
    }
}