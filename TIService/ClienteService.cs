using EntitiesDTO;
using TIData;
using TIEntities;

namespace TIService
{
    public class ClienteService
    {
        private Result ValidarCompletitudClientes(Cliente cliente)
        {
            var validaciones = new (object valor, string mensaje)[]
            {
                (cliente.Nombre, "Falta agregar nombre"),
                (cliente.Apellido, "Falta agregar apellido"),
                (cliente.Dni, "Falta agregar DNI"),
                (cliente.Longitud, "Falta agregar longitud"),
                (cliente.Latitud, "Falta agregar latitud"),
                (cliente.Telefono, "Falta agregar telefono"),
                (cliente.Email, "Falta agregar mail")
            };

            foreach (var (valor, mensaje) in validaciones)
            {
                if (valor == null ||
                    (valor is string str && string.IsNullOrEmpty(str)) ||
                    (valor is int num && num <= 0))
                {
                    return new Result { Message = mensaje };
                }
            }

            return new Result { Success = true };
        }

        public Result AgregarCliente(ClienteDTO clienteDTO)
        {
            Cliente cliente = new Cliente();
            cliente = PasarDtoAEntitie(clienteDTO);
            var resultado = ValidarCompletitudClientes(cliente);
            if (!resultado.Success)
            {
                return new Result { Success = false, Message = resultado.Message };
            }
            ClienteFiles.EscribirClienteAJson(cliente);
            return new Result { Success = true };
        }

        public List<ClienteDTO> ObtenerClientes()
        {
            List<ClienteDTO> ListaClientes = new List<ClienteDTO>();
            var result = ClienteFiles.LeerClienteAJson().Where(x => x.FechaDeEliminacion == null).ToList();
            foreach (var cliente in result)
            {
                var NewDTO = PasarEntitieADto(cliente);
                ListaClientes.Add(NewDTO);
            }
            return ListaClientes;
        }

        public Result EditarCliente(int Dni, ClienteDTO clienteTemporalDTO)
        {
            var clienteTemporal = PasarDtoAEntitie(clienteTemporalDTO);
            Cliente cliente = ClienteFiles.LeerClienteAJson().FirstOrDefault(x => x.Dni == clienteTemporal.Dni);
            var resultado = ValidarCompletitudClientes(clienteTemporal);

            if (cliente == null || !resultado.Success)
            {
                return new Result { };
            }
            cliente.Dni = clienteTemporal.Dni;
            cliente.Nombre = clienteTemporal.Nombre;
            cliente.Apellido = clienteTemporal.Apellido;
            cliente.Email = clienteTemporal.Email;
            cliente.Telefono = clienteTemporal.Telefono;
            cliente.Longitud = clienteTemporal.Longitud;
            cliente.Latitud = clienteTemporal.Latitud;
            cliente.FechaDeNacimiento = clienteTemporal.FechaDeNacimiento;
            cliente.FechaUltimaActualizacion = DateTime.Now;
            ClienteFiles.EscribirClienteAJson(cliente);

            return new Result { Success = true };
        }

        public Result EliminarCliente(int dni)
        {
            List<Cliente> clientes = ClienteFiles.LeerClienteAJson();

            Cliente cliente = clientes.FirstOrDefault(x => x.Dni == dni);

            if (cliente == null)
            {
                return new Result { Message = "No se encontro al cliente" };
            }

            cliente.FechaDeEliminacion = DateTime.Now;

            ClienteFiles.EscribirClienteAJson(cliente);

            return new Result { Success = true };
        }

        public ClienteDTO PasarEntitieADto(Cliente cliente)
        {
            ClienteDTO clienteDTO = new ClienteDTO();
            clienteDTO.Dni = cliente.Dni;
            clienteDTO.FechaDeNacimiento = cliente.FechaDeNacimiento;
            clienteDTO.Telefono = cliente.Telefono;
            clienteDTO.Email = cliente.Email;
            clienteDTO.Latitud = cliente.Latitud;
            clienteDTO.Longitud = cliente.Longitud;
            clienteDTO.Nombre = cliente.Nombre;
            clienteDTO.Apellido = cliente.Apellido;
            return clienteDTO;
        }

        public Cliente PasarDtoAEntitie(ClienteDTO clienteDTO)
        {
            Cliente cliente = new Cliente();
            cliente.Dni = clienteDTO.Dni;
            cliente.FechaDeNacimiento = clienteDTO.FechaDeNacimiento;
            cliente.Telefono = clienteDTO.Telefono;
            cliente.Email = clienteDTO.Email;
            cliente.Latitud = clienteDTO.Latitud;
            cliente.Longitud = clienteDTO.Longitud;
            cliente.Nombre = clienteDTO.Nombre;
            cliente.Apellido = clienteDTO.Apellido;
            return cliente;
        }
    }
}