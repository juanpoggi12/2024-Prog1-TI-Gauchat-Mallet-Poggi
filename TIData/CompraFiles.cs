using Newtonsoft.Json;
using TIEntities;

namespace TIData
{
    public class CompraFiles
    {
        private static string rutaArchivo = Path.GetFullPath(Path.Combine("TIAPI", "compra.json"));

        // Método para establecer una ruta personalizada
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
                compras.Add(compra);
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