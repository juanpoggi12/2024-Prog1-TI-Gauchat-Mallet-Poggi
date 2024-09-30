namespace EntitiesDTO
{
    public class ProductoDTO
    {
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public double AltoCaja { get; set; }
        public double AnchoCaja { get; set; }
        public double ProfundidadCaja { get; set; }
        public double PrecioUnitario { get; set; }
        public int StockMinimo { get; set; }
        public int Stock { get; set; }
    }
}