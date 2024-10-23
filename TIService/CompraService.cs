using EntitiesDTO;
using TIData;
using TIEntities;

namespace TIService
{
    public class CompraService
    {      
        public Result AgregarCompra(CompraDTO compraDTO)
        {
            Compra compra = new Compra();
            compra = PasarDtoAEntity(compraDTO);
            compra.FechaEntregaEstimada = compra.FechaEntregaSolicitada;

            Producto producto = ProductoFiles.LeerProductosAJson().FirstOrDefault(x => x.Codigo == compra.CodigoProducto);
            if (producto == null)
            {
                return new Result { Message = "Producto no encontrado", Status = 404 };
            }

            var resultado = producto.ValidarStock(compra.CantidadComprada);
            if (!resultado.Success)
            {
                return new Result { Message = resultado.Message, Status = 400 };
            }
            compra.PrecioProducto = producto.PrecioUnitario;
            compra.MontoTotal = compra.CalcularMontoTotal();

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

        private static CompraDTO PasarEnityADto(Compra compra)
        {
            return new CompraDTO
            {
                CodigoProducto = compra.CodigoProducto,
                FechaEntregaSolicitada = compra.FechaEntregaSolicitada,
                DniCliente = compra.DniCliente,
                CantidadComprada = compra.CantidadComprada,
            };
        }

        private static Compra PasarDtoAEntity(CompraDTO compraDTO)
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