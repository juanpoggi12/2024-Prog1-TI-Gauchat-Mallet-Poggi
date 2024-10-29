using EntitiesDTO;
using TIData;
using TIEntities;

namespace TIService
{
    public class ClienteService
    {
        public Result AgregarCliente(ClienteDTO clienteDTO)
        {
            
            Cliente cliente = new Cliente();
            cliente = PasarDtoAEntity(clienteDTO);
            Compra compra = new Compra
            {
                Latitud = cliente.Latitud,
                Longitud = cliente.Longitud
            };
            if (compra.CalcularDistancia() > 750)
            {
                return new Result { Message = "No llegamos hasta ahi, muy lejos", Status = 400};
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

            return new Result { Success = true, Message = "El cliente se edito correctamente" };
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