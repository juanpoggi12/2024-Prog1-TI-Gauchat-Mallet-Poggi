using Newtonsoft.Json;
using TIEntities;

namespace TIData
{
    public class CamionetaFiles
    {
        private static string rutaArchivo = Path.GetFullPath("camioneta.Json");

        public static void SetRutaArchivo(string nuevaRuta)
        {
            rutaArchivo = nuevaRuta;
        }

        public static void EscribirCamionetaAJson(Camioneta camioneta)
        {
            List<Camioneta> camionetas = LeerCamionetaAJson();

            camionetas.Add(camioneta);

            var json = JsonConvert.SerializeObject(camionetas, Formatting.Indented);
            File.WriteAllText($"{rutaArchivo}", json);
        }

        public static List<Camioneta> LeerCamionetaAJson()
        {
            if (File.Exists($"{rutaArchivo}"))
            {
                var json = File.ReadAllText($"{rutaArchivo}");
                return JsonConvert.DeserializeObject<List<Camioneta>>(json);
            }
            else
            {
                return new List<Camioneta>();
            }
        }
    }
}