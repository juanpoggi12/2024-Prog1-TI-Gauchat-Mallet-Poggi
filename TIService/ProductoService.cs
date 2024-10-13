using EntitiesDTO;
using System.Timers;
using TIData;
using TIEntities;

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
                    (valor is string str && string.IsNullOrWhiteSpace(str)) ||
                    (valor is int num && num <= 0) ||
                    (valor is double dob && dob <= 0))
                {
                    return new Result { Message = mensaje };
                }
            }

            return new Result { Success = true };
        }

        public Result AgergarProducto(ProductoDTO productoDTO)
        {
            Producto producto = new Producto();
            producto = PasarDtoAEntity(productoDTO);
            var Result = ValidarCompletitudProducto(producto);
            if (!Result.Success)
            {
                return new Result { Message = Result.Message };
            }
            ProductoFiles.EscribirProductosAJson(producto);
            return new Result { Success = true, Message = "El producto se agrego correctamente" };
        }

        public Result ActualizarStockProducto(int Codigo, int Cantidad)
        {
            var Listado = ProductoFiles.LeerProductosAJson();
            var Result = Listado.FirstOrDefault(x => x.Codigo == Codigo);
            if (Result == null)
            {
                return new Result { Message = "Producto no encontrado" };
            }
            Result.Stock = Result.Stock + Cantidad;
            ProductoFiles.EscribirProductosAJson(Result);
            return new Result { Success = true, Message = "El producto se modifico correctamente" };
        }

        public ProductoDTO PasarEntityADto(Producto producto)
        {
            return new ProductoDTO
            {
                Nombre = producto.Nombre,
                Marca = producto.Marca,
                PrecioUnitario = producto.PrecioUnitario,
                ProfundidadCaja = producto.ProfundidadCaja,
                Stock = producto.Stock,
                AnchoCaja = producto.AnchoCaja,
                StockMinimo = producto.StockMinimo,
                AltoCaja = producto.AltoCaja
            };
        }

        public Producto PasarDtoAEntity(ProductoDTO productoDTO)
        {
            return new Producto
            {
                Nombre = productoDTO.Nombre,
                Marca = productoDTO.Marca,
                PrecioUnitario = productoDTO.PrecioUnitario,
                ProfundidadCaja = productoDTO.ProfundidadCaja,
                Stock = productoDTO.Stock,
                AnchoCaja = productoDTO.AnchoCaja,
                StockMinimo = productoDTO.StockMinimo,
                AltoCaja = productoDTO.AltoCaja
            };
        }
    }
}