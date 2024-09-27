using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesDTO
{
    public class ClienteDTO
    {
        public int Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public int Telefono { get; set; }
        public float Longitud { get; set; }
        public float Latitud { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
    }
}
