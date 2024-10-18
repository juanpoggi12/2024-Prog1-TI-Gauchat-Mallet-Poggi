namespace TIEntities
{
    public class Cliente
    {
        public int Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public double Longitud { get; set; }
        public double Latitud { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        public DateTime? FechaUltimaActualizacion { get; set; }
        public DateTime? FechaDeEliminacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public Cliente()
        {
            FechaCreacion = DateTime.Now;
        }
    }
}