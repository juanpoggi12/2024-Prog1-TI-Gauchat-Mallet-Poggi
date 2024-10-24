using Newtonsoft.Json;
using TIEntities;

namespace TIData
{
    public class ClienteFiles
    {
        private static string rutaArchivo = Path.GetFullPath("cliente.json");
        public static void EscribirClienteAJson(Cliente cliente)
        {
            List<Cliente> clientes = LeerClienteAJson();
            var clienteExistente = clientes.FirstOrDefault(x => x.Dni == cliente.Dni);
            if (clienteExistente != null)
            {
                clientes.RemoveAll(x => x.Dni == clienteExistente.Dni);               
            }          
            clientes.Add(cliente);
            var json = JsonConvert.SerializeObject(clientes, Formatting.Indented);
            File.WriteAllText(rutaArchivo, json);
        }
        public static List<Cliente> LeerClienteAJson()
        {
            if (File.Exists($"{rutaArchivo}"))
            {
                var json = File.ReadAllText($"{rutaArchivo}");
                return JsonConvert.DeserializeObject<List<Cliente>>(json);
            }
            else
            {
                return new List<Cliente>();
            }
        }
    }
}