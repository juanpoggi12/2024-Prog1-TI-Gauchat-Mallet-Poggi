using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIEntities
{
    public class Cliente
    {
        //DNI, Nombre, Apellido, Email de contacto, Teléfono de contacto, latitud geográfica, longitud geográfica y fecha de nacimiento.
        public int Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public int Telefono { get; set; }
        public double Longitud { get; set; }
        public double Latitud { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        public DateTime? FechaUltimaActualizacion { get; set; }
        public DateTime? FechaDeEliminacion { get; set; }
        public DateTime DarDeAlta { get; set; }

        public Cliente()
        {
            DarDeAlta = DateTime.Now;
        }
       
    }

}

