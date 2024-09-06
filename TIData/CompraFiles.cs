using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIEntities;
using Newtonsoft;
using Newtonsoft.Json;
namespace TIData
{
    public class CompraFiles
    {
        private static string rutaArchivo = Path.GetFullPath("compra.json");

        public static void EscribirCompra(Compra compra)
        {
            List<Compra> compras = LeerCompra();

            if(compra.Codigo == 0)
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

        public static List<Compra> LeerCompra()
        {
            if (File.Exists($"{rutaArchivo}"))
            {
                var json = File.ReadAllText($"{rutaArchivo}");
                return JsonConvert.DeserializeObject<List<Compra>>(json);
            } else
            {
                return new List<Compra>();
            }
        }
    }
}
