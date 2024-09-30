using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIEntities;
using Newtonsoft;
using Newtonsoft.Json

namespace TIData
{
    public class ViajeFiles
    {
        private static string rutaArchivo = Path.GetFullPath("Viaje.json");

        public static void EscribirViajeAJson(Viaje viaje)
        {
            List<Viaje> viajes = LeerViajeAJson();
            if (viaje.Codigo == 0)
            {
                viaje.Codigo = viajes.Count();
            }
            else
            {
                viajes.RemoveAll(x => x.Codigo == viaje.Codigo);
            }

            viajes.Add(viaje);

            var json = JsonConvert.SerializeObject(viajes, Formatting.Indented);
            File.WriteAllText($"{rutaArchivo}", json);
        }

        public static List<Viaje> LeerViajeAJson()
        {
            if (File.Exists($"{rutaArchivo}"))
            {
                var json = File.ReadAllText($"{rutaArchivo}");
                return JsonConvert.DeserializeObject<List<Viaje>>(json);
            }
            else
            {
                return new List<Viaje>();
            }
        }
    }
}