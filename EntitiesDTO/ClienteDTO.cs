namespace EntitiesDTO
{
    public class ClienteDTO
    {
        public int Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public double Longitud { get; set; }
        public double Latitud { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
    }
}