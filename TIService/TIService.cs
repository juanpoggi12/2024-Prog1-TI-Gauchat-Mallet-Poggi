using TIEntities;
namespace TIService
{
    public class TIService
    {
        public Result ValidarCompletitudClientes(Cliente cliente)
        {
           if (cliente.Nombre == null)
            {
                return new Result { Message = "Falta agregar nombre", Success = false};
            }
           if(cliente.Apellido == null)
            {
                return new Result { Message = "Falta agregar apellido", Success = false };
            }
           if (cliente.Dni == null)
            {
                return new Result { Message = "Falta agregar apellido", Success = false };
            }
            if (cliente.Longitud == null)
            {
                return new Result { Message = "Falta agregar longitud", Success = false };
            }
            if (cliente.Latitud == null)
            {
                return new Result { Message = "Falta agregar latitud", Success = false };
            }
            if (cliente.Telefono == null)
            {
                return new Result { Message = "Falta agregar telefono", Success = false };
            }
            if (cliente.Email == null)
            {
                return new Result { Message = "Falta agregar mail", Success = false };
            }

            return new Result {};
        } 


    }
}
