namespace TIEntities
{
    public class Producto
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public double AltoCaja { get; set; }
        public double AnchoCaja { get; set; }
        public double ProfundidadCaja { get; set; }
        public double PrecioUnitario { get; set; }
        public int StockMinimo { get; set; }
        public int Stock { get; set; }

        public Result ValidarStock(int CantidadComprada)
        {
            if (Stock < CantidadComprada)
            {
                return new Result {Message = $"No hay esa cantidad de stock, el stock actual es {Stock}" };
            }
            return new Result { Success = true };
        }      

        public double PasarACentimetrosCubicos()
        {
            return AltoCaja * AnchoCaja * ProfundidadCaja;
        }
    }
}