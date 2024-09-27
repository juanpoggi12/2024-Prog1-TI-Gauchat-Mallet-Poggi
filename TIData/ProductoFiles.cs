using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TIEntities;
using Newtonsoft;
using Newtonsoft.Json;
using System.IO.Pipes;

namespace TIData
{
    public class ProductoFiles
    {
        private static string rutaArchivo = Path.GetFullPath("Producto.json");
        
        public static void EscribirProductosAJson(Producto producto)
        {
            List<Producto> productos = LeerProductosAJson();

            if(producto.Codigo == 0)
            {
                producto.Codigo = productos.Count() + 1; 
            }
            else
            {
                productos.RemoveAll(x => x.Codigo == producto.Codigo);
            }

            productos.Add(producto);

            var json =  JsonConvert.SerializeObject(productos, Formatting.Indented);
            File.WriteAllText(rutaArchivo, json);
        }

        public static List<Producto> LeerProductosAJson()
        {
            if (File.Exists($"{rutaArchivo}"))
            {
                var json = File.ReadAllText($"{rutaArchivo}");
                return JsonConvert.DeserializeObject<List<Producto>>(json);
            } else
            {
                return new List<Producto>();
            }
        }
    }
}
