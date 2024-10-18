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
                    (valor is string str && string.IsNullOrWhiteSpace(str)) ||
                    (valor is int num && num <= 0) ||
                    (valor is double dob && dob == 0) ||
                    (valor is DateTime dt && dt == default))
                {
                    return new Result { Message = mensaje };
                }
            }

            return new Result { Success = true };
        }

        public Result AgregarCliente(ClienteDTO clienteDTO)
        {
            Cliente cliente = new Cliente();
            cliente = PasarDtoAEntity(clienteDTO);
            var resultado = ValidarCompletitudClientes(cliente);
            if (!resultado.Success)
            {
                return new Result { Message = resultado.Message };
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
            var clienteTemporal = PasarDtoAEntity(clienteTemporalDTO);
            Cliente cliente = ClienteFiles.LeerClienteAJson().FirstOrDefault(x => x.Dni == Dni);
            if (cliente == null)
            {
                return new Result { Message = "No se encontro al cliente a editar", Status = 404 };
            }
            var resultado = ValidarCompletitudClientes(clienteTemporal);

            if (!resultado.Success)
            {
                return new Result { Message = resultado.Message, Status = 400 };
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

            return new Result { Success = true, Message = "El cliente se edito correctamente"};
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

        private ClienteDTO PasarEntitieADto(Cliente cliente)
        {
            return new ClienteDTO
            {
                Dni = cliente.Dni,
                FechaDeNacimiento = cliente.FechaDeNacimiento,
                Telefono = cliente.Telefono,
                Email = cliente.Email,
                Latitud = cliente.Latitud,
                Longitud = cliente.Longitud,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
            };
        }

        private Cliente PasarDtoAEntity(ClienteDTO clienteDTO)
        {
            return new Cliente
            {
                Dni = clienteDTO.Dni,
                FechaDeNacimiento = clienteDTO.FechaDeNacimiento,
                Telefono = clienteDTO.Telefono,
                Email = clienteDTO.Email,
                Latitud = clienteDTO.Latitud,
                Longitud = clienteDTO.Longitud,
                Nombre = clienteDTO.Nombre,
                Apellido = clienteDTO.Apellido,  
            };
        }
    }
}