using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIEntities;

namespace TIData
{
    public class CamionetaFiles
    {
        private static string rutaArchivo = Path.GetFullPath("Camioneta.json");
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
