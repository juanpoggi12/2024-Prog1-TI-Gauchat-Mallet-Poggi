using Newtonsoft.Json;
using TIEntities;

namespace TIData
{
    public class CamionetaFiles
    {
        private static string rutaArchivo = Path.GetFullPath("camioneta.Json");

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