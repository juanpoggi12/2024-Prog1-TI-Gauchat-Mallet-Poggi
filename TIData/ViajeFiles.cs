using Newtonsoft.Json;

using TIEntities;

namespace TIData
{
    public class ViajeFiles
    {
        private static string rutaArchivo = Path.GetFullPath(Path.Combine("TIAPI", "Viaje.json"));

        // Método para establecer una ruta personalizada
        public static void SetRutaArchivo(string nuevaRuta)
        {
            rutaArchivo = nuevaRuta;
        }

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