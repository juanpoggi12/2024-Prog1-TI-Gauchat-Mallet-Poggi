using Newtonsoft.Json;
using TIEntities;

namespace TIData
{
    public class CompraFiles
    {
        private static string rutaArchivo = Path.GetFullPath("compra.json");

        public static void SetRutaArchivo(string nuevaRuta)
        {
            rutaArchivo = nuevaRuta;
        }

        public static void EscribirCompraAJson(Compra compra)
        {
            List<Compra> compras = LeerCompraAJson();

            if (compra.Codigo == 0)
            {
                compra.Codigo = compras.Count() + 1;
            }
            else
            {
                compras.RemoveAll(x => x.Codigo == compra.Codigo);
            }

            compras.Add(compra);

            var json = JsonConvert.SerializeObject(compras, Formatting.Indented);
            File.WriteAllText(rutaArchivo, json);
        }

        public static List<Compra> LeerCompraAJson()
        {
            if (File.Exists($"{rutaArchivo}"))
            {
                var json = File.ReadAllText($"{rutaArchivo}");
                return JsonConvert.DeserializeObject<List<Compra>>(json);
            }
            else
            {
                return new List<Compra>();
            }
        }
    }
}