using Newtonsoft.Json;
using TIEntities;

namespace TIData
{
    public class ProductoFiles
    {
        private static string rutaArchivo = Path.GetFullPath("D:\\Universidad\\2do año\\Programacion l\\Trabajo-Final\\TIAPI\\Producto.json");

        public static void EscribirProductosAJson(Producto producto)
        {
            List<Producto> productos = LeerProductosAJson();

            if (producto.Codigo == 0)
            {
                producto.Codigo = productos.Count() + 1;
            }
            else
            {
                productos.RemoveAll(x => x.Codigo == producto.Codigo);
            }

            productos.Add(producto);

            var json = JsonConvert.SerializeObject(productos, Formatting.Indented);
            File.WriteAllText(rutaArchivo, json);
        }

        public static List<Producto> LeerProductosAJson()
        {
            if (File.Exists($"{rutaArchivo}"))
            {
                var json = File.ReadAllText($"{rutaArchivo}");
                return JsonConvert.DeserializeObject<List<Producto>>(json);
            }
            else
            {
                return new List<Producto>();
            }
        }
    }
}