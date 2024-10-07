using EntitiesDTO;
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
                    (valor is string str && string.IsNullOrEmpty(str)) ||
                    (valor is int num && num <= 0))
                {
                    return new Result { Message = mensaje, Success = false };
                }
            }

            return new Result { Success = true };
        }

        public Result AgergarProducto(ProductoDTO productoDTO)
        {
            Producto producto = new Producto();
            producto = PasarDtoAEntitie(productoDTO);
            var Result = ValidarCompletitudProducto(producto);
            if (!Result.Success)
            {
                return new Result { Message = Result.Message };
            }
            ProductoFiles.EscribirProductosAJson(producto);
            return new Result { Success = true };
        }

        public Result ActualizarStockProducto(int Codigo, int Cantidad)
        {
            var Listado = ProductoFiles.LeerProductosAJson();
            var Result = Listado.FirstOrDefault(x => x.Codigo == Codigo);
            if (Result == null)
            {
                return new Result { Success = false, Message = "Producto no encontrado" };
            }
            Result.Stock = Result.Stock + Cantidad;
            ProductoFiles.EscribirProductosAJson(Result);
            return new Result { Success = true };
        }

        public ProductoDTO PasarEntitieADto(Producto producto)
        {
            ProductoDTO productoDTO = new ProductoDTO();
            productoDTO.Nombre = producto.Nombre;
            productoDTO.Marca = producto.Marca;
            productoDTO.PrecioUnitario = producto.PrecioUnitario;
            productoDTO.ProfundidadCaja = producto.ProfundidadCaja;
            productoDTO.Stock = producto.Stock;
            productoDTO.AnchoCaja = producto.AnchoCaja;
            productoDTO.StockMinimo = producto.StockMinimo;
            productoDTO.AltoCaja = producto.AltoCaja;
            return productoDTO;
        }

        public Producto PasarDtoAEntitie(ProductoDTO productoDTO)
        {
            Producto producto = new Producto();
            producto.Nombre = productoDTO.Nombre;
            producto.Marca = productoDTO.Marca;
            producto.PrecioUnitario = productoDTO.PrecioUnitario;
            producto.ProfundidadCaja = productoDTO.ProfundidadCaja;
            producto.Stock = productoDTO.Stock;
            producto.AnchoCaja = productoDTO.AnchoCaja;
            producto.StockMinimo = productoDTO.StockMinimo;
            producto.AltoCaja = productoDTO.AltoCaja;
            return producto;
        }
    }
}