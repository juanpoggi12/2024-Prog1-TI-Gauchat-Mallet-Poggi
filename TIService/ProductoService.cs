using EntitiesDTO;
using System.Timers;
using TIData;
using TIEntities;

namespace TIService
{
    public class ProductoService
    {
        public Result AgergarProducto(ProductoDTO productoDTO)
        {
            Producto producto = new Producto();
            producto = PasarDtoAEntity(productoDTO);
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

        public List<ProductoDTO> ObtenerListaProductosConStockBajo()
        {
            List<Producto> Productos = ProductoFiles.LeerProductosAJson().Where(x => x.Stock < x.StockMinimo).ToList();
            List<ProductoDTO> ProductosDTO = new List<ProductoDTO>();
            foreach (Producto producto in Productos)
            {
                var ProductoDTO = PasarEntityADto(producto);
                ProductosDTO.Add(ProductoDTO);
            }
            return ProductosDTO;
        }

        private ProductoDTO PasarEntityADto(Producto producto)
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

        private Producto PasarDtoAEntity(ProductoDTO productoDTO)
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