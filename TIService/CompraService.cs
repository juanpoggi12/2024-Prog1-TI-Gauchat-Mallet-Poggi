using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIEntities;
using TIData;
using EntitiesDTO;

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
                    return new Result { Message = mensaje, Success = false };
                }
            }

            return new Result { Success = true };
        }

        public Result AgregarCompra(CompraDTO compraDTO)
        {
            Compra compra = new Compra();
             compra = PasarDtoAEntitie(compraDTO);
            var resultado = ValidarCompletitudCompra(compra);
            if (!resultado.Success)
            {
                return new Result {Message = resultado.Message };
            }
           Producto producto = ProductoFiles.LeerProductosAJson().FirstOrDefault(x => x.Codigo == compra.Codigo);
            resultado = producto.ValidarStock(compra.CantidadComprada);
            if (!resultado.Success)
            {
                return new Result {Message = resultado.Message };
            }
            Cliente cliente = ClienteFiles.LeerClienteAJson().FirstOrDefault(x => x.Dni == compra.DniCliente);
            if (cliente == null) {
                return new Result { Message = "Cliente no encontrado" };
            }
            compra.Longitud = cliente.Longitud;
            compra.Latitud = cliente.Latitud;
            ProductoService productoService = new ProductoService();
            productoService.ActualizarStockProducto(producto.Codigo, -(compra.CantidadComprada));
            CompraFiles.EscribirCompraAJson(compra);
            return new Result { Success = true };
        }
        public List<CompraDTO> ObtenerCompras()
        {
            List<CompraDTO> ComprasDTO = new List<CompraDTO>();   
            var lista = CompraFiles.LeerCompraAJson();  

            foreach(var recorrer in lista)
            {
                ComprasDTO.Add(PasarEnitieADto(recorrer));
            }
            return ComprasDTO;
        }
        
        
        public static CompraDTO PasarEnitieADto(Compra compra) 
        {
            CompraDTO compraDTO = new CompraDTO() 
            {
                CodigoProducto = compra.CodigoProducto,
                FechaEntregaSolicitada = compra.FechaEntregaSolicitada,
                DniCliente = compra.DniCliente,
                CantidadComprada = compra.CantidadComprada,
            };

            return compraDTO;
        }
        public static Compra PasarDtoAEntitie(CompraDTO compraDTO)
        {
            Compra compra = new Compra() 
            {
                CodigoProducto = compraDTO.CodigoProducto,
                FechaEntregaSolicitada = compraDTO.FechaEntregaSolicitada,
                DniCliente = compraDTO.DniCliente,
                CantidadComprada = compraDTO.CantidadComprada,  
            };
            
            return compra;
        }
        
    }
}
