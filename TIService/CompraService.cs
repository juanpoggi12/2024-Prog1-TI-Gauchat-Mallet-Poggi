using EntitiesDTO;
using TIData;
using TIEntities;

namespace TIService
{
    public class CompraService
    {
        public Result ValidarCompletitudCompra(Compra compra)
        {
            var validacion = new (object valor, string mensaje)[]
            {
                (compra.CantidadComprada, "Falta agregar cantidad comprada"),
                (compra.FechaEntregaSolicitada, "Falta agregar fecha de entrega solicitada"),
                (compra.DniCliente, "Falta agregar dni del cliente"),
                (compra.CodigoProducto, "Falta agregar codigo producto")
            };

            foreach (var (valor, mensaje) in validacion)
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

        public Result AgregarCompra(CompraDTO compraDTO)
        {
            Compra compra = new Compra();
            compra = PasarDtoAEntity(compraDTO);
            var resultado = ValidarCompletitudCompra(compra);
            if (!resultado.Success)
            {
                return new Result { Message = resultado.Message, Status = 400 };
            }

            Producto producto = ProductoFiles.LeerProductosAJson().FirstOrDefault(x => x.Codigo == compra.Codigo);
            if (producto == null)
            {
                return new Result { Message = "Producto no encontrado", Status = 404 };
            }

            resultado = producto.ValidarStock(compra.CantidadComprada);
            if (!resultado.Success)
            {
                return new Result { Message = resultado.Message, Status = 400 };
            } 

            Cliente cliente = ClienteFiles.LeerClienteAJson().FirstOrDefault(x => x.Dni == compra.DniCliente);
            if (cliente == null)
            {
                return new Result { Message = "Cliente no encontrado", Status = 404 };
            }

            compra.Longitud = cliente.Longitud;
            compra.Latitud = cliente.Latitud;
            ProductoService productoService = new ProductoService();
            productoService.ActualizarStockProducto(producto.Codigo, -(compra.CantidadComprada));
            CompraFiles.EscribirCompraAJson(compra);
            return new Result { Message = "La compra se agrego correctamente", Success = true };
        }

        public static CompraDTO PasarEnityADto(Compra compra)
        {
            return new CompraDTO
            {
                CodigoProducto = compra.CodigoProducto,
                FechaEntregaSolicitada = compra.FechaEntregaSolicitada,
                DniCliente = compra.DniCliente,
                CantidadComprada = compra.CantidadComprada,
            };
        }

        public static Compra PasarDtoAEntity(CompraDTO compraDTO)
        {
            return new Compra
            {
                CodigoProducto = compraDTO.CodigoProducto,
                FechaEntregaSolicitada = compraDTO.FechaEntregaSolicitada,
                DniCliente = compraDTO.DniCliente,
                CantidadComprada = compraDTO.CantidadComprada,
            };
        }
    }
}