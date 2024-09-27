using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIEntities;
using TIData;

namespace TIService
{
   public class ProductoService
    {

        public Result ValidarCompletitudProducto(Producto producto)
        {
            var validaciones = new (object valor, string mensaje)[]
            {
                (producto.Marca, "Falta la Marca"),
                (producto.AltoCaja, "Falta alto de la caja"),
                (producto.AnchoCaja, "Falta ancho de la caja"),
                (producto.ProfundidadCaja, "Falta profundidad caja"),
                (producto.PrecioUnitario, "Falta el precio"),
                (producto.StockMinimo, "Falta stock minimo"),
                (producto.Stock, "Falta stock total")
            };

            foreach (var (valor, mensaje) in validaciones)
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

        public Result AgergarProducto(Producto producto)
        {
            var Result = ValidarCompletitudProducto(producto);
                if (Result.Success == false)
            {
                return new Result { Message = Result.Message };
            }
            ProductoFiles.EscribirProductosAJson(producto);
            return new Result { Success = true };

        }
        public Result ActualizarStockProducto (int Codigo, int Cantidad)
        {
            var Listado = ProductoFiles.LeerProductosAJson();
            var Result = Listado.FirstOrDefault(x => x.Codigo == Codigo);
            if (Result == null)
            {
                return new Result { Success = false, Message = "Producto no encontrado" };
            }
            Result.Stock = Cantidad;
            return new Result { Success = true };
        }
    }
}
